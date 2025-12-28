using JustSupportSystem.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JustSupportSystem.System
{
    public class AdminUserAccessAttribute : ActionFilterAttribute
    {
        private UserAccount? LoggedInUser(ActionExecutingContext context)
        {
            //context.HttpContext.Items.TryGetValue("APPDATAHTTP", out var userObj);
            //if (userObj == null || !(userObj is UserAccount userIdentity))
            //{
            //    return null;
            //}
            //if (userObj is UserAccount && (userObj as UserAccount).UserRoleID == (short)UserRole.Administrator)
            //{
            //    return userObj as UserAccount;
            //}
            return null;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (LoggedInUser(context) == null)
            {
                context.Result = new Microsoft.AspNetCore.Mvc.RedirectResult("/login?reason=user_not_logged");
            }
            // Do something before the action executes.
        }

    }
}
