﻿using BusinessLayer.Interface;
using CommonLayer.CustomExceptions;
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
    public class AdminController : ControllerBase
    {
        private readonly IAdminBL adminBL;

        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
        }

        [HttpPost("Login")]
        public IActionResult AdminLogin(string email, string password)
        {
            try
            {
                var admin = this.adminBL.AdminLogin(email, password);
                if (admin != null)
                {
                    return this.Ok(new { Success = true, message = "Admin Logged In Sucessfully", Response = admin });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Please Enter Correct Credentials" });
                }
            }
            catch (Exception)
            {
                throw new AppException("Field can't be null");
            }
        }
    }
}
