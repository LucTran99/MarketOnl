using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using MarketOnl.Extentions;

namespace MarketOnl.Areas.Admin.Attributes
{
    public class AuthorizedAttribute : Attribute, IAuthorizationFilter
    {

        public string Code { get; set; } = "";

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            List<string> scopes = context.HttpContext.Session.Get<List<string>>("Codes");
            if (scopes.Count == 0 || !scopes.Contains(Code))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
