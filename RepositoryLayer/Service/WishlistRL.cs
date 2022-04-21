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
    public class WishlistRL : IWishlistRL
    {
        private SqlConnection sqlConnection;

        private IConfiguration Configuration { get; }

        public WishlistRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public string AddWishlist(int bookId, int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("spAddWishlist", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    sqlConnection.Open();
                    int i = Convert.ToInt32(cmd.ExecuteScalar());
                    sqlConnection.Close();
                    if (i == 2)
                    {
                        return "Book is Already in Wishlist";
                    }

                    if (i == 1)
                    {
                        return "Please Enter Correct BookId";
                    }
                    else
                    {
                        return "Book is Successfully Added in Wishlist";
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

        public List<WishlistModel> GetAllEntriesFromWishlist(int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("spGetAllEntriesFromWishlist", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        List<WishlistModel> wishlistModel = new List<WishlistModel>();
                        while (reader.Read())
                        {
                            BookModel bookModel = new BookModel();
                            WishlistModel wishlist = new WishlistModel();
                            bookModel.BookName = reader["BookName"].ToString();
                            bookModel.AuthorName = reader["AuthorName"].ToString();
                            bookModel.OriginalPrice = Convert.ToInt32(reader["OriginalPrice"]);
                            bookModel.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"]);
                            bookModel.BookImage = reader["BookImage"].ToString();
                            wishlist.WishlistId = Convert.ToInt32(reader["WishlistId"]);
                            wishlist.UserId = Convert.ToInt32(reader["UserId"]);
                            wishlist.BookId = Convert.ToInt32(reader["BookId"]);
                            wishlist.Bookmodel = bookModel;
                            wishlistModel.Add(wishlist);
                        }

                        sqlConnection.Close();
                        return wishlistModel;
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

        public bool DeleteWishlist(int wishlistId, int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("spDeleteWishlist", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@WishlistId", wishlistId);
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