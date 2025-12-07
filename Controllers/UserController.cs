using JustSupportSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace JustSupportSystem.Controllers
{
    public class UserController(JDBContext jDBContext) : JBaseController(jDBContext)
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
