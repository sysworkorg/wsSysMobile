using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsSysMobileREST.Areas.Api.Models
{
    public class Stock
    {
        public String idDeposito { get; set; }  //   Aqui envio GENERAL (sin filtro de depositos) // el ID del Deposito // COMPROMETIDO (notas de pedido pendientes de facturar)
        public double stock { get; set; }


    }
}