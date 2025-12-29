using JustSupportSystem.DTO;
using JustSupportSystem.JSystem;
using JustSupportSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;


namespace JustSupportSystem.Controllers
{
    
    public class HomeController(JDBContext jDBContext) : JBaseController(jDBContext) 
    {
        [UserAccess]
        public IActionResult Index()
        {
            return View();
        }



        [Route("/get-started/login-start")]
        public IActionResult LoginPreAccess()
        {
            return SecureRedirect("/access-point/login-start");
        }

        [Route("/access-point/login-start")]
        public IActionResult LoginPage(string token)
        {
            if (string.IsNullOrEmpty(token) || !VerifyToken(token))
            {
                return NotFound();
            }
            ViewBag.Token = GetToken();
            return View();
        }

        [HttpPost]
        [Route("/access-point/login-start/submit")]
        public IActionResult LoginSubmit(string token, string data1, string data2)
        {
            if (string.IsNullOrEmpty(token) || !VerifyToken(token))
            {
                return SecureRedirect("/access-point/login-start");
            }
            var account = JDB.UserAccounts.FirstOrDefault(p => !p.IsSSOUser && !p.IsDeleted && p.Email == data1);
            if (account != null)
            {
                if (account.FailedTries > 5)
                {
                    ViewBag.Error = "Account blocked. Please contact Admin team.";
                    ViewBag.Token = GetToken();
                    return View("LoginPage");
                }
                if (!string.IsNullOrEmpty(account.Password) && account.Password.Decrypt().Equals(data2))
                {
                    UserToken utoken = new UserToken();
                    utoken.UserID = account.Id;
                    utoken.IsExpired = false;
                    utoken.IsEmailToken = false;
                    utoken.Token = System.Guid.NewGuid().ToString();
                    if (account.IsTOTPRequired)
                    {
                        utoken.IsTOPTVerified = false;
                        ViewBag.TOPT = true;
                    }
                    else
                    {
                        utoken.IsTOPTVerified = true;
                    }
                    JDB.UserTokens.Add(utoken);
                    account.LastLoginDate = DateTime.UtcNow;
                    account.FailedTries = 0;
                    HttpContext.WriteCookie("_adsense", utoken.Token, DateTime.UtcNow.AddHours(8));
                }
                else
                {
                    account.FailedTries += 1;
                }
                JDB.SaveChanges();
            }
            else
            {
                ViewBag.Error = "Invalid username or password!";
            }
            ViewBag.Token = GetToken();
            return View("LoginPage");
        }

        [HttpPost]
        [Route("/access-point/login-start/totp")]
        public IActionResult Logintotp(string token, string data1)
        {
            if (string.IsNullOrEmpty(token) || !VerifyToken(token))
            {
                return SecureRedirect("/access-point/login-start");
            }
            string tokenCookies = HttpContext.GetCookie("_adsense");

            if (tokenCookies != null && !tokenCookies.Equals(""))
            {
                var tokenDb = JDB.UserTokens.Include(p => p.UserAccount).FirstOrDefault(p => p.Token == tokenCookies);
                if (tokenDb != null && !tokenDb.IsTOPTVerified) 
                {
                    var account = JDB.UserAccounts.FirstOrDefault(p => !p.IsSSOUser && !p.IsDeleted && p.Id == tokenDb.UserID);
                    if (account != null)
                    {
                        if (account.FailedTries > 5)
                        {
                            JDB.Remove(tokenDb);
                            JDB.SaveChanges();
                            ViewBag.Error = "Account blocked. Please contact admin team";
                        }
                        else
                        {
                            TotpService service = new TotpService();
                            bool result = service.ValidateOTP(account.TOTPSecret, data1);
                            if (result)
                            {
                                tokenDb.IsTOPTVerified = true;
                                account.FailedTries = 0;
                                JDB.SaveChanges();
                                return SecureRedirect("/");
                            }
                            else
                            {
                                account.FailedTries += 1;
                                JDB.SaveChanges();
                                ViewBag.Error = "Invalid OTP entered";
                            }
                        }
                    }
                }
            }
            
            ViewBag.Token = GetToken();
            return View("LoginPage");
        }

        [Route("/access-point/internet-sso-redirect")]
        public IActionResult SSOLoginPage(string token)
        {
            return Redirect("https://home.helixgs.com/integrate/123123");
        }
        public IActionResult ViewPage()
        {
            //TB7K64HSM6DW25FV6G4TS7RXTKOJJDKJ -> TOTP
            //tS5qqd4zxmZLa8DAjkf6Pw== -> PASS
            //TotpService service = new TotpService();
            ////string key = service.GenerateSecretKey();
            //var qrcode = service.GenerateQRCode("jaskaranshsd@live.com", "TB7K64HSM6DW25FV6G4TS7RXTKOJJDKJ");

            //string base64Image = Convert.ToBase64String(qrcode);
            //ViewBag.QRCodeImage = "data:image/png;base64," + base64Image;
            var password = "Happy@2239".Encrypt();
            return Content(password);
        }

    }
}
