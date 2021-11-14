using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.ViewModel.Catalog.ProductImages
{
    public class ProductImageVm
    {
        public bool IsDefault { get; set; }

        public string ImagePath { get; set; }
    }
}
