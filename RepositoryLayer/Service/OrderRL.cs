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
    public class OrderRL : IOrderRL
    {
        private SqlConnection sqlConnection;

        private IConfiguration Configuration { get; }

        public OrderRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public AddOrderModel AddOrder(AddOrderModel orderModel, int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("spAddOrder", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrderBookQuantity", orderModel.Quantity);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@BookId", orderModel.BookId);
                    cmd.Parameters.AddWithValue("@AddressId", orderModel.AddressId);
                    sqlConnection.Open();
                    int i = Convert.ToInt32(cmd.ExecuteScalar());
                    sqlConnection.Close();
                    if (i == 3)
                    {
                        return null;
                    }

                    if (i == 2)
                    {
                        return null;
                    }
                    else
                    {
                        return orderModel;
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

        public List<OrderModel> GetAllOrders(int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("spGetOrders", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        List<OrderModel> order = new List<OrderModel>();
                        while (reader.Read())
                        {
                            OrderModel orderModel = new OrderModel();
                            BookModel bookModel = new BookModel();
                            orderModel.OrdersId = Convert.ToInt32(reader["OrdersId"]);
                            orderModel.UserId = Convert.ToInt32(reader["UserId"]);
                            orderModel.BookId = Convert.ToInt32(reader["BookId"]);
                            orderModel.AddressId = Convert.ToInt32(reader["AddressId"]);
                            orderModel.TotalPrice = Convert.ToInt32(reader["TotalPrice"]);
                            orderModel.Quantity = Convert.ToInt32(reader["OrderBookQuantity"]);
                            orderModel.OrderDate = Convert.ToDateTime(reader["OrderDate"]);
                            bookModel.BookName = reader["BookName"].ToString();
                            bookModel.AuthorName = reader["AuthorName"].ToString();
                            bookModel.BookImage = reader["BookImage"].ToString();
                            orderModel.BookModel = bookModel;
                            order.Add(orderModel);
                        }

                        sqlConnection.Close();
                        return order;
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
    }
}