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

        //public bool RequiredSponsorId { get; set; }
        private new List<string> UserTypes { get; set; }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            string userTypeId = Convert.ToString(HttpContext.Current.Session["UserType"]);
            if (string.IsNullOrEmpty(userTypeId))
            {
                string actionName = filterContext.ActionDescriptor != null ? filterContext.ActionDescriptor.ActionName.ToLower() : string.Empty;
                string controllerName = filterContext.ActionDescriptor != null && filterContext.ActionDescriptor.ControllerDescriptor != null ?
                                                    filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower() : string.Empty;
                base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new
                  RouteValueDictionary(new { controller = "Account", action = "Login" }));
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string userTypeId = Convert.ToString(HttpContext.Current.Session["UserType"]);
            if (UserTypes.Contains(userTypeId))
            {
                return true;
            }
            else
            {
                // httpContext.IsCustomErrorEnabled
                return false;
            }

        }
    }
}