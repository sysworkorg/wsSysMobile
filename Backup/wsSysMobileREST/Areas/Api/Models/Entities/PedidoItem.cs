using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsSysMobileREST.Areas.Api.Models
{
    public class PedidoItem
    {
        public int idPedido { get; set; }
        public string idArticulo { get; set; }
        public decimal cantidad { get; set; }
        public decimal importeUnitario { get; set; }
        public decimal porcDto { get; set; }
        public decimal total { get; set; }

    }
}