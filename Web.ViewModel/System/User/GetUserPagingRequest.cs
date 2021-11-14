using System;
using System.Collections.Generic;
using System.Text;
using Web.ViewModel.Common;

namespace Web.ViewModel.System.User
{
    public class GetUserPagingRequest: PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}
