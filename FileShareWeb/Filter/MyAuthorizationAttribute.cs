using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileShareWeb.Filter
{
    public class MyAuthorizationAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        public int ActionMemberType { get; private set; }
        public MyAuthorizationAttribute()
        {

        }
        public MyAuthorizationAttribute(int _memberType)
        {
            this.ActionMemberType = _memberType;
        }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var member = (DB.USER_TABLE)HttpContext.Current.Session["LogonUser"];
            if (member == null)
            {
                filterContext.Result = new RedirectResult("/i/Index"); // i contoller index action result
            }
        }
    }
}