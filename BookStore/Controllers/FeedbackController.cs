using BusinessLayer.Interface;
using CommonLayer.CustomExceptions;
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
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackBL feedbackBL;

        public FeedbackController(IFeedbackBL feedbackBL)
        {
            this.feedbackBL = feedbackBL;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("Add")]
        public IActionResult AddFeedback(FeedbackModel feedbackModel)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var feedback = this.feedbackBL.AddFeedback(feedbackModel, userId);
                if (feedback!= null)
                {
                    return this.Ok(new { Status = true, Message = "Feedback For This Book Added Successfully", Response = feedback });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = " Please Enter Correct BookId" });
                }
            }
            catch (Exception)
            {
                throw new AppException("Fields can't be null");
            }
        }

        [HttpGet("Get")]
        public IActionResult GetFeedback(int bookId)
        {
            try
            {
                var result = this.feedbackBL.GetAllFeedback(bookId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Feedback For Given Book Id Fetched Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Please Enter Correct BookId" });
                }
            }
            catch (Exception)
            {
                throw new AppException("Field can't be null");
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpPut("Update")]
        public IActionResult UpdateFeedback(FeedbackModel feedbackModel, int feedbackId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.feedbackBL.UpdateFeedback(feedbackModel, userId, feedbackId);
                if (result.Equals("Feedback For this Book is Updated Successfully"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception)
            {
                throw new AppException("Field can't be null");
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpPut("Delete")]
        public IActionResult DeleteFeedback(int feedbackId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (this.feedbackBL.DeleteFeedback(feedbackId, userId))
                {
                    return this.Ok(new { Status = true, Message = "Feedback Deleted Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Error While Deleting Feedback" });
                }
            }
            catch (Exception)
            {
                throw new AppException("Field can't be null");
            }
        }
    }
}