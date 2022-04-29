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
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;

        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        [HttpPost("Register")]
        public IActionResult UserRegister(UserModel userModel)
        {
            try
            {
                var user = this.userBL.UserRegister(userModel);
                if (user != null)
                {
                    return this.Ok(new { Success = true, message = "User Added Sucessfully", Response = user });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "User Registration Failed" });
                }
            }
            catch (Exception)
            {
                throw new AppException("Fields can't be null");
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(string email, string password)
        {
            try
            {
                var login = this.userBL.Login(email, password);
                if (login != null)
                {
                    return this.Ok(new { Success = true, message = "Logged In Sucessfully", Response = login });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Login Failed" });
                }
            }
            catch (Exception)
            {
                throw new AppException("Field Can't be null");
            }
        }

        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                var forgotPassword = this.userBL.ForgotPassword(email);
                if (forgotPassword != null)
                {
                    return this.Ok(new { Success = true, message = " Token Sent on given Mail To Reset The Password", Response = forgotPassword });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Failed to send token to given mail Id" });
                }
            }
            catch (Exception)
            {
                throw new KeyNotFoundException("Email Not Found");
            }
        }

        [Authorize]
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(string newPassword, string confirmPassword)
        {
            try
            {
                var email = User.Claims.FirstOrDefault(e => e.Type == "Email").Value.ToString();
                if (this.userBL.ResetPassword(email, newPassword, confirmPassword))
                {
                    return this.Ok(new { Success = true, message = " Password Changed Sucessfully " });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = " Password Change Failed!!! Please Try Again Later " });
                }
            }
            catch (Exception)
            {
                throw new AppException("New and Confirm Password Can't be Different");
            }
        }
    }
}