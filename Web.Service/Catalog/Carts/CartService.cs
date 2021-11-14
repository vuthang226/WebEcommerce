using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Web.Data.EF;
using Web.Data.Entities;
using Web.ViewModel.Catalog.Carts;
using Web.ViewModel.Common;
using System.Linq;

namespace Web.Service.Catalog.Carts
{
    public class CartService: ICartService
    {
        private readonly WebDbContext _context;
        private readonly HttpContext _httpContext;

        public CartService(WebDbContext context,  IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContext = httpContextAccessor.HttpContext;
        }
        public async Task<ServiceResult> AddToCart(CartCreate request)
        {
            var userId = new Guid(_httpContext.User.FindFirstValue(ClaimTypes.Sid));
            var item = await _context.Carts.FirstOrDefaultAsync(i => i.ProductId == request.ProductId && i.UserId == userId);
            if(item == null)
            {
                var cart = new Cart()
                {
                    UserId = userId,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    DateCreated = DateTime.Now,
                };
                await _context.Carts.AddAsync(cart);
            }
            else
            {
                item.Quantity += request.Quantity;
            }       
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.OnSuccess(await _context.SaveChangesAsync(), "Sản phẩm đã được thêm vào Giỏ hàng");
            return serviceResult;
        }

        public Task<ServiceResult> GetById(Guid productId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CartVm>> GetCartsViewModel()
        {
            var userId = new Guid(_httpContext.User.FindFirstValue(ClaimTypes.Sid));
            var query = from c in _context.Carts
                        join p in _context.Products on c.ProductId equals p.Id
                        join pi in _context.ProductImages on p.Id equals pi.ProductId into ppi
                        from pi in ppi.DefaultIfEmpty()
                        join u in _context.Users on p.UserId equals u.Id
                        where c.UserId == userId && pi.IsDefault == true
                        select new { p, c, u, pi };
            var items = await query.Select(x => new CartVm()
                {
                    ProductId = x.c.ProductId,
                    ProductName = x.p.Name,
                    PriceOriginal = x.p.Price,
                    PriceSale = x.p.PriceSaleOff,
                    ImagePath = x.pi.ImagePath,
                    ShopName = x.u.ShopName,
                    ShopId = x.u.Id,
                    Quantity = x.c.Quantity
                }).ToListAsync();

            return items;
        }

        public async Task UpdateCart(int quantity, Guid productId)
        {
            var userId = new Guid(_httpContext.User.FindFirstValue(ClaimTypes.Sid));
            var item = await _context.Carts.FirstOrDefaultAsync(i => i.ProductId == productId && i.UserId == userId);
            if (item != null)
            {
                if (quantity > 0)
                {
                    item.Quantity = quantity;
                }
                else
                {
                    _context.Carts.Remove(item);
                }
                await _context.SaveChangesAsync();
            }   
        }

        //public CheckoutViewModel GetCheckoutViewModel()
        //{
        //    var session = HttpContext.Session.GetString(SystemConstants.CartSession);
        //    List<CartItemViewModel> currentCart = new List<CartItemViewModel>();
        //    if (session != null)
        //        currentCart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);
        //    var checkoutVm = new CheckoutViewModel()
        //    {
        //        CartItems = currentCart,
        //        CheckoutModel = new CheckoutRequest()
        //    };
        //    return checkoutVm;
        //}
    }
}
