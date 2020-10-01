using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WFJ.Web.CustomAttribute
{
    public class AuthorizeActivity : System.Web.Mvc.AuthorizeAttribute
    {
        public AuthorizeActivity(params object[] userTypes)
        {
            UserTypes = new List<string>();
            foreach (var userType in userTypes)
            {
                UserTypes.Add(userType.ToString());
            }
        }

        private List<string> UserTypes { get; set; }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new
                  RouteValueDictionary(new { controller = "Account", action = "Login" }));
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            int userId = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
            string userTypeId = Convert.ToString(HttpContext.Current.Session["UserType"]);
            if (userId !=0)
            {
                if ((UserTypes.Contains(userTypeId))|| UserTypes.Contains("0"))
                {
                    return true;
                }
                return false;
            }
            else
            {
                // httpContext.IsCustomErrorEnabled
                return false;
            }

        }
    }
}