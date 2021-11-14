using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Data.Enum;

namespace Web.Data.Entities
{
    public class AppUser: IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public Status UserStatus { get; set; }
        public ShopStatus ShopStatus { get; set; }
        public string ShopName { get; set; }
        public string ShopAddress { get; set; }
        public string ShopDescription { get; set; }
        public string ShopPhone { get; set; }
        public string ShopNameContact { get; set; }
        public List<Cart> Carts { get; set; }
        public List<Order> Orders { get; set; }
        public List<Product> Products { get; set; }


    }
}
