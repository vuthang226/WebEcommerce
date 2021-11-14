using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Data.Entities
{
    public class Cart
    {
        public Guid ProductId { set; get; }
        public Guid UserId { get; set; }
        public int Quantity { set; get; }
        public DateTime DateCreated { get; set; }
        public AppUser AppUser { get; set; }
        public Product Product { get; set; }

        
    }
}
