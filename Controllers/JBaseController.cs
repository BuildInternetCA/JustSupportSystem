using JustSupportSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JustSupportSystem.Controllers
{
    public class JBaseController : Controller
    {
        protected JDBContext JDB;

        public JBaseController(JDBContext jDBContext)
        {
            JDB = jDBContext;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                JDB?.Dispose();
            }
            base.Dispose(disposing);
        }
        protected UserAccount? LoggedInUser()
        {
            ControllerContext.HttpContext.Items.TryGetValue("APPDATAHTTP", out var userObj);
            if (userObj == null || !(userObj is UserAccount userIdentity))
            {
                return null;
            }
            return userObj as UserAccount;
        }

        protected bool IsLoggedIn()
        {
            return LoggedInUser() != null;
        }

        public async Task<IActionResult> Form(object? model)
        {
            return View("/Views/Partial/Form.cshtml", model);
        }

        public IActionResult Table(object? model)
        {
            return View("/Views/Partial/Table.cshtml", model);
        }
    }
}
