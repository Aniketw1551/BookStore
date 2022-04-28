using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMemoryCache memoryCache;

        private readonly IDistributedCache distributedCache;

        private readonly IBookBL bookBL;

        public BookController(IBookBL bookBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.bookBL = bookBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("Add")]
        public IActionResult AddBook(AddBookModel addbook)
        {
            try
            {
                var book = this.bookBL.AddBook(addbook);
                if (book!= null)
                {
                    return this.Ok(new { Success = true, message = "Book Added Sucessfully", Response = book });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Error While Adding Book" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet("{bookId}/Get")]
        public IActionResult GetBookByBookId(int bookId)
        {
            try
            {
                var book = this.bookBL.GetBookByBookId(bookId);
                if (book!= null)
                {
                    return this.Ok(new { Success = true, message = "Book Details of Given Book Id Fetched Sucessfully", Response = book });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Error! Please Enter Correct Book Id" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet("Get")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var allbooks = this.bookBL.GetAllBooks();
                if (allbooks != null)
                {
                    return this.Ok(new { Success = true, message = "All Book Details Fetched Sucessfully", Response = allbooks });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Error! Please Enter Correct Book Id" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        {
            var cacheKey = "customerList";
            string serializedCustomerList;
            var customerList = new List<BookModel>();
            var redisCustomerList = await distributedCache.GetAsync(cacheKey);
            if (redisCustomerList != null)
            {
                serializedCustomerList = Encoding.UTF8.GetString(redisCustomerList);
                customerList = JsonConvert.DeserializeObject<List<BookModel>>(serializedCustomerList);
            }
            else
            {
                customerList = (List<BookModel>)bookBL.GetAllBooks();
                serializedCustomerList = JsonConvert.SerializeObject(customerList);
                redisCustomerList = Encoding.UTF8.GetBytes(serializedCustomerList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCustomerList, options);
            }
            return Ok(customerList);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("Update")]
        public IActionResult UpdateBookDetails(BookModel bookModel)
        {
            try
            {
                var Book = this.bookBL.UpdateBookDetails(bookModel);
                if (Book!= null)
                {
                    return this.Ok(new { Success = true, message = "Book Details Updated Sucessfully", Response = Book });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Error! There was problem Updating the Book Details" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("Delete")]
        public IActionResult DeleteBook(int bookId)
        {
            try
            {
                if (this.bookBL.DeleteBook(bookId))
                {
                    return this.Ok(new { Success = true, message = "Book Deleted Sucessfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Please Enter Correct Book Id" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}
