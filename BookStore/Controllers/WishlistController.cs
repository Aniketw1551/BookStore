using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistBL wishlistBL;

        public WishlistController(IWishlistBL wishlistBL)
        {
            this.wishlistBL = wishlistBL;
        }

        [Authorize]
        [HttpPost("Add")]
        public IActionResult AddInWishlist(int bookId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.wishlistBL.AddWishlist(bookId, userId);
                if (result.Equals("Book is Successfully Added in Wishlist"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("{UserId}/Get")]
        public IActionResult GetAllWishlist()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var wishlist = this.wishlistBL.GetAllEntriesFromWishlist(userId);
                if (wishlist != null)
                {
                    return this.Ok(new { success = true, message = "All Wishlist Entries Fetched Successfully ", response = wishlist });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Please Enter Correct User Id" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("Delete")]
        public IActionResult DeleteWishlist(int wishlistId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (this.wishlistBL.DeleteWishlist(wishlistId, userId))
                {
                    return this.Ok(new { Status = true, Message = "Successfully Deleted Book From Wishlist" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Error! Wishlist Delete Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}