using System;
using System.Collections.Generic;
using System.Text;
using Web.Data.Entities;
using Web.Data.Enum;
using Web.ViewModel.Catalog.ProductImages;

namespace Web.ViewModel.Catalog.Products
{
    public class ProductVm
    {
        public Product Product { set; get; }
        public List<ProductImageVm> Images { get; set; }  
    }
}
