using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PHSach.Helper;
using PHSach.Models;
using PHSach.Models.EntityModel;
using PHSach.Models.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PHSach.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AccountController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("/account/login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });
            }

            // 1. Mã hóa password thành MD5
            var passwordHash = model.Password.ToMd5();

            // 2. Kiểm tra User trong DB
            var user = _context.Users.FirstOrDefault(u =>
                u.Username == model.Username
                && u.PasswordHash == passwordHash
                && u.IsActive);

            if (user == null)
            {
                return Unauthorized(new { message = "Sai tài khoản hoặc mật khẩu" });
            }

            // 3. Tạo claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Role ?? "User")
            };

            // 4. Tạo identity & principal
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // 5. Đăng nhập bằng cookie auth
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal,
                new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe, // true nếu chọn "Nhớ mật khẩu"
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(60)
                });

            // 6. Trả JSON cho client
            return Ok(new { message = "Đăng nhập thành công" });
        }


    [HttpGet]
        public IActionResult CreateAccount()
        {
            return View();
        }
    }
}
