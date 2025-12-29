using JustSupportSystem.Models;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;

namespace JustSupportSystem.JSystem
{
    public class JRouteSecurity: IRule
    {
       // private TrafficLog Log { get; set; }
        public void ApplyRule(RewriteContext context)
        {
            try
            {
                var dbContext = context.HttpContext.RequestServices.GetRequiredService<JDBContext>();

                //using (var ContextDb = new BIDBContext())
                //{
                //    ActivateTrafficLog(context.HttpContext, ContextDb);
                //    CheckIPRestriction(context.HttpContext, ContextDb);
                    LoginSystemActivate(context.HttpContext, dbContext);
                //}
                // if (IsBot(context.HttpContext)) throw new Exception("Issue with website");
            }
            catch (Exception e)
            {
                context.HttpContext.Response.WriteAsync(e.ToString());
                //try
                //{
                //    using (var ContextDb = new BIDBContext())
                //    {
                //        context.HttpContext.AddException(ContextDb, e);
                //    }
                //}
                //catch
                //{
                //    HttpServices.WriteLog($"SystemExceptions_{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}", new List<Exception> { e });
                //}
                //if (context.HttpContext.Request.Path != null && !context.HttpContext.Request.Path.ToString().Contains("system/error"))
                //{
                //    context.HttpContext.Response.Redirect("/system/error/E500");
                //}
                //else
                //{
                //    context.HttpContext.Response.WriteAsync("Apologies, we're experiencing some technical issues at the moment. We're working to resolve them as quickly as possible.");
                //}
                context.Result = RuleResult.EndResponse;
            }
            return;
        }

        //private void CheckIPRestriction(HttpContext context, BIDBContext DbContext)
        //{
        //    var ipAddress = GetClientIP(context);
        //    if (!string.IsNullOrEmpty(ipAddress))
        //    {
        //        var restriction = DbContext.RequestFilters.AsNoTracking().FirstOrDefault(p => p.FilterTypeID == 1 && p.FilterValue == ipAddress);
        //        if (restriction != null)
        //        {
        //            throw new Exception("Access denied!");
        //        }
        //    }
        //}
        //private bool IsBot(HttpContext context)
        //{
        //    if (context.Request.Headers.ContainsKey(HeaderNames.UserAgent))
        //    {
        //        var userAgent = context.Request.Headers[HeaderNames.UserAgent].ToString();
        //        if (!string.IsNullOrEmpty(userAgent) && (
        //            userAgent.ToLower().Contains("googlebot")
        //         || userAgent.ToLower().Contains("bingbot")))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        //private string GetClientIP(HttpContext context)
        //{
        //    var ip = context.Connection.RemoteIpAddress?.ToString();
        //    if (string.IsNullOrEmpty(ip))
        //    {
        //        if (context.Request.Headers.ContainsKey("X-Forwarded-For"))
        //        {
        //            ip = context.Request.Headers["X-Forwarded-For"].ToString();

        //        }
        //    }
        //    return ip ?? "";
        //}
        //private void ActivateTrafficLog(HttpContext context, BIDBContext DbContext)
        //{
        //    if (!context.Items.ContainsKey("TrafficLogId"))
        //    {
        //        Log = new TrafficLog
        //        {
        //            WebSite = context.Request.Host.Host,
        //            DateAdded = DateTime.UtcNow,
        //            IpAddress = GetClientIP(context),
        //            Path = context.Request.Path.ToString(),
        //            HttpReferer = context.Request.Headers.ContainsKey("Referer") ? context.Request.Headers["Referer"].ToString() : "Unknown",
        //            UserAgent = context.Request.Headers.ContainsKey(HeaderNames.UserAgent) ? context.Request.Headers[HeaderNames.UserAgent].ToString() : "Unknown",
        //        };
        //        if (!string.IsNullOrEmpty(Log.UserAgent))
        //        {
        //            try
        //            {
        //                var uaParser = Parser.GetDefault();
        //                ClientInfo c = uaParser.Parse(Log.UserAgent);
        //                Log.Browser = c.UA.Family;
        //                Log.Device = c.Device.Family;
        //                Log.OS = c.OS.Family;
        //            }
        //            catch
        //            {
        //                Log.Browser = "Unknown";
        //                Log.Device = "Unknown";
        //                Log.OS = "Unknown";
        //            }
        //        }
        //        DbContext.TrafficLogs.Add(Log);
        //        DbContext.SaveChanges();
        //        context.Items.Add("TrafficLogId", Log.Id);
        //    }
        //}
        private void LoginSystemActivate(HttpContext context, JDBContext DbContext)
        {
            UserToken user;
            string token = context.GetCookie("_adsense");

            if (token != null && !token.Equals(""))
            {
                var tokenDb = DbContext.UserTokens.Include(p => p.UserAccount).AsNoTracking().FirstOrDefault(p => p.Token == token);
                if (tokenDb != null)
                {
                    var span = DateTime.UtcNow - tokenDb.DateAdded;
                    if (span.TotalHours > 2)
                    {
                        DbContext.Remove(tokenDb);
                        DbContext.SaveChanges();
                        return;
                    }
                    if (!tokenDb.IsExpired && !tokenDb.IsDeleted && tokenDb.IsTOPTVerified)
                    {
                        user = tokenDb;
                        if (user != null)
                        {
                            if (user.UserAccount != null)
                            {
                                context.Items.Add("APPDATAHTTP", user.UserAccount);
                            }
                        }
                    }
                }

            }

        }
    }
}
