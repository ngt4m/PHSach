using Microsoft.AspNetCore.Mvc;
using PHSach.Models;

namespace PHSach.ViewComponents
{
    [ViewComponent(Name = "_Sidebar")]
    public class AdminSidebarViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public AdminSidebarViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("~/Views/Components/_Sidebar.cshtml");
        }
    }
}
