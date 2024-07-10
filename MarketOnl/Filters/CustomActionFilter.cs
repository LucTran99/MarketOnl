using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using MarketOnl.Data;
using MarketOnl.Extentions;

namespace MarketOnl.Filters
{
    public class CustomActionFilter: IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var area = context.RouteData.Values["area"];
            if (context.RouteData.Values["action"].ToString() != "Login" && area?.ToString() == "Admin")
            {

                var memeber = context.HttpContext.Session.Get<Account>("Member");
                if (memeber == null)
                {
                    context.Result = new RedirectResult("/admin/Accounts/Login");
                }

            }
        }
    }
}
