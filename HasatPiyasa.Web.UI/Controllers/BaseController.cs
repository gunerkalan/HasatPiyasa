using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HasatPiyasa.Business;
using HasatPiyasa.Entity.Entity;
using HasatPiyasa.Web.UI.FilterAttributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HasatPiyasa.Web.UI.Controllers
{
    public class BaseController : Controller
    {
         
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = HttpContext.Session.Get<Users>("User");
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
                  
                    context.Result = RedirectToAction("Index", "home");

                }
            }

            base.OnActionExecuting(context);
        }

        public Users GetCurrentUser()
        {
            var user = HttpContext.Session.Get<Users>("User"); 
            return user;           
            
        }

    }
}
