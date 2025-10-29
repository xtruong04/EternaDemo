using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EternaDemo.Areas.Admin.Filters
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // Đã đăng nhập nhưng không phải Admin
                filterContext.Result = new RedirectResult("/?unauthorized=true");
            }
            else
            {
                // Chưa đăng nhập → về Login
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}