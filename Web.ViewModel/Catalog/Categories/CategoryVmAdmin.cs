using System;
using System.Collections.Generic;
using System.Text;
using Web.Data.Enum;

namespace Web.ViewModel.Catalog.Categories
{
    public class CategoryVmAdmin
    {
        public int Id { set; get; }
        public string Name { get; set; }
        public string CategoryUrl { get; set; }
        public Status Status { set; get; }
        public bool IsShowOnHome { set; get; }
    }
}
