using System.Web.Mvc;

namespace wsSysMobileREST.Areas.Api
{
    public class ApiAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Api";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            //      LAS RUTAS QUE DEBEMOS GENERAR SON:
            //
            //      /test   (*)                                               Sin Acceso a base de datos,  
            //                                                             solo determina si el ws esta activo
            //
            //  GETTERS
            //
            //      /getRegistros  (*)
            //      /getDepositos  (*)
            //      /getRubros     (*)
            //      /getVendedores (*)
            //
            //      /getArticulo/{idArticulo}
            //      /getArticulos/{pagina}                                  pagina=null (Trae todos)
            //
            //      /getCliente/{idCliente}
            //      /getClientes/{pagina}                                   pagina=null (Trae todos)
            //
            //      /getCuentaCorriente/{cliente}/{fechaDesde}/{fechaHasta}
            //      /getCuentaStock/{idArticulo}
            //
            //  SETTERS
            //
            //      /setPedido              POST:un solo objeto pedido
            //      /setPedidos             POST:un Array de objetos pedido

            context.MapRoute("AccesoPrueba", "Api/Test", new { controller = "Prueba", action = "pruebaFuncionamiento" });
            
            context.MapRoute("obtieneRegistros", "Api/getRegistros", new { controller = "Registros", action = "getRegistros" });
            
            context.MapRoute("obtieneDepositos", "Api/getDepositos", new { controller = "Depositos", action = "getDepositos" });
            context.MapRoute("obtieneDepositosDescomprimidos", "Api/getDepositosD", new { controller = "Depositos", action = "getDepositosD" });

            context.MapRoute("obtieneRubros", "Api/getRubros", new { controller = "Rubros", action = "getRubros" });
            context.MapRoute("obtieneRubrosDescomprimidos", "Api/getRubrosD", new { controller = "Rubros", action = "getRubrosD" });

            context.MapRoute("obtieneVendedores", "Api/getVendedores", new { controller = "Vendedores", action = "getVendedores" });
            context.MapRoute("obtieneVendedoresDescomprimidos", "Api/getVendedoresD", new { controller = "Vendedores", action = "getVendedoresD" });


            context.MapRoute("obtieneArticulos", "Api/getArticulos/{pagina}", new { controller = "Articulos", action = "getArticulos", pagina = UrlParameter.Optional });
            context.MapRoute("obtieneArticulosDescomprimidos", "Api/getArticulosD/{pagina}", new { controller = "Articulos", action = "getArticulosD", pagina = UrlParameter.Optional });

            context.MapRoute("obtieneClientes", "Api/getClientes/{pagina}", new { controller = "Clientes", action = "getClientes", pagina = UrlParameter.Optional });
            context.MapRoute("obtieneClientesDescomprimidos", "Api/getClientesD/{pagina}", new { controller = "Clientes", action = "getClientesD", pagina = UrlParameter.Optional });

            
            context.MapRoute("obtieneStock", "Api/getStock/{idArticulo}", new { controller = "Stock", action = "getStock", idArticulo = UrlParameter.Optional });
            context.MapRoute("obtieneStockDescomprimido", "Api/getStockD/{idArticulo}", new { controller = "Stock", action = "getStockD", idArticulo = UrlParameter.Optional });

            context.MapRoute("obtieneCtaCte", "Api/getCtaCte/{cliente}/{fechaDesde}/{fechaHasta}",
                                new
                                    {
                                        controller = "CtaCte",
                                        action = "getCtaCte",
                                        cliente = UrlParameter.Optional,
                                        fechaDesde = UrlParameter.Optional,
                                        fechaHasta = UrlParameter.Optional
                                    }
                            );


            context.MapRoute("GrabarPedidos", "Api/setPedidos", new { controller = "Pedidos", action = "setPedidos", jSonPedidos = UrlParameter.Optional });
            context.MapRoute("GrabaPedido", "Api/setPedido", new { controller = "Pedidos", action = "setPedido", jSonPedido = UrlParameter.Optional });


            context.MapRoute
            (
                "Api_default",
                "Api/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
