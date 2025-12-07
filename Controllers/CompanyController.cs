using JustSupportSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace JustSupportSystem.Controllers
{
    public class CompanyController(JDBContext jDBContext) : JBaseController(jDBContext)
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
