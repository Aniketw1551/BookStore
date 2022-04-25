using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    { 
        private readonly ICartBL cartBL;

        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }

        [Authorize]
        [HttpPost("Add")]
        public IActionResult AddCart(Cart cart)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "Id").Value);
                var cartdetails = this.cartBL.AddCart(cart,userId);
                if (cartdetails != null)
                {
                    return this.Ok(new { Success = true, message = " Book Added in cart Sucessfully", Response = cartdetails });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Cart Add Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("{UserId}/ Get")]
        public IActionResult GetCartDetailsByUser()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "Id").Value);
                var cartdetails = this.cartBL.GetCartDetailsByUser(userId);
                if (cartdetails != null)
                {
                    return this.Ok(new { Success = true, message = "cart Details Fetched Sucessfully", Response = cartdetails });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Error! Please Enter Correct User Id" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

       [Authorize]
       [HttpPut("Update")]
        public IActionResult UpdateCart(Cart cartModel)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "Id").Value);
                var cart = this.cartBL.UpdateCart(cartModel, userId);
                if (cart != null)
                {
                    return this.Ok(new { Success = true, message = "Cart Details Updated Sucessfully", Response = cart });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Cart Update Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("Delete")]
        public IActionResult DeletCart(int cartId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "Id").Value);
                if(this.cartBL.DeleteCart(cartId, userId))
                
                {
                    return this.Ok(new { Success = true, message = "Cart Deleted Sucessfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Cart Delete Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}