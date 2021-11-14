using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Data.Enum
{
    public enum OrderStatus
    {
        NewOrder,
        WaitingDelivery,
        Delivering,
        Delivered,
        Finished,
        Cancelled
    }
}
