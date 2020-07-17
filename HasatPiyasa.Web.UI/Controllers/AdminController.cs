using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.DirectX.Common.Direct2D;
using HasatPiyasa.Business.Abstract;
using HasatPiyasa.Entity.Entity;
using HasatPiyasa.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        #region Emtea işlemleri

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

        #region Emtea Grup işlemleri

        [HttpGet]
        public ActionResult EmteaGroupList()
        {
            EmteaGoupAddModel model = new EmteaGoupAddModel
            {
                EmteaGroup = new EmteaGroups(),
                Emteas = LoadEmteas()
            };

            return View(model);
        }

        private List<SelectListItem> LoadEmteas()
        {
            List<SelectListItem> emteas = (from emtea in _emteaService.ListAllEmteas().Veri
                                           select new SelectListItem
                                           {
                                               Value = emtea.Id.ToString(),
                                               Text = emtea.EmteaName
                                           }
                        ).ToList();
            return emteas;
        }

        #endregion


    }
}
