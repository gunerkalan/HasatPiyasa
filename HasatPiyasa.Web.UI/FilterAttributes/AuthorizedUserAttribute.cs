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
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
