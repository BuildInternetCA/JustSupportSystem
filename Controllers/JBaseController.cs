using JustSupportSystem.Models;
using JustSupportSystem.JSystem;
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
        protected string GetToken()
        {
            return (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() * 39).ToString().Encrypt().EncodeURL();
        }

        protected IActionResult SecureRedirect(string url, string queryWithoutQuestionMark="")
        {
            string token = GetToken();
            url += "?token=" + token + queryWithoutQuestionMark;
            return Content(@$"<html>
                                <head>
                                    <title>Loading page</title>
                                </head>
                                <body>
                                   <script type='text/javascript'>
                                        window.location.href = ""{url}"";
                                    </script>
                                </body>
                                </html>", "text/html");
        }

        protected bool VerifyToken(string token)
        {
            try
            {
                long tokenVak = 0;
                token = token.Decrypt().DecodeURL();
                if (long.TryParse(token, out tokenVak))
                {
                    tokenVak = tokenVak / 39;
                    var date = DateTimeOffset.FromUnixTimeMilliseconds(tokenVak);
                    var span = DateTimeOffset.UtcNow - date;
                    if (span.TotalMinutes > 5)
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch
            {
                // people never stop
            }
            return false;
        }
    }
}
