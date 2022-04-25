using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Service
{
    public class FeedbackRL : IFeedbackRL
    {
        private SqlConnection sqlConnection;

        private IConfiguration Configuration { get; }

        public FeedbackRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public FeedbackModel AddFeedback(FeedbackModel feedbackModel, int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("spAddFeedback", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Comment", feedbackModel.Comment);
                    cmd.Parameters.AddWithValue("@Rating", feedbackModel.Rating);
                    cmd.Parameters.AddWithValue("@BookId", feedbackModel.BookId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    return feedbackModel;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<GetFeedbackModel> GetAllFeedback(int bookId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("spGetFeedback", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        List<GetFeedbackModel> feedbackModel = new List<GetFeedbackModel>();
                        while (reader.Read())
                        {
                            GetFeedbackModel getFeedback = new GetFeedbackModel();
                            UserModel user = new UserModel
                            {
                                FullName = reader["FullName"].ToString()
                            };

                            getFeedback.FeedbackId = Convert.ToInt32(reader["FeedbackId"]);
                            getFeedback.Comment = reader["Comment"].ToString();
                            getFeedback.Rating = Convert.ToInt32(reader["Rating"]);
                            getFeedback.BookId = Convert.ToInt32(reader["BookId"]);
                            getFeedback.User = user;
                            feedbackModel.Add(getFeedback);
                        }
                        sqlConnection.Close();
                        return feedbackModel;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public string UpdateFeedback(FeedbackModel feedbackModel, int feedbackId, int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("spUpdateFeedback", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Comment", feedbackModel.Comment);
                    cmd.Parameters.AddWithValue("@Rating", feedbackModel.Rating);
                    cmd.Parameters.AddWithValue("@BookId", feedbackModel.BookId);
                    cmd.Parameters.AddWithValue("@FeedbackId", feedbackId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    this.sqlConnection.Open();
                    int i = Convert.ToInt32(cmd.ExecuteScalar());
                    this.sqlConnection.Close();
                    if (i == 2)
                    {
                        return "Please Enter Correct Book Id";
                    }

                    if (i == 1)
                    {
                        return "Feedback is Already Present for This Book";
                    }
                    else
                    {
                        return "Feedback For This Book is Updated Successfully";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool DeleteFeedback(int feedbackId, int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("spDeleteFeedback", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FeedbackId", feedbackId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    sqlConnection.Open();
                    int i = cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    if (i >= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }  
            catch (Exception)
            {

                throw;
            }
        }
    }
}  