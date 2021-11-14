using System;
using System.Collections.Generic;
using System.Text;
using Web.ViewModel.Common;

namespace Web.ViewModel.Catalog.Products
{
    public class GetPublicProductPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }

        public Guid? CategoryId { get; set; }

        public Guid? UserId { get; set; }
    }
}
