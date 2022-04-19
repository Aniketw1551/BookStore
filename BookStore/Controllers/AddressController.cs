using BusinessLayer.Interface;
using CommonLayer.Model;
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
    public class AddressController : ControllerBase
    {
        private readonly IAddressBL addressBL;

        public AddressController(IAddressBL addressBL)
        {
            this.addressBL = addressBL;
        }

        [Authorize]
        [HttpPost("Add")]
        public IActionResult AddAddress(AddressModel addressModel)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var address = this.addressBL.AddAddress(addressModel, userId);
                if (address.Equals("Address Added Successfully"))
                {
                    return this.Ok(new { Status = true, Response = address });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Response=address });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { status = false, Response = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("{UserId}/Get")]
        public IActionResult GetAllAddresses()
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var addresses = this.addressBL.GetAllAddresses(userId);
                if (addresses!= null)
                {
                    return this.Ok(new { Status = true, Message = "All Address Fetched Successfully", Response = addresses });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Error! Please Enter Correct User Id" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { status = false, Response = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("Update")]
        public IActionResult UpdateAddress(AddressModel addressModel, int addressId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var address = this.addressBL.UpdateAddress(addressModel, addressId, userId);
                if (address != null)
                {
                    return this.Ok(new { Status = true, Message = "Address Updated Successfully", Response = address });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Please Enter Correct AddressId or TypeId" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { status = false, Response = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("Delete")]
        public IActionResult DeleteAddress(int addressId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (this.addressBL.DeleteAddress(addressId, userId))
                {
                    return this.Ok(new { Status = true, Message = "Address Deleted Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Please Enter Correct Address Id" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { status = false, Response = ex.Message });
            }
        }
    }
}
