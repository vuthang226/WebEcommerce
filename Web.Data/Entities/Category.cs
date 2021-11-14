using System;
using System.Collections.Generic;
using System.Text;
using Web.Data.Enum;

namespace Web.Data.Entities
{
    public class Category
    {
        public int Id { set; get; }
        public string Name { get; set; }
        public int SortOrder { set; get; }
        public bool IsShowOnHome { set; get; }
        public string Description { get; set; }
        public string CategoryUrl { get; set; }
        public Status Status { set; get; }
        public List<Product> Products { get; set; }

    }
}
