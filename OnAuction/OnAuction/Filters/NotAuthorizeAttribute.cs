using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnAuction.Filters
{
    public class NotAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool auth = filterContext.HttpContext.User.Identity.IsAuthenticated;

            if (auth)
            {
                filterContext.Result = new HttpNotFoundResult();
            }
        }
    }
}
