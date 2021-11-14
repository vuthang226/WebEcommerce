using System;
using System.Collections.Generic;
using System.Text;

namespace Web.ViewModel.Catalog.Carts
{
    public class CartCreate
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
