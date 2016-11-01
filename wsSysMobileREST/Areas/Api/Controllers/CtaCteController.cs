using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wsSysMobileREST.Areas.Api.Models;

namespace wsSysMobileREST.Areas.Api.Controllers
{
    public class CtaCteController : Controller
    {
        private DaoCtaCte daoCtaCte;

        public CtaCteController() 
        {
            daoCtaCte = CsShared.getInstance().getDatamanager().getDaoCtaCte();
        }

        // GET /Api/getCtaCte/{cliente}/{fechaDesde}/{fechaHasta}
        [HttpGet]
        public JsonResult getCtaCte(String cliente,String fechaDesde,String fechaHasta)
        {
            return Json(daoCtaCte.getCtaCte(cliente,fechaDesde,fechaHasta), JsonRequestBehavior.AllowGet);
        }
        
        
        public ActionResult Index()
        {
            return View();
        }

    }
}
