using eShopSolution.Application.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Web.Data.EF;
using Web.Data.Entities;
using Web.Data.Enum;
using Web.ViewModel.Catalog.ProductImages;
using Web.ViewModel.Catalog.Products;
using Web.ViewModel.Common;

namespace Web.Service.Catalog.Products
{
    public class ProductService: IProductService
    {
        private readonly WebDbContext _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        private readonly HttpContext _httpContext;

        public ProductService(WebDbContext context, IStorageService storageService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _storageService = storageService;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<ServiceResult> Create(ProductCreate request)
        {
            var userId = new Guid(_httpContext.User.FindFirstValue(ClaimTypes.Sid));
            var product = new Product()
            {
                UserId = userId,
                Name = request.Name,
                CategoryId = request.CategoryId,
                Price = request.Price,
                PriceSaleOff = request.PriceSaleOff,
                Stock = request.Stock,
                Unit = request.Unit,
                Description = request.Description,
                PhoneContaxt = request.PhoneContaxt,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                ProductStatus = Status.InActive
            };
            //Save image
            if (request.FormImages != null)
            {
                List<ProductImage> listsImage = new List<ProductImage>();
                bool IsDefault = true;
                foreach (IFormFile image in request.FormImages)
                {
                    listsImage.Add(new ProductImage()
                    {
                        FileSize = image.Length,
                        ImagePath = await this.SaveFile(image),
                        IsDefault = IsDefault,
                    });
                    if (IsDefault) IsDefault = false;
                }
                product.ProductImages = listsImage;
            }
            await _context.Products.AddAsync(product);
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.OnSuccess(await _context.SaveChangesAsync(),"Thêm thành công");
            return serviceResult;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }

        public async Task<ServiceResult> Delete(Guid productId)
        {
            var userId = new Guid(_httpContext.User.FindFirstValue(ClaimTypes.Sid));
            ServiceResult serviceResult = new ServiceResult();
            var item = await _context.Products.FirstOrDefaultAsync(i => i.Id == productId && i.UserId == userId);
            if (item != null)
            {
                var images = _context.ProductImages.Where(i => i.ProductId == productId);
                foreach (var image in images)
                {
                    await _storageService.DeleteFileAsync(image.ImagePath);
                }
                _context.Remove(item);
                serviceResult.OnSuccess(await _context.SaveChangesAsync(),"Xóa thành công");
            }
            else
            {
                serviceResult.OnError(0,$"Không tìm thấy sản phẩm: {productId}");
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult> GetById(Guid productId)
        {
            ServiceResult serviceResult = new ServiceResult();
            var item = await _context.Products.FirstOrDefaultAsync(i => i.Id == productId);
            if (item != null)
            {
                var images = await _context.ProductImages.Where(x => x.ProductId == productId).Select(x => new ProductImageVm()
                {
                    ImagePath = x.ImagePath,
                    IsDefault = x.IsDefault,
                }).ToListAsync();
               
                serviceResult.OnSuccess(new ProductVm { 
                    Product = item,
                    Images = images,
                });
            }
            else
            {
                serviceResult.OnError(0, $"Không tìm thấy sản phẩm: {productId}");
            }

            return serviceResult;
        }

        public async Task<ServiceResult> Update(ProductUpdate request)
        {
            var userId = new Guid(_httpContext.User.FindFirstValue(ClaimTypes.Sid));
            ServiceResult serviceResult = new ServiceResult();
            var item = await _context.Products.FirstOrDefaultAsync(i => i.Id == request.Id && i.UserId == userId); 
            if(item != null)
            {
                item.CategoryId = request.CategoryId;
                item.Price = request.Price;
                item.PriceSaleOff = request.PriceSaleOff;
                item.ProductStatus = Status.InActive;
                item.Stock = request.Stock;
                item.PhoneContaxt = request.PhoneContaxt;
                item.Unit = request.Unit;
                item.DateUpdated = DateTime.Now;
                item.Name = request.Name;
                item.Description = request.Description;
                item.PhoneContaxt = request.PhoneContaxt;

                if (request.FormImages != null)
                {
                    var images = _context.ProductImages.Where(i => i.ProductId == request.Id);
                    foreach (var image in images)
                    {
                        await _storageService.DeleteFileAsync(image.ImagePath);
                    }
                    List<ProductImage> listsImage = new List<ProductImage>();
                    bool IsDefault = true;
                    foreach (IFormFile image in request.FormImages)
                    {
                        listsImage.Add(new ProductImage()
                        {
                            FileSize = image.Length,
                            ImagePath = await this.SaveFile(image),
                            IsDefault = IsDefault,
                        });
                        if (IsDefault) IsDefault = false;
                    }
                    item.ProductImages = listsImage;
                }
                serviceResult.OnSuccess(await _context.SaveChangesAsync(), "Cập nhật thành công");
            }
            else
            {
                serviceResult.OnError(0, "Không tìm thấy sản phẩm");
            }
            return serviceResult;
        }

        public Task<ServiceResult> UpdatePrice(Guid productId, int newPrice)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> UpdateStock(Guid productId, int addedQuantity)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<ProductVmp>> GetAllPaging(GetManageProductPagingRequest request)
        {

            var query = from p in _context.Products
                        join pi in _context.ProductImages on p.Id equals pi.ProductId into ppi
                        from pi in ppi.DefaultIfEmpty()
                        join c in _context.Categories on p.CategoryId equals c.Id
                        where pi.IsDefault == true
                        select new{ p,pi,c};


            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.p.Name.Contains(request.Keyword));

            if (request.CategoryId != null)
            {
                query = query.Where(p => p.p.CategoryId == request.CategoryId);
            }
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductVmp()
                {
                    Id = x.p.Id,
                    CategoryId = x.p.CategoryId,
                    CategoryName = x.c.Name,
                    Name = x.p.Name,
                    Price = x.p.Price,
                    PriceSaleOff = x.p.PriceSaleOff,
                    ImagePath = x.pi.ImagePath
                }).ToListAsync();

            var pagedResult = new PagedResult<ProductVmp>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }
    }
}
