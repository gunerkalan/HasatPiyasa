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
        private IEmteaGroupService _emteaGroupService;
        private IEmteaTypeService _emteaTypeService;

        public AdminController(IEmteaService emteaService, IEmteaGroupService emteaGroupService, IEmteaTypeService emteaTypeService)
        {
            _emteaService = emteaService;
            _emteaGroupService = emteaGroupService;
            _emteaTypeService = emteaTypeService;
            
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

        [HttpGet]
        public async Task<object> EmteaGroupListData()
        {
            var res = await _emteaGroupService.GetEmteaGroupTable();
            return JsonConvert.SerializeObject(res.Veri);
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

        #region EmteaTip işlemleri

        [HttpGet]
        public ActionResult EmteaTypeList()
        {
            var model = new EmteTypeAddModel
            {
                EmteaTypes = new EmteaTypes()
            };

            return View(model);
        }

        //ListDatasıGelecek

        #endregion


    }
}
