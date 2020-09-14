using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HasastPiyasa.DataAccess.Abstract;
using HasatPiyasa.Business.Abstract;
using HasatPiyasa.Core.Entities;
using HasatPiyasa.Entity.Entity;
using HasatPiyasa.Web.UI.Controllers;
using HasatPiyasa.Web.UI.Models;
using HasatPiyasa_Web_UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HasatPiyasa_Web_UI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IEmteaService _emteaService;
        private readonly IFormDataInputService _formDataInputService;

        public HomeController(IEmteaService emteaService, IFormDataInputService formDataInputService)
        {
            _emteaService = emteaService;
            _formDataInputService = formDataInputService;
        }


        [HttpGet]
        public ActionResult Index()
        {
            DataInputRiceListModel model = new DataInputRiceListModel
            {
                FormDataInput = new FormDataInput()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<object> DataInputListDataForToday()
        {
            var res = await _formDataInputService.GetFormDataGTableForOnlyDate(DateTime.Today.Date);

            var response = res.Veri.GroupBy(x => x.SubeName).ToList().Select(x => new FormDataInputDto
            {
                SubeName = x.Key,
                //EmteaName = res.Veri.Where(u=>u.EmteaName.Contains(x.Key)).Select(a=>a.EmteaName).ToString(),
                //EmteaCode = res.Veri.Where(u => u.EmteaCode.Contains(x.Key)).FirstOrDefault().EmteaCode,
                //SubeCode = res.Veri.Where(u => u.SubeCode.Contains(x.Key)).FirstOrDefault().SubeCode,
                //CityName = string.Join(',', res.Veri.Where(u=>u.CityName.Contains(x.Key)).Select(u=>u.CityName).ToArray()),
               
            }).ToList();

            return JsonConvert.SerializeObject(res.Veri);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
