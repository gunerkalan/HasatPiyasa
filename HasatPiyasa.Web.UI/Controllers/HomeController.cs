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

            var grp = res.Veri.GroupBy(s => s.SubeName).ToList();
            var response = grp.Select(s => new FormDataInputDto
            {
                SubeName = s.Key,
                EmteaName =string.Join(',', s.Select(s=>s.EmteaName).Distinct().ToArray()),
                EmteaCode = string.Join(',', s.Select(s => s.EmteaCode).Distinct().ToArray()),
                SubeCode = s.FirstOrDefault(u => u.SubeName==s.Key).SubeCode,
                CityName = string.Join(',', s.Select(u=>u.CityName).Distinct().ToArray()),
               
            }).ToList();

            return JsonConvert.SerializeObject(response);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
