using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Web.Data.Entities;
using Web.Data.Enum;
using Web.ViewModel.Common;
using Web.ViewModel.System.Role;
using Web.ViewModel.System.User;

namespace Web.Service.System.Users
{
    public class UserService: IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;

        public UserService(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager,
            IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
        }

        public async Task<ServiceResult> Authencate(LoginRequest request)
        {
            ServiceResult serviceResult = new ServiceResult();
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) {
                serviceResult.OnError(0, "Tài khoản hoặc mật khẩu không chính xác");
                return serviceResult;
            }
            
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);

            if (!result.Succeeded)
            {
                serviceResult.OnError(0, "Tài khoản hoặc mật khẩu không chính xác");
                return serviceResult;
            }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Sid,user.Id.ToString()),
                new Claim(ClaimTypes.GivenName,user.FullName),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, request.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            serviceResult.OnSuccess(new JwtSecurityTokenHandler().WriteToken(token),"Đăng nhập thành công");
            return serviceResult;
        }

        public async Task<ServiceResult> Delete(Guid id)
        {
            ServiceResult serviceResult = new ServiceResult();

            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                serviceResult.OnError(0, "Tài khoản không tồn tại");
                return serviceResult;
            }
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                serviceResult.OnError(0, "Xóa không thành công");
                return serviceResult;
            }
            serviceResult.OnError(0, "Xóa thành công");

            return serviceResult;
        }

        public async Task<ServiceResult> GetById(Guid id)
        {
            ServiceResult serviceResult = new ServiceResult();
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                serviceResult.OnError(0, "Tài khoản không tồn tại");
                return serviceResult;
            }
            var roles = await _userManager.GetRolesAsync(user);
            var userVm = new UserVm()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                Id = user.Id,
                UserName = user.UserName,
                Roles = roles
            };
            serviceResult.OnSuccess(userVm);
            return serviceResult;
        }

        public async Task<ServiceResult> GetUsersPaging(GetUserPagingRequest request)
        {
            ServiceResult serviceResult = new ServiceResult();
            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.UserName.Contains(request.Keyword)
                 || x.PhoneNumber.Contains(request.Keyword) || x.Email.Contains(request.Keyword));
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new UserVm()
                {
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    UserName = x.UserName,
                    FullName = x.FullName,
                    Id = x.Id,
                }).ToListAsync();

            //4. Select and projection
            var pagedResultBase = new PagedResultBase()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            };
            //xem lai
            serviceResult.OnSuccess(new { pagedResultBase , data });
            return serviceResult;
            
        }

        public async Task<ServiceResult> Register(RegisterRequest request)
        {
            ServiceResult serviceResult = new ServiceResult();
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                serviceResult.OnError(0, "Tên tài khoản đã tồn tại");
                return serviceResult;
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                serviceResult.OnError(0, "Email đã tồn tại");
                return serviceResult;
            }

            user = new AppUser()
            {
                Email = request.Email,
                FullName = request.FullName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                ShopStatus = ShopStatus.UnRegistered,
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                serviceResult.OnError(0, "Tạo tài khoản không thành công");
                return serviceResult;
            }
            serviceResult.OnSuccess(1);
            return serviceResult;
        }

        public Task<ServiceResult> RegisterShop(ShopRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult> RoleAssign(Guid id, List<RoleAssignRequest> request)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            ServiceResult serviceResult = new ServiceResult();
            if (user == null)
            {
                serviceResult.OnError(0, "Tài khoản không tồn tại");
                return serviceResult;
            }
            var removedRoles = request.Where(x => x.Selected == false).Select(x => x.Name).ToList();
            foreach (var roleName in removedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == true)
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }
            await _userManager.RemoveFromRolesAsync(user, removedRoles);

            var addedRoles = request.Where(x => x.Selected).Select(x => x.Name).ToList();
            foreach (var roleName in addedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == false)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }
            serviceResult.OnSuccess(1, "Gán quyền thành công");
            return serviceResult;
        }

        public async Task<ServiceResult> Update(Guid id, UserVm request)
        {
            ServiceResult serviceResult = new ServiceResult();
            if (await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != id))
            {
                serviceResult.OnError(0, "Email đã được sử dụng");
                return serviceResult;
            }
            var user = await _userManager.FindByIdAsync(id.ToString());
            user.Email = request.Email;
            user.FullName = request.FullName;
            user.PhoneNumber = request.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                serviceResult.OnError(0, "Cập nhật tài khoản không thành công");
                return serviceResult;
            }
            serviceResult.OnError(1, "Cập nhật thành công");
            return serviceResult;
        }
    }
}
