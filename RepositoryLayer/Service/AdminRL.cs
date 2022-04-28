using CommonLayer.Accounts;
using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class AdminRL : IAdminRL
    {
        private SqlConnection sqlConnection;

        private IConfiguration Configuration { get; }

        public AdminRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public AdminAccount AdminLogin(string email, string password)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("spAdminLogin", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);
                    this.sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    AdminAccount adminAccount = new AdminAccount();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            adminAccount.AdminId = Convert.ToInt32(reader["AdminId"] == DBNull.Value ? default : reader["AdminId"]);
                            adminAccount.FullName = Convert.ToString(reader["FullName"] == DBNull.Value ? default : reader["FullName"]);
                            adminAccount.Email = Convert.ToString(reader["Email"] == DBNull.Value ? default : reader["Email"]);
                            adminAccount.PhoneNumber = Convert.ToString(reader["PhoneNumber"] == DBNull.Value ? default : reader["PhoneNumber"]);
                        }
                        this.sqlConnection.Close();
                        adminAccount.Token = this.GenerateJWTToken(adminAccount);
                        return adminAccount;
                    }
                    else
                    {
                        throw new Exception("Email Or Password Is Incorrect");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GenerateJWTToken(AdminAccount adminAccount)
        {
            // header
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // payload
            var claims = new[]
            {
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim("Email", adminAccount.Email),
                new Claim("AdminId", adminAccount.AdminId.ToString()),
            };

            // signature
            var token = new JwtSecurityToken(
                this.Configuration["Jwt:Issuer"],
                this.Configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
            