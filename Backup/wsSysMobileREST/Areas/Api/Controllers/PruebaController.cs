using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wsSysMobileREST.Areas.Api.Controllers
{
    public class PruebaController : Controller
    {
        //
        // GET: /Api/Prueba/

        [HttpGet]
        public String pruebaFuncionamiento()
        {

            return "wsSysMobileRest: Funcionando";
        }


        public ActionResult Index()
        {
            return View();
        }

    }
}
