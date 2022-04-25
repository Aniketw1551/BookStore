using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
   public interface IOrderBL
    {
        public AddOrderModel AddOrder(AddOrderModel orderModel, int userId);
        public List<OrderModel> GetAllOrders(int userId);
    }
}
