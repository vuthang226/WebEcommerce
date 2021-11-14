using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.ViewModel.Catalog.Products;
using Web.ViewModel.Common;

namespace Web.Service.Catalog.Products
{
    public interface IProductService
    {
        // trang chủ
        // Lấy sản phầm theo trang
        // lấy sản phẩm theo category
        // lấy sản phẩm theo Shop user id


        // quản lý
        // Thêm status
        // 

        // Lấy sp theo status

        // lấy sp theo id và hình ảnh theo id(tách ra cũng đc)

        // update sản phẩm và hình ảnh 

        // xóa sản phẩm

        // tạo sản phẩm


        Task<ServiceResult> Create(ProductCreate request);

        Task<ServiceResult> Update(ProductUpdate request);

        Task<ServiceResult> Delete(Guid productId);

        Task<ServiceResult> GetById(Guid productId);

        Task<ServiceResult> UpdatePrice(Guid productId, int newPrice);

        Task<ServiceResult> UpdateStock(Guid productId, int addedQuantity);

        Task<PagedResult<ProductVmp>> GetAllPaging(GetManageProductPagingRequest request);



    }
}
