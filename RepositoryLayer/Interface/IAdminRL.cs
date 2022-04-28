using CommonLayer.Accounts;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
   public interface IAdminRL
    {
        public AdminAccount AdminLogin(string email, string password);
    }
}
