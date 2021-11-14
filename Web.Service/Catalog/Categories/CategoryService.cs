using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data.EF;
using Web.Data.Entities;
using Web.ViewModel.Catalog.Categories;
using Web.ViewModel.Common;

namespace Web.Service.Catalog.Categories
{
    public class CategoryService:ICategoryService
    {
        private readonly WebDbContext _context;
        public CategoryService(WebDbContext context)
        {
            _context = context;
        }
        public async Task<ServiceResult> CreateCategory(CategoryCreate category) {
            ServiceResult serviceResult = new ServiceResult();
            var item = new Category
            {
                Name = category.Name,
                SortOrder = category.SortOrder,
                Description = category.Description,
                IsShowOnHome = category.IsShowOnHome,
                CategoryUrl = category.CategoryUrl,
            };
            await _context.Categories.AddAsync(item);
            serviceResult.OnSuccess(await _context.SaveChangesAsync(), "Thêm thành công");
            return serviceResult;

        }

        public async Task<ServiceResult> UpdateCategory(int Id, CategoryCreate category)
        {
            ServiceResult serviceResult = new ServiceResult();
            var item = await _context.Categories.FirstOrDefaultAsync(i => i.Id == Id);
            if (item != null)
            {
                item.Name = category.Name;
                item.SortOrder = category.SortOrder;
                item.Description = category.Description;
                item.IsShowOnHome = category.IsShowOnHome;
                item.CategoryUrl = category.CategoryUrl;
                serviceResult.OnSuccess(await _context.SaveChangesAsync(), "Cập nhật thành công");
            }
            else
            {
                serviceResult.OnError(0, "Không tìm thấy danh mục");
            }
            
            return serviceResult;

        }


        public async Task<List<CategoryVm>> GetAllCategoryCustomer()
        {
            var items = await _context.Categories.Where(x=>x.Status == Data.Enum.Status.Active).Select(x => new CategoryVm()
            {
                Id = x.Id,
                Name = x.Name,
                CategoryUrl = x.CategoryUrl,
            }).ToListAsync();
            return items;
        }

        public async Task<ServiceResult> GetById(int id)
        {
            ServiceResult serviceResult = new ServiceResult();
            var item = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (item != null)
            {
                serviceResult.OnSuccess(item);
            }
            else
            {
                serviceResult.OnError(0, "Không tìm thấy danh mục");
            }
            return serviceResult;
        }

        public async Task<List<CategoryVmAdmin>> GetAllCategoryAdmin()
        {
            var items = await _context.Categories.Select(x => new CategoryVmAdmin()
            {
                Id = x.Id,
                Name = x.Name,
                CategoryUrl = x.CategoryUrl,
                Status = x.Status,
                IsShowOnHome = x.IsShowOnHome,
            }).ToListAsync();
            return items;
        }

        public async Task<ServiceResult> DeleteCategory(int id)
        {
            ServiceResult serviceResult = new ServiceResult();
            var item = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (item != null)
            {
                _context.Categories.Remove(item);
                serviceResult.OnSuccess(1);
            }
            else
            {
                serviceResult.OnError(0, "Không tìm thấy danh mục");
            }
            return serviceResult;
        }
    }
}
