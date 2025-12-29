using JustSupportSystem.Models;

namespace JustSupportSystem.JSystem
{
    public class JMidlleware
    {
        private readonly RequestDelegate _next;
        public JMidlleware(RequestDelegate next, IHostApplicationLifetime applicationLifetime)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await context.Response.WriteAsync(e.ToString());
            }
        }
    }
    public static class JMidllewareExtensions
    {
        public static IApplicationBuilder UseBIAMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JMidlleware>();
        }
    }
}
