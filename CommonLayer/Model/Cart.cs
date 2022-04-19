using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class Cart
    {
        public int CartId { get; set; }
        public int BookId { get; set; }
        public int OrderQuantity { get; set; }
    }
}