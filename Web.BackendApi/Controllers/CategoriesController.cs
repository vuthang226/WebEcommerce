using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Service.Catalog.Categories;
using Web.ViewModel.Catalog.Categories;
using Web.ViewModel.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        public readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<ActionResult> GetAllCategoryCustomer()
        {
            try
            {
                return Ok(await _categoryService.GetAllCategoryCustomer());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("admin")]
        public async Task<ActionResult> GetAllCategoryAdmin()
        {
            try
            {
                return Ok(await _categoryService.GetAllCategoryAdmin());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public async Task<ServiceResult> Get(int id)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = await _categoryService.GetById(id);
            }
            catch (Exception e)
            {
                serviceResult.HandleException("Có lỗi xảy ra", e);
            }
            return serviceResult;
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public async Task<ServiceResult> CreateCategory([FromBody] CategoryCreate item)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = await _categoryService.CreateCategory(item);
            }
            catch (Exception e)
            {
                serviceResult.HandleException("Có lỗi xảy ra", e);
            }
            return serviceResult;
            
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public async Task<ServiceResult> UpdateCategory(int id, [FromBody] CategoryCreate item)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = await _categoryService.UpdateCategory(id,item);
            }
            catch (Exception e)
            {
                serviceResult.HandleException("Có lỗi xảy ra", e);
            }
            return serviceResult;
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public async Task<ServiceResult> DeleteAsync(int id)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = await _categoryService.DeleteCategory(id);
            }
            catch (Exception e)
            {
                serviceResult.HandleException("Có lỗi xảy ra", e);
            }
            return serviceResult;
        }
    }
}
