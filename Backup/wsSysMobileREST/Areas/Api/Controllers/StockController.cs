using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using wsSysMobileREST.Areas.Api.Models;

namespace wsSysMobileREST.Areas.Api.Controllers
{
    public class StockController : Controller
    {
        private DaoStock daoStock;

        public StockController() 
        {
            daoStock = CsShared.getInstance().getDatamanager().getDaoStock();
        }

        // GET /Api/getStockD
        [HttpGet]
        public JsonResult getStockD(string idArticulo)
        {
            return Json(daoStock.getStock(idArticulo), JsonRequestBehavior.AllowGet);
        }

        // GET /Api/getStock
        [HttpGet]
        public String getStock(string idArticulo)
        {
            JsonResult jr = Json(daoStock.getStock(idArticulo), JsonRequestBehavior.AllowGet);
            string json = new JavaScriptSerializer().Serialize(jr.Data);
            return Util.compressString(json);
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
