using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HasastPiyasa.DataAccess.Abstract;
using HasatPiyasa.Business.Abstract;
using HasatPiyasa.Web.UI.Controllers;
using HasatPiyasa_Web_UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HasatPiyasa_Web_UI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IEmteaService _emteaService;

        public HomeController(IEmteaService emteaService )
        {
            _emteaService = emteaService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HasaInputViewModel();
            var emtea =await _emteaService.GetEmteaTable(1);
            model.Emteas = emtea.Veri;
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View();
        }
    }
}
