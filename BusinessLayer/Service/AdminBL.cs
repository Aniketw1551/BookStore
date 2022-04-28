using BusinessLayer.Interface;
using CommonLayer.Accounts;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
   public class AdminBL : IAdminBL
    {
        private readonly IAdminRL adminRL;

        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        public AdminAccount AdminLogin(string email, string password)
        {
            try
            {
                return this.adminRL.AdminLogin(email, password);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
