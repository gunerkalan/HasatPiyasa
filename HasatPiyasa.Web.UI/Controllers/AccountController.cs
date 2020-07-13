using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HasatPiyasa.Business.Abstract;
using HasatPiyasa.Entity.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HasatPiyasa.Web.UI.Controllers
{
    public class AccountController : Controller
    {
        private IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(UserForLoginDto userForLoginDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.Login(userForLoginDto);

                if (result.BasariliMi)
                {
                    //Session["User"] = result.Veri;
                    //FormsAuthentication.SetAuthCookie(result.Veri.UserName, false);
                    //var userpermisson = await _siloUserPermissionsService.GetUserPermisions(result.Veri.Id);
                    //Session["Permissions"] = userpermisson.Veri;

                    return RedirectToAction("Index", "i");
                }

                ModelState.AddModelError("", result.Mesaj);
            }

            return View(userForLoginDto);
        }

        public ActionResult Logout()
        {
            //FormsAuthentication.SignOut();
            //Session["User"] = null;
            return RedirectToAction("Login", "Account");
        }
    }
}
