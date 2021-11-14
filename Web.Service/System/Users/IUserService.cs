using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.ViewModel.Common;
using Web.ViewModel.System.Role;
using Web.ViewModel.System.User;

namespace Web.Service.System.Users
{
    public interface IUserService
    {
        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ServiceResult> Authencate(LoginRequest request);
        /// <summary>
        /// Đang kí tải khoản mua hàng
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ServiceResult> Register(RegisterRequest request);
        /// <summary>
        /// Update thông tin người dùng
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>        
        Task<ServiceResult> Update(Guid id, UserVm request);
        /// <summary>
        /// Lấy ra danh sách người dùng theo trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>        
        Task<ServiceResult> GetUsersPaging(GetUserPagingRequest request);
        /// <summary>
        /// Lấy chi tiết thông tin tk theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        Task<ServiceResult> GetById(Guid id);
        /// <summary>
        /// xóa 1 tk
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        Task<ServiceResult> Delete(Guid id);
        /// <summary>
        /// Gán quyền
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>        
        Task<ServiceResult> RoleAssign(Guid id, List<RoleAssignRequest> request);
        /// <summary>
        /// Đăng kí gian hàng
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ServiceResult> RegisterShop(ShopRequest request);
    }
}
