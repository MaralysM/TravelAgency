

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Qmos.UI.Filters
{
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext FilterContext)
        {
            if (FilterContext.HttpContext.Session.Get("User") == null)
            {
                FilterContext.Result = new RedirectResult("~/Account/Login");
                return;
            }
            base.OnActionExecuting(FilterContext);
        }
    }
}
