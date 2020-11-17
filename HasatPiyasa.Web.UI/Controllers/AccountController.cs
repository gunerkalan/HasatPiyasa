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
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;

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
        public async Task<object> Login(UserForLoginDto userForLoginDto)
        {
            var sonuc = await _authService.Login(userForLoginDto);

            if (sonuc.BasariliMi)
            {
                _httpContext.HttpContext.Session.Set("User", sonuc.Veri);
                return JsonConvert.SerializeObject(new { success = true, messages = sonuc.Mesaj });



            }
            else
            {
                return JsonConvert.SerializeObject(new { success = false, messages = sonuc.Mesaj });
            }

        }


        [HttpGet]
        public async Task<ActionResult> Logout()
        {

            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
