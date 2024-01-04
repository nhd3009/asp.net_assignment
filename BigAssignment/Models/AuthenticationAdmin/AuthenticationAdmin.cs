using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BigAssignment.Areas.Admin.Models.AuthenAdmin
{
    public class AuthenticationAdmin : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Session.GetString("Admin_Name") == null)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary{
                    {"Controller", "Access" },
                    {"Action",  "Login"}
                });
            }
        }
    }
}