using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using Oasis.DataAccess;
using Oasis.DataModel;
using Oasis.DataModel.Model;

namespace Oasis.Infrastructure
{
    public class AuthenticationManager
    {
        public void Authenticate(string userName)
        {
            var str = "";
            var unitOfWork = new UnitOfWork(new OasisContext());
            var user = unitOfWork.GetBaseRepository<User>().GetAll
                (
                    u => u.UserRoles,
                    u => u.UserRoles.Select(ur => ur.Role),
                    u => u.UserRoles.Select(ur => ur.Role.PermissionRoles)
                ).Single(u => u.UserName == userName);

            var userCookieData = new UserCookieData
            {
                UserId = user.Id,
                Roles = new HashSet<string>(user.UserRoles.Select(ur => ur.Role.Name)),

                PermissionIds = new HashSet<int>(user.UserRoles.SelectMany(ur => ur.Role.PermissionRoles)
                                                   .Select(pr => pr.PermissionId)),
                UserName = user.UserName
            };

            var userData = JsonConvert.SerializeObject(userCookieData);
            var authenticationTicket = new FormsAuthenticationTicket(1, user.UserName, DateTime.Now,
                DateTime.Now.AddMinutes(20), false, userData);
            FormsAuthentication.SetAuthCookie(userName, true);

            var encryptedTicket = FormsAuthentication.Encrypt(authenticationTicket);
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true,
                Expires = authenticationTicket.Expiration
            });
        }
    }
}