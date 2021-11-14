using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.ViewModel.Catalog.Carts;
using Web.ViewModel.Common;

namespace Web.Service.Catalog.Carts
{
    public interface ICartService
    {
        // Thêm vào giỏ hhang  , soluong/id sản phẩm/ 
        Task<ServiceResult> AddToCart(CartCreate request);

        Task UpdateCart(int quantity, Guid productId);

        Task<ServiceResult> GetById(Guid productId);

        Task<List<CartVm>> GetCartsViewModel();

        
    }
}
