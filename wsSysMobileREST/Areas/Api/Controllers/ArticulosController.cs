using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using wsSysMobileREST.Areas.Api.Models;

namespace wsSysMobileREST.Areas.Api.Controllers
{
    public class ArticulosController : Controller
    {
        private DaoArticulo daoArticulo;

        public ArticulosController() 
        {

            daoArticulo = CsShared.getInstance().getDatamanager().getDaoArticulo() ;

        }

        // GET /Api/getArticulosD/{pagina}
        [HttpGet]
        public JsonResult getArticulosD(int? pagina)
        {
            return Json(daoArticulo.getArticulos(pagina), JsonRequestBehavior.AllowGet);
        }
        
        // GET /Api/getArticulos/{pagina}
        [HttpGet]
        public String getArticulos(int? pagina)
        {
            JsonResult jr = Json(daoArticulo.getArticulos(pagina), JsonRequestBehavior.AllowGet);
            string json = new JavaScriptSerializer().Serialize(jr.Data);
            return Util.compressString(json);
        }
        

        public ActionResult Index()
        {
            return View();
        }


    }
}
