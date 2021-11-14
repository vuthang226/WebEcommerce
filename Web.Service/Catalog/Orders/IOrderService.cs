using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.ViewModel.Catalog.Orders;
using Web.ViewModel.Common;

namespace Web.Service.Catalog.Orders
{
    public interface IOrderService 
    {
        Task<ServiceResult> CreateOrder(OrderCreate request);
        Task<ServiceResult> ShowOrderCustomer();
        Task<ServiceResult> ShowOrderShop();
    }
}
