using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsSysMobileREST.Areas.Api.Models
{
    public class Pedido
    {
        public int idPedido { get; set; }
        public string idCliente { get; set; }
        public string idVendedor { get; set; }
        public string fecha { get; set; }
        public decimal totalNeto { get; set; }
        public decimal totalFinal { get; set; }
        public List<PedidoItem> detallePedido { get; set; }

    }
}