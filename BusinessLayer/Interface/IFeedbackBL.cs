using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IFeedbackBL
    {
        public FeedbackModel AddFeedback(FeedbackModel feedbackModel, int userId);
        public List<GetFeedbackModel> GetAllFeedback(int bookId);
        public string UpdateFeedback(FeedbackModel feedbackModel, int feedbackId, int userId);
        public bool DeleteFeedback(int feedbackId, int userId);
    }
}