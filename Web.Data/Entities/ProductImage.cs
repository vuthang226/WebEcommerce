using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Data.Entities
{
    public class ProductImage
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string ImagePath { get; set; }

        public bool IsDefault { get; set; }

        public long FileSize { get; set; }

        public Product Product { get; set; }
    }
}
