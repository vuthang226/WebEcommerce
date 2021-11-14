using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Service.Catalog.Products;
using Web.ViewModel.Catalog.Products;
using Web.ViewModel.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {

        public readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ServiceResult> Get(Guid id)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = await _productService.GetById(id);
            }
            catch (Exception e)
            {
                serviceResult.HandleException("Có lỗi xảy ra",e);
            }
            return serviceResult;
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task<ServiceResult> Post([FromForm] ProductCreate request)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = await _productService.Create(request);
            }
            catch (Exception e)
            {
                serviceResult.HandleException("Có lỗi xảy ra", e);
            }
            return serviceResult;
        }

        // PUT api/<ProductsController>/5
        [HttpPut]
        public async Task<ServiceResult> Put( [FromForm] ProductUpdate request)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = await _productService.Update(request);
            }
            catch (Exception e)
            {
                serviceResult.HandleException("Có lỗi xảy ra", e);
            }
            return serviceResult;
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public async Task<ServiceResult> Delete(Guid id)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = await _productService.Delete(id);
            }
            catch (Exception e)
            {
                serviceResult.HandleException("Có lỗi xảy ra", e);
            }
            return serviceResult;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<PagedResult<ProductVmp>> GetAllPaging([FromQuery]GetManageProductPagingRequest request)
        {
            
            try
            {
                return await _productService.GetAllPaging(request);
            }
            catch 
            {
                return new PagedResult<ProductVmp>();
            }
            
        }
    }
}
