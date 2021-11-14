using System;
using System.Collections.Generic;
using System.Text;

namespace Web.ViewModel.Catalog.Carts
{
    public class CartVm
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public Guid ShopId { get; set; }
        public string ShopName { get; set; }
        public int PriceOriginal { get; set; }
        public int PriceSale { get; set; }
        public int Quantity { get; set; }
        public string ImagePath { get; set; }
    }
}
