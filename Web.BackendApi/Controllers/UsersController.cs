using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Service.System.Users;
using Web.ViewModel.Common;
using Web.ViewModel.System.User;

namespace Web.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<ServiceResult> Authenticate([FromBody] LoginRequest request)
        {
            ServiceResult serviceResult = new ServiceResult();
            if (!ModelState.IsValid)
            {
                serviceResult.OnError(ModelState);
                return serviceResult;
            }
            
            try
            {
                serviceResult = await _userService.Authencate(request);
            }
            catch (Exception e)
            {
                serviceResult.HandleException("Có lỗi xảy ra", e);
            }
            return serviceResult;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ServiceResult> Register([FromBody] RegisterRequest request)
        {
            ServiceResult serviceResult = new ServiceResult();
            if (!ModelState.IsValid)
            {
                serviceResult.OnError(ModelState);
                return serviceResult;
            }

            try
            {
                serviceResult = await _userService.Register(request);
            }
            catch (Exception e)
            {
                serviceResult.HandleException("Có lỗi xảy ra", e);
            }
            return serviceResult;
        }

        //PUT: http://localhost/api/users/id
        [HttpPut("{id}")]
        public async Task<ServiceResult> Update(Guid id, [FromBody] UserVm request)
        {
            ServiceResult serviceResult = new ServiceResult();
            if (!ModelState.IsValid)
            {
                serviceResult.OnError(ModelState);
                return serviceResult;
            }

            try
            {
                serviceResult = await _userService.Update(id,request);
            }
            catch (Exception e)
            {
                serviceResult.HandleException("Có lỗi xảy ra", e);
            }
            return serviceResult;
        }

        //[HttpPut("{id}/roles")]
        //public async Task<IActionResult> RoleAssign(Guid id, [FromBody] RoleAssignRequest request)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var result = await _userService.RoleAssign(id, request);
        //    if (!result.IsSuccessed)
        //    {
        //        return BadRequest(result);
        //    }
        //    return Ok(result);
        //}

        //http://localhost/api/users/paging?pageIndex=1&pageSize=10&keyword=
        [HttpGet("paging")]
        public async Task<ServiceResult> GetAllPaging([FromQuery] GetUserPagingRequest request)
        {
            ServiceResult serviceResult = new ServiceResult();

            try
            {
                serviceResult = await _userService.GetUsersPaging(request); 
            }
            catch (Exception e)
            {
                serviceResult.HandleException("Có lỗi xảy ra", e);
            }
            return serviceResult;
            
           
        }

        [HttpGet("{id}")]
        public async Task<ServiceResult> GetById(Guid id)
        {
            ServiceResult serviceResult = new ServiceResult();

            try
            {
                serviceResult = await _userService.GetById(id);
            }
            catch (Exception e)
            {
                serviceResult.HandleException("Có lỗi xảy ra", e);
            }
            return serviceResult;
        }

        [HttpDelete("{id}")]
        public async Task<ServiceResult> Delete(Guid id)
        {
            ServiceResult serviceResult = new ServiceResult();

            try
            {
                serviceResult = await _userService.Delete(id);
            }
            catch (Exception e)
            {
                serviceResult.HandleException("Có lỗi xảy ra", e);
            }
            return serviceResult;
        }
    }
}
