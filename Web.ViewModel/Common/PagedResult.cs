using System.Collections.Generic;
using Web.ViewModel.Catalog.Products;

namespace Web.ViewModel.Common
{
    public class PagedResult<T>:PagedResultBase
    {
        public List<T> Items { get; set; }
    }
}