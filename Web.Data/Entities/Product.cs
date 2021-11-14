using System;
using System.Collections.Generic;
using System.Text;
using Web.Data.Enum;

namespace Web.Data.Entities
{
    public class Product
    {
        public Guid Id { set; get; }
        public Guid UserId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int Price { set; get; }
        public int PriceSaleOff { set; get; }
        public int Stock { set; get; }
        public string Unit { get; set; }
        public string Description { set; get; }
        public DateTime DateCreated { set; get; }
        public DateTime DateUpdated { get; set; }
        public Status ProductStatus { get; set; }
        public string PhoneContaxt { get; set; }
        public AppUser AppUser { get; set; }
        public Category Category { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<Cart> Carts { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }
}
