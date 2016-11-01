using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using wsSysMobileREST.Areas.Api.Models;

namespace wsSysMobileREST.Areas.Api.Controllers
{
    public class RubrosController : Controller
    {
        private DaoRubro daoRubro;

        public RubrosController() 
        {
            daoRubro = CsShared.getInstance().getDatamanager().getDaoRubro() ;
        }

        // GET /Api/getRubrosD
        [HttpGet]
        public JsonResult getRubrosD()
        {
            return Json(daoRubro.getRubros(), JsonRequestBehavior.AllowGet);
        }
        
        // GET /Api/getRubros
        [HttpGet]
        public String getRubros()
        {
            JsonResult jr = Json(daoRubro.getRubros(), JsonRequestBehavior.AllowGet);
            string json = new JavaScriptSerializer().Serialize(jr.Data);

            return Util.compressString(json);
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
