using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
   public interface IWishlistBL
    {
        public string AddWishlist(int bookId, int userId);
        public List<WishlistModel> GetAllEntriesFromWishlist(int userId);
        public bool DeleteWishlist(int wishlistId, int userId);
    }
}
