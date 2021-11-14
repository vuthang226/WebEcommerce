using System;
using System.Collections.Generic;
using System.Text;

namespace Web.ViewModel.Catalog.Orders
{
    public class OrderCreate
    {
        public List<Guid> ProductIds { get; set; }
        public string ShipName { set; get; }
        public string ShipAddress { set; get; }
        public string ShipEmail { set; get; }
        public string ShipPhoneNumber { set; get; }
        public string OrderNote { get; set; }
    }
}
