using System;
using System.Collections.Generic;
using System.Text;

namespace Web.ViewModel.Catalog.Categories
{
    public class CategoryCreate
    {
        public string Name { get; set; }
        public int SortOrder { set; get; }
        public bool IsShowOnHome { set; get; }
        public string Description { get; set; }
        public string CategoryUrl { get; set; }
    }
}
