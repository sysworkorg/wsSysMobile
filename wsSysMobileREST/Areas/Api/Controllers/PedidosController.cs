using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wsSysMobileREST.Areas.Api.Models;

namespace wsSysMobileREST.Areas.Api.Controllers
{
    public class PedidosController : Controller
    {
        private DaoPedido daoPedido;

        public PedidosController() 
        {
            daoPedido = CsShared.getInstance().getDatamanager().getDaoPedido();
        }

        
        public string setPedidos(string jsonPedidos)
        {

            StreamReader str = new StreamReader(Request.InputStream);
            String contenido = str.ReadToEnd().ToString();

            bool resultadoProceso = daoPedido.procesaJsonPedidos(contenido);

            if (resultadoProceso) 
                return "1";
            else
                return "0";
        
        }
        
        public string setPedido(string jsonPedido)
        {

            StreamReader str = new StreamReader(Request.InputStream);
            String contenido = str.ReadToEnd().ToString();

            bool resultadoProceso = daoPedido.procesaJsonPedido(contenido);

            if (resultadoProceso)
                return "1";
            else
                return "0";

        }

        //
        // GET: /Api/grabarPedidos/
        public ActionResult Index()
        {
            return View();
        }

    }
}