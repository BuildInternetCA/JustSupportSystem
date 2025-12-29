using JustSupportSystem.JSystem;
using JustSupportSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace JustSupportSystem.Controllers
{
    [AdminUserAccess]
    public class AdminController(JDBContext jDBContext) : JBaseController(jDBContext)
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
