using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CartBL : ICartBL
    {
        private readonly ICartRL cartRL;
        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }

        public Cart AddCart(Cart cart, int userId)
        {
            try
            {
                return this.cartRL.AddCart(cart, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CartModel> GetCartDetailsByUser(int userId)
        {
            try
            {
                return this.cartRL.GetCartDetailsByUser(userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Cart UpdateCart(Cart cartModel, int userId)
        {
            try
            {
                return this.cartRL.UpdateCart(cartModel, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteCart(int cartId, int userId)
        {
            try
            {
                return this.cartRL.DeleteCart(cartId, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
