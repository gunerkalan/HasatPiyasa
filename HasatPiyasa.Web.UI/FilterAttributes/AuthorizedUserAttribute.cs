using HasatPiyasa.Business;
using HasatPiyasa.Entity.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasatPiyasa.Web.UI.FilterAttributes
{
    [AttributeUsage(AttributeTargets.All,AllowMultiple =true,Inherited =true)]
    public class AuthorizedUserAttribute :Attribute, IActionFilter
    {
        public readonly string _role;

        public AuthorizedUserAttribute(string role)
        {
            _role = role;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
          
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var user =  context.HttpContext.Session.Get<Users>("User");
            var action = ((ControllerActionDescriptor)context.ActionDescriptor);
            var actionName = ((ControllerActionDescriptor)context.ActionDescriptor).ActionName;
            var controllerName = ((ControllerActionDescriptor)context.ActionDescriptor).ControllerName;
            if (user == null)
            {
                context.Result = (ActionResult)new RedirectToActionResult("login", "account", null);
            }

            var attr = (AuthorizedUserAttribute)action.MethodInfo.GetCustomAttributes(false).FirstOrDefault(x => x.GetType() == typeof(AuthorizedUserAttribute));
            if (attr != null)
            {
                var requirePermisson = attr._role;

                if (attr._role != user.Roles)
                {

                    context.Result = (ActionResult)new RedirectToActionResult("index", "home", null);
                }
            }

           
        }
    }
}
