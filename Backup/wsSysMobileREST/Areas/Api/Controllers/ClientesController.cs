using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using wsSysMobileREST.Areas.Api.Models;

namespace wsSysMobileREST.Areas.Api.Controllers
{
    public class ClientesController : Controller
    {
        private DaoCliente daoCliente;

        public ClientesController() 
        {
            daoCliente = CsShared.getInstance().getDatamanager().getDaoCliente();
        }

        
        // GET /Api/getClientesD/{pagina}
        [HttpGet]
        public String getClientesD(int? pagina)
        {

            JsonResult jr = Json(daoCliente.getClientes(pagina), JsonRequestBehavior.AllowGet);
            
            JavaScriptSerializer jSS = new JavaScriptSerializer();
            jSS.MaxJsonLength = 2147483647;
            
            string json = jSS.Serialize(jr.Data);
            return json;
        }

        
        // GET /Api/getClientes/{pagina}
        [HttpGet]
        public String getClientes(int? pagina)
        {
           
            JsonResult jr = Json(daoCliente.getClientes(pagina), JsonRequestBehavior.AllowGet);

            JavaScriptSerializer jSS = new JavaScriptSerializer();
            jSS.MaxJsonLength = 2147483647;
            string json = jSS.Serialize(jr.Data);

            return Util.compressString(json);
        }

       
        public ActionResult Index()
        {
            return View();
        }

    }
}
