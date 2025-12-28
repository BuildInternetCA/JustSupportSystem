using System.Diagnostics;
using System.Text;

namespace JustSupportSystem.System
{
    public static class HttpServices
    {
        
        public static void WriteLog(string fileName, List<Exception> es)
        {
            if (!Directory.Exists("Logs"))
            {
                Directory.CreateDirectory("Logs");
            }
            StringBuilder exceptions = new StringBuilder();
            foreach (var ex in es)
            {
                exceptions = exceptions.AppendLine($"{DateTime.UtcNow}: {ex.Message}\n{ex.StackTrace}\n\n");
            }
            File.WriteAllText($"Logs/{fileName}.log", exceptions.ToString());
        }
        public static void WriteCookie(this HttpContext context, string key, string data, DateTime dateExpirey)
        {
            if (!context.Request.Headers.ContainsKey("User-Agent"))
            {
                return;
            }
            CookieOptions option = new CookieOptions();
            string agent1 = context.Request.Headers["User-Agent"].ToString();
            string dataEnptd = data.Encrypt();
            agent1 = (agent1 + dataEnptd.Length + "" + data.Length).Encrypt();
            if (!Debugger.IsAttached)
            {
                option.Domain = ".buildinternet.ca";
                option.Expires = DateTime.UtcNow.AddDays(999);
            }
            else
            {
                option.Domain = "localhost";
                option.Expires = dateExpirey;
            }
            option.HttpOnly = true;
            option.Secure = true;
            option.Path = "/";
            //testing
            option.SameSite = SameSiteMode.Strict;
            context.Response.Cookies.Append(key, dataEnptd, option);
            context.Response.Cookies.Append(key + "_ctrl", agent1, option);
        }
        public static void RemoveCookie(this HttpContext context, string key)
        {
            CookieOptions option = new CookieOptions();
            if (!Debugger.IsAttached)
            {
                option.Domain = "localhost";
            }
            else
            {
                option.Domain = ".buildinternet.ca";
            }
            option.Expires = DateTime.UtcNow.AddDays(-99);
            option.HttpOnly = true;
            option.Secure = true;
            option.Path = "/";
            context.Response.Cookies.Append(key, "DATA IS EXPIRED".Encrypt(), option);
            context.Response.Cookies.Append(key + "_ctrl", "DATA IS EXPIRED".Encrypt(), option);
        }

        public static string GetCookie(this HttpContext context, string key)
        {
            if (!context.Request.Headers.ContainsKey("User-Agent"))
            {
                return "We have problem in accessing cookies";
            }
            if (context.Request.Cookies.ContainsKey(key) && context.Request.Cookies.ContainsKey(key))
            {
                string agent = context.Request.Headers["User-Agent"].ToString();
                string dataEnpt = context.Request.Cookies[key];
                string orgData = dataEnpt.Decrypt();
                agent = (agent + dataEnpt.Length + "" + orgData.Length).Encrypt();
                string controlCookie = context.Request.Cookies[key + "_ctrl"];
                if (agent.Equals(controlCookie))
                {
                    return orgData;
                }
            }
            return "";
        }
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            if (request.Headers != null)
                return request.Headers.ContainsKey("X-Requested-With") ? (request.Headers["X-Requested-With"] == "XMLHttpRequest") : request.Headers.ContainsKey("x-requested-with") ? (request.Headers["x-requested-with"] == "XMLHttpRequest") : false;
            return false;
        }
    }
}
