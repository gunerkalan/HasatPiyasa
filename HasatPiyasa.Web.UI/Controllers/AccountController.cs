using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HasatPiyasa.Business.Abstract;
using HasatPiyasa.Entity.Dtos;
using HasatPiyasa.Entity.Entity;
using Microsoft.AspNetCore.Mvc;
using HasatPiyasa.Business;
using Microsoft.AspNetCore.Http;

namespace HasatPiyasa.Web.UI.Controllers
{
    public class AccountController : Controller
    {
        private IAuthService _authService;
        IHttpContextAccessor _httpContext;
        public AccountController(IAuthService authService, IHttpContextAccessor httpContext)
        {
            _authService = authService;
            _httpContext = httpContext;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<bool> Login(UserForLoginDto userForLoginDto)
        { 
                var result = await _authService.Login(userForLoginDto);

                if (result.BasariliMi)
                {
                   _httpContext.HttpContext.Session.Set("User", result.Veri);
                                      
                    return true;
                }
                else
                {
                    return false;
                }                
             
        }

        public ActionResult Logout()
        {
            //FormsAuthentication.SignOut();
            //Session["User"] = null;
            return RedirectToAction("Login", "Account");
        }
    }
}
