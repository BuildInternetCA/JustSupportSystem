using System.Diagnostics;
using JustSupportSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JustSupportSystem.Controllers
{
    public class HomeController(JDBContext jDBContext) : JBaseController(jDBContext) 
    {
        public IActionResult Index()
        {
            JDB.UserAccounts.FirstOrDefault();
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
