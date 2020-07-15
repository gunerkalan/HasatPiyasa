using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HasatPiyasa.Business;
using HasatPiyasa.Entity.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HasatPiyasa.Web.UI.Controllers
{
    public class BaseController : Controller
    {
         
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = HttpContext.Session.Get<Users>("User");

            if (user == null)
            {
                context.Result = (ActionResult)new RedirectToActionResult("login", "account", null);
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
