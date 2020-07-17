using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HasatPiyasa.Web.UI.Controllers
{
    public class AdminController : Controller
    {
      # region Emtea işlemleri

        public ActionResult EmteaList()
        {
            return View();
        }


     #endregion

        
    }
}
