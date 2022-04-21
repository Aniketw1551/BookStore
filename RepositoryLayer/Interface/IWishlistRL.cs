using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
   public interface IWishlistRL
    {
        public string AddWishlist(int bookId, int userId);
        public List<WishlistModel> GetAllEntriesFromWishlist(int userId);
        public bool DeleteWishlist(int wishlistId, int userId);
    }
}
