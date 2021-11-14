using System;
using System.Collections.Generic;
using System.Text;

namespace Web.ViewModel.Catalog.Products
{
    public class ProductVmp
    {
        public Guid Id { set; get; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Name { get; set; }
        public int Price { set; get; }
        public int PriceSaleOff { set; get; }
        public string ImagePath { get; set; }
    }
}
