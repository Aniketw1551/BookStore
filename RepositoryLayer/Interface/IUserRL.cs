using CommonLayer.Accounts;
using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
   public interface IUserRL
    {
        public UserModel UserRegister(UserModel userModel);
        public UserAccount Login(string email, string password);
        public string ForgotPassword(string email);
        public bool ResetPassword(string email, string newPassword, string confirmPassword);
    }
}
