using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Newtonsoft.Json;
using Oasis.Infrastructure;
using WebGrease.Css.Extensions;

namespace Oasis
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthorizeRequest(object sender, EventArgs e)
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie == null)
                return;

            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

            var userCookieData = JsonConvert.DeserializeObject<UserCookieData>(ticket.UserData);
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userCookieData.UserId.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Name, userCookieData.UserName));
            identity.AddClaim(new Claim("Permission", string.Join(",", userCookieData.PermissionIds ?? new HashSet<int>())));

            userCookieData.Roles.ForEach(r => identity.AddClaim(new Claim(ClaimTypes.Role, r)));

            var principal = new GenericPrincipal(identity, userCookieData.Roles?.ToArray() ?? new string[0]);
            HttpContext.Current.User = principal;
        }
    }
}
