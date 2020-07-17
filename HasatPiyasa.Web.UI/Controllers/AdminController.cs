using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.DirectX.Common.Direct2D;
using HasatPiyasa.Business.Abstract;
using HasatPiyasa.Entity.Entity;
using HasatPiyasa.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HasatPiyasa.Web.UI.Controllers
{
    public class AdminController : BaseController
    {
        private IEmteaService _emteaService;

        public AdminController(IEmteaService emteaService)
        {
            _emteaService = emteaService;
        }

      # region Emtea işlemleri

        [HttpGet]
        public ActionResult EmteaList()
        {
            EmteaModel model = new EmteaModel
            {
                Emtea = new Emteas()
            };

            return View(model);
        }

        [HttpGet]
        public object EmteaListData()
        {
            var res = _emteaService.ListAllEmteas().Veri;

            return JsonConvert.SerializeObject(res);
        }


     #endregion

        
    }
}
