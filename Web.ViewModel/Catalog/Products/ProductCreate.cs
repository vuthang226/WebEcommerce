using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.ViewModel.Catalog.Products
{
    public class ProductCreate
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int Price { set; get; }
        public int PriceSaleOff { set; get; }
        public int Stock { set; get; }
        public string Unit { get; set; }
        public string Description { set; get; }
        public string PhoneContaxt { get; set; }
        public List<IFormFile> FormImages { get; set; }
    }
}
