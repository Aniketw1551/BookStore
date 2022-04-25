using BusinessLayer.Interface;
using CommonLayer.Model;
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
    public class OrderController : ControllerBase
    {
        private readonly IOrderBL orderBL;

        public OrderController(IOrderBL orderBL)
        {
            this.orderBL = orderBL;
        }

        [HttpPost("Add")]
        public IActionResult AddOrder(AddOrderModel orderModel)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == "Id").Value);
                var order = this.orderBL.AddOrder(orderModel, userId);
                if (order!= null)
                {
                    return this.Ok(new { Status = true, Message = "Order Placed Successfully", Response = order });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Please Enter Correct BookId" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message });
            }
        }

        [HttpGet("Get")]
        public IActionResult GetAllOrders()
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == "Id").Value);
                var order = this.orderBL.GetAllOrders(userId);
                if (order != null)
                {
                    return this.Ok(new { Status = true, Message = "Order Details Fetched Successfully", Response = order});
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Please Login First to VIew All Orders" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, ex.Message });
            }
        }
    }
}