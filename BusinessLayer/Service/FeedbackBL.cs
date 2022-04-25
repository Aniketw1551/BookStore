using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
   public class FeedbackBL : IFeedbackBL
    {
        private readonly IFeedbackRL feedbackRL;

        public FeedbackBL(IFeedbackRL feedbackRL)
        {
            this.feedbackRL = feedbackRL;
        }

        public FeedbackModel AddFeedback(FeedbackModel feedbackModel, int userId)
        {
            try
            {
                return this.feedbackRL.AddFeedback(feedbackModel, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<GetFeedbackModel> GetAllFeedback(int bookId)
        {
            try
            {
                return this.feedbackRL.GetAllFeedback(bookId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string UpdateFeedback(FeedbackModel feedbackModel, int feedbackId, int userId)
        {
            try
            {
                return this.feedbackRL.UpdateFeedback(feedbackModel,feedbackId,userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteFeedback(int feedbackId,int userId)
        {
            try
            {
                return this.feedbackRL.DeleteFeedback(feedbackId,userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
