using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HasatPiyasa.Business.Abstract;
using HasatPiyasa.Core.Utilities.Enums;
using HasatPiyasa.Web.UI.Models;
using HasatPiyasa_Web_UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HasatPiyasa.Web.UI.Controllers
{
    public class ReportController : BaseController
    {
        private ISubeService _subeService;
        private IFormDataInputService _formDataInputService;

        public ReportController(IFormDataInputService formDataInputService, ISubeService subeService)
        {
            _formDataInputService = formDataInputService;
            _subeService = subeService;
        }

       [HttpGet]
       public ActionResult RiceGeneralReportBySube()
        {
            var model = new  HasaInputViewModel();
           
            return View(model);
        }

        [HttpGet]
        public async Task<object> ChooseFormDataInput()
        {
            var res = await _formDataInputService.GetReporFormDataGTable((int)DataInput.Data.Rice);
            return JsonConvert.SerializeObject(res.Veri);
        }

    }
}
