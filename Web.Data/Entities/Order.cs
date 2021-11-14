using System;
using System.Collections.Generic;
using System.Text;
using Web.Data.Enum;

namespace Web.Data.Entities
{
    public class Order
    {
        public Guid Id { set; get; }
        public DateTime OrderDate { set; get; }
        public Guid UserId { set; get; }
        public Guid ShopId { set; get; }
        public string OrderNote { get; set; }
        public string ShipName { set; get; }
        public string ShipAddress { set; get; }
        public string ShipEmail { set; get; }
        public string ShipPhoneNumber { set; get; }
        public int ShipFee { get; set; }
        public OrderStatus OrderStatus { set; get; }
        
        public int Amount { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
        public AppUser AppUser { get; set; }



    }
}
