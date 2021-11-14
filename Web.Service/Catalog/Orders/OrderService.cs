using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Web.Data.EF;
using Web.Data.Entities;
using Web.Data.Enum;
using Web.ViewModel.Catalog.Orders;
using Web.ViewModel.Common;

namespace Web.Service.Catalog.Orders
{
    public class OrderService : IOrderService
    {
        private readonly WebDbContext _context;
        private readonly HttpContext _httpContext;

        public OrderService(WebDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<ServiceResult> CreateOrder(OrderCreate request)
        {
            var userId = new Guid(_httpContext.User.FindFirstValue(ClaimTypes.Sid));
            var shipFee = 30000;
            //var query = from c in _context.Products
            //            join p in _context.Products on c.ProductId equals p.Id
            //            join pi in _context.ProductImages on p.Id equals pi.ProductId into ppi
            //            from pi in ppi.DefaultIfEmpty()
            //            join u in _context.Users on p.UserId equals u.Id
            //            where c.UserId == userId && pi.IsDefault == true
            //            select new { p, c, u, pi };
            var items = await _context.Carts.Where(i => i.UserId == userId && request.ProductIds.Contains(i.ProductId)).ToListAsync();
            Dictionary<Guid, Guid> exits = new Dictionary<Guid, Guid>();
            ServiceResult serviceResult = new ServiceResult();
            foreach (Cart item in items)
            {
                if (!exits.ContainsKey(item.Product.UserId))
                {
                    Guid orderId = Guid.NewGuid();
                    exits.Add(item.Product.UserId, orderId);
                    await _context.AddAsync(new Order
                    {
                        Id = orderId,
                        OrderDate = DateTime.Now,
                        UserId = userId,
                        ShopId = item.Product.UserId,
                        OrderNote = request.OrderNote,
                        ShipAddress = request.ShipAddress,
                        ShipEmail = request.ShipEmail,
                        ShipName = request.ShipName,
                        ShipFee = shipFee,
                        ShipPhoneNumber = request.ShipPhoneNumber,
                        OrderStatus = OrderStatus.NewOrder,
                        Amount = shipFee + item.Quantity * (item.Product.PriceSaleOff > -1 ? item.Product.PriceSaleOff : item.Product.Price)
                    });
                    await _context.AddAsync(new OrderDetail
                    {
                        ProductId = item.ProductId,
                        OrderId = orderId,
                        Quantity = item.Quantity,
                        Price = item.Quantity * (item.Product.PriceSaleOff > -1 ? item.Product.PriceSaleOff : item.Product.Price),
                    });
                    //cần save cho này
                }
                else
                {
                    Guid orderId;
                    if (exits.TryGetValue(item.Product.UserId, out orderId))
                    {
                        await _context.AddAsync(new OrderDetail
                        {
                            ProductId = item.ProductId,
                            OrderId = orderId,
                            Quantity = item.Quantity,
                            Price = item.Quantity * (item.Product.PriceSaleOff > -1 ? item.Product.PriceSaleOff : item.Product.Price),
                        });
                        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
                        order.Amount += item.Quantity * (item.Product.PriceSaleOff > -1 ? item.Product.PriceSaleOff : item.Product.Price);
                    }


                }
            }
            serviceResult.OnSuccess(await _context.SaveChangesAsync(), "Đặt hàng thành công");
            return serviceResult;
        }

        public async Task<ServiceResult> ShowOrderCustomer()
        {
            var userId = new Guid(_httpContext.User.FindFirstValue(ClaimTypes.Sid));
            var items = await _context.Orders.Where(x => x.UserId == userId).ToListAsync();
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.OnSuccess(items);
            return serviceResult;
        }
        public async Task<ServiceResult> ShowOrderShop()
        {
            var userId = new Guid(_httpContext.User.FindFirstValue(ClaimTypes.Sid));
            var items = await _context.Orders.Where(x => x.ShopId == userId).ToListAsync();
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.OnSuccess(items);
            return serviceResult;
        }
        public async Task<ServiceResult> ChangeStatusOrder()
        {
            ServiceResult serviceResult = new ServiceResult();
            
            return serviceResult;
        }
    }
}
