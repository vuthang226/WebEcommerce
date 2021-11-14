using System;
using System.Collections.Generic;
using System.Text;
using Web.Data.Enum;
using Web.ViewModel.Common;

namespace Web.ViewModel.Catalog.Products
{
    public class GetManageProductPagingRequest:PagingRequestBase
    {
        public string Keyword { get; set; }

        public int? CategoryId { get; set; }

        public Guid? UserId { get; set; }

        public Status? StatusProduct { get; set; }
    }
}
