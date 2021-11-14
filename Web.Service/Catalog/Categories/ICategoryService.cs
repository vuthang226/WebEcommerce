using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Data.Entities;
using Web.ViewModel.Catalog.Categories;
using Web.ViewModel.Common;

namespace Web.Service.Catalog.Categories
{
    public interface ICategoryService
    {
        /// <summary>
        /// Tạo Category mới
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        Task<ServiceResult> CreateCategory(CategoryCreate category); 
        /// <summary>
        /// Update Category
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        Task<ServiceResult> UpdateCategory(int Id, CategoryCreate category);
        /// <summary>
        /// Lấy list danh sách category cho người dùng
        /// </summary>
        /// <returns></returns>
        Task<List<CategoryVm>> GetAllCategoryCustomer();
        /// <summary>
        /// Lấy list dánh sách category cho admin thêm các trường như hiển thị trang chủ/active
        /// </summary>
        /// <returns></returns>
        Task<List<CategoryVmAdmin>> GetAllCategoryAdmin();
        /// <summary>
        /// Lấy chi tiết 1 danh mục theo id
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult> GetById(int id);
        /// <summary>
        /// Xóa 1 dm theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ServiceResult> DeleteCategory(int id);

        
    }
}
