using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using wsSysMobileREST.Areas.Api.Models;

namespace wsSysMobileREST.Areas.Api.Controllers
{
    public class VendedoresController : Controller
    {

        private DaoVendedor daoVendedor;

        public VendedoresController()
        {
            daoVendedor = CsShared.getInstance().getDatamanager().getDaoVendedor() ;
        }
        
        // GET /Api/getVendedoresD
        [HttpGet]
        public JsonResult getVendedoresD()
        {
            return Json(daoVendedor.getVendedores(), JsonRequestBehavior.AllowGet);
        }
        
        // GET /Api/getVendedores
        [HttpGet]
        public String getVendedores()
        {
            JsonResult jr = Json(daoVendedor.getVendedores(), JsonRequestBehavior.AllowGet);
            string json = new JavaScriptSerializer().Serialize(jr.Data);
            return Util.compressString(json);
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
