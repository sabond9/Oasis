using System.Web;
using System.Web.Mvc;
using Oasis.Common;
using Oasis.DataModel.Enums;

namespace Oasis.Infrastructure
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly Permissions[] _permissions;

        public CustomAuthorizeAttribute(params Permissions[] permissions)
        {
            _permissions = permissions;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            return isAuthorized && AppSecurityContext.UserHasPermissions(_permissions);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("Account/Index");
        }
    }
}