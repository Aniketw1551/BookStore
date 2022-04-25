using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
   public class OrderModel
    {
        public int OrdersId { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public int BookId { get; set; }
        public int TotalPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime? OrderDate { get; set; }
        public BookModel BookModel { get; set; }
    }
}
