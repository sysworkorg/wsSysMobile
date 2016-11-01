using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using wsSysMobileREST.Areas.Api.Models;

namespace wsSysMobileREST.Areas.Api.Controllers
{
    public class DepositosController : Controller
    {

        private DaoDeposito daoDeposito;

        public DepositosController() 
        {
            daoDeposito = CsShared.getInstance().getDatamanager().getDaoDeposito();
        }

        // GET /Api/getDepositosD
        [HttpGet]
        public JsonResult getDepositosD()
        {
            return Json(daoDeposito.getDepositos(), JsonRequestBehavior.AllowGet);
        }
        
        // GET /Api/getDepositos
        [HttpGet]
        public String getDepositos()
        {
            JsonResult jr = Json(daoDeposito.getDepositos(), JsonRequestBehavior.AllowGet);
            string json = new JavaScriptSerializer().Serialize(jr.Data);

            return Util.compressString(json);
        }


        public ActionResult Index()
        {
            return View();
        }

    }
}
