using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wsSysMobileREST.Areas.Api.Models;

namespace wsSysMobileREST.Areas.Api.Controllers
{
    public class RegistrosController : Controller
    {
        private DaoRegistro daoRegistro;

        public RegistrosController() 
        {
            daoRegistro = CsShared.getInstance().getDatamanager().getDaoRegistro();
        }

        [HttpGet]
        public JsonResult getRegistros()
        {
            return Json(daoRegistro.obtenerRegistros(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
