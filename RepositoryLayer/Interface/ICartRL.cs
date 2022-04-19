using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
   public interface ICartRL
    {
        public Cart AddCart(Cart cart, int userId);
        public List<CartModel> GetCartDetailsByUser(int userId);
        public Cart UpdateCart(Cart cartModel, int userId);
        public bool DeleteCart(int cartId, int userId);
    }
}
