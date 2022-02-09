using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnAuction.Filters
{
    public class UserAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool auth = false;

            foreach (var role in Roles.Split(',').ToList())
            {
                if (filterContext.HttpContext.User.IsInRole(role))
                {
                    auth = true;
                }
            }

            if (!auth)
            {
                filterContext.Result = new HttpNotFoundResult();
            }
        }
    }
}
