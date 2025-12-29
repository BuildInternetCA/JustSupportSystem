using JustSupportSystem.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JustSupportSystem.JSystem
{
    public class UserAccessAttribute : ActionFilterAttribute
    {
        private UserAccount? LoggedInUser(ActionExecutingContext context)
        {
            context.HttpContext.Items.TryGetValue("APPDATAHTTP", out var userObj);
            if (userObj == null || !(userObj is UserAccount userIdentity))
            {
                return null;
            }
            if (userObj is UserAccount)
            {
                return userObj as UserAccount;
            }
            return null;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (LoggedInUser(context) == null)
            {
                context.Result = new Microsoft.AspNetCore.Mvc.RedirectResult("/get-started/login-start");
            }
            // Do something before the action executes.
        }

    }
    public class AdminUserAccessAttribute : ActionFilterAttribute
    {
        private UserAccount? LoggedInUser(ActionExecutingContext context)
        {
            context.HttpContext.Items.TryGetValue("APPDATAHTTP", out var userObj);
            if (userObj == null || !(userObj is UserAccount userIdentity))
            {
                return null;
            }
            if (userObj is UserAccount && (userObj as UserAccount).GetUserRoleEnum() == UserRoleEnum.System_Admin)
            {
                return userObj as UserAccount;
            }
            return null;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (LoggedInUser(context) == null)
            {
                context.Result = new Microsoft.AspNetCore.Mvc.RedirectResult("/get-started/login-start");
            }
            // Do something before the action executes.
        }

    }

    public class AgentUserAccessAttribute : ActionFilterAttribute
    {
        private UserAccount? LoggedInUser(ActionExecutingContext context)
        {
            context.HttpContext.Items.TryGetValue("APPDATAHTTP", out var userObj);
            if (userObj == null || !(userObj is UserAccount userIdentity))
            {
                return null;
            }
            if (userObj is UserAccount && ((userObj as UserAccount).GetUserRoleEnum() == UserRoleEnum.Agent_User || (userObj as UserAccount).GetUserRoleEnum() == UserRoleEnum.Agent_Manager) || (userObj as UserAccount).GetUserRoleEnum() == UserRoleEnum.System_Admin)
            {
                return userObj as UserAccount;
            }
            return null;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (LoggedInUser(context) == null)
            {
                context.Result = new Microsoft.AspNetCore.Mvc.RedirectResult("/get-started/login-start");
            }
            // Do something before the action executes.
        }

    }

    public class AgentManagerUserAccessAttribute : ActionFilterAttribute
    {
        private UserAccount? LoggedInUser(ActionExecutingContext context)
        {
            context.HttpContext.Items.TryGetValue("APPDATAHTTP", out var userObj);
            if (userObj == null || !(userObj is UserAccount userIdentity))
            {
                return null;
            }
            if (userObj is UserAccount && ((userObj as UserAccount).GetUserRoleEnum() == UserRoleEnum.Agent_Manager || (userObj as UserAccount).GetUserRoleEnum() == UserRoleEnum.System_Admin))
            {
                return userObj as UserAccount;
            }
            return null;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (LoggedInUser(context) == null)
            {
                context.Result = new Microsoft.AspNetCore.Mvc.RedirectResult("/get-started/login-start");
            }
            // Do something before the action executes.
        }

    }


    public class ClientUserAccessAttribute : ActionFilterAttribute
    {
        private UserAccount? LoggedInUser(ActionExecutingContext context)
        {
            context.HttpContext.Items.TryGetValue("APPDATAHTTP", out var userObj);
            if (userObj == null || !(userObj is UserAccount userIdentity))
            {
                return null;
            }
            if (userObj is UserAccount && ((userObj as UserAccount).GetUserRoleEnum() == UserRoleEnum.Client_User || (userObj as UserAccount).GetUserRoleEnum() == UserRoleEnum.Client_Manager || (userObj as UserAccount).GetUserRoleEnum() == UserRoleEnum.System_Admin))
            {
                return userObj as UserAccount;
            }
            return null;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (LoggedInUser(context) == null)
            {
                context.Result = new Microsoft.AspNetCore.Mvc.RedirectResult("/get-started/login-start");
            }
            // Do something before the action executes.
        }

    }

    public class ClientManagerUserAccessAttribute : ActionFilterAttribute
    {
        private UserAccount? LoggedInUser(ActionExecutingContext context)
        {
            context.HttpContext.Items.TryGetValue("APPDATAHTTP", out var userObj);
            if (userObj == null || !(userObj is UserAccount userIdentity))
            {
                return null;
            }
            if (userObj is UserAccount && ((userObj as UserAccount).GetUserRoleEnum() == UserRoleEnum.Client_Manager || (userObj as UserAccount).GetUserRoleEnum() == UserRoleEnum.System_Admin))
            {
                return userObj as UserAccount;
            }
            return null;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (LoggedInUser(context) == null)
            {
                context.Result = new Microsoft.AspNetCore.Mvc.RedirectResult("/get-started/login-start");
            }
            // Do something before the action executes.
        }

    }

}
