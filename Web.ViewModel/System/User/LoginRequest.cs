using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Web.ViewModel.System.User
{
    public class LoginRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui lòng điền tên đăng nhập")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Vui lòng điền mật khẩu")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
