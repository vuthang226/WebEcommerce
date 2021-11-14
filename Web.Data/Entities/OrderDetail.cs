using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Data.Entities
{
    public class OrderDetail
    {
        public Guid OrderId { set; get; }
        public Guid ProductId { set; get; }
        public int Quantity { set; get; }
        public int Price { set; get; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
