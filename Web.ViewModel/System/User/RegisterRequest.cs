using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Web.ViewModel.System.User
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Vui lòng điền tên")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng điền email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng điền SĐT")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Vui lòng điền tên đăng nhập")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng điền mật khẩu")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu")]
        public string ConfirmPassword { get; set; }
    }
}
