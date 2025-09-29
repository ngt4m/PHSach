using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public HomeController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
