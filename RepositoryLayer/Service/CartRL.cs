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
    public class CartRL : ICartRL
    {
        private SqlConnection sqlConnection;

        private IConfiguration Configuration { get; }

        public CartRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public Cart AddCart(Cart cart, int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("spAddCart", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrderQuantity", cart.OrderQuantity);
                    cmd.Parameters.AddWithValue("@BookId", cart.BookId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    sqlConnection.Open();
                    int i = cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    if (i >= 1)
                    {
                        return cart;
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

        public List<CartModel> GetCartDetailsByUser(int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("spGetCartByUser", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        List<CartModel> cartmodel = new List<CartModel>();
                        while (reader.Read())
                        {
                            BookModel bookModel = new BookModel();
                            CartModel cartModel = new CartModel();

                            bookModel.BookName = reader["BookName"].ToString();
                            bookModel.AuthorName = reader["AuthorName"].ToString();
                            bookModel.OriginalPrice = Convert.ToInt32(reader["OriginalPrice"]);
                            bookModel.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"]);
                            bookModel.BookImage = reader["BookImage"].ToString();
                            cartModel.UserId = Convert.ToInt32(reader["UserId"]);
                            cartModel.BookId = Convert.ToInt32(reader["BookId"]);
                            cartModel.CartId = Convert.ToInt32(reader["CartId"]);
                            cartModel.OrderQuantity = Convert.ToInt32(reader["OrderQuantity"]);
                            cartModel.Bookmodel = bookModel;
                            cartmodel.Add(cartModel);
                        }

                        sqlConnection.Close();
                        return cartmodel;
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

        public Cart UpdateCart(Cart cartModel, int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("spUpdateCart", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrderQuantity", cartModel.OrderQuantity);
                    cmd.Parameters.AddWithValue("@BookId", cartModel.BookId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@CartId", cartModel.CartId);
                    sqlConnection.Open();
                    int i = cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    if (i >= 1)
                    {
                        return cartModel;
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

        public bool DeleteCart(int cartId, int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));

            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("spDeleteCart", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CartId", cartId);
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
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}