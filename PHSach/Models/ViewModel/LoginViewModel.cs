using System.ComponentModel.DataAnnotations;

namespace PHSach.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Mật khẩu phải từ 6-20 ký tự")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }


}
