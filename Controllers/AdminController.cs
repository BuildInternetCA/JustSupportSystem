using JustSupportSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace JustSupportSystem.Controllers
{
    public class AdminController(JDBContext jDBContext) : JBaseController(jDBContext)
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
