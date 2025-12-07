using System.Diagnostics;
using JustSupportSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JustSupportSystem.Controllers
{
    public class HomeController : JBaseController
    {
         
        public HomeController(JDBContext jDBContext) : base(jDBContext)
        {

        }
        public IActionResult Index()
        {
            DbCtx.UserAccounts.FirstOrDefault();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ViewPage()
        {
            return View();
        }

    }
}
