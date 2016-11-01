using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsSysMobileREST.Areas.Api.Models
{
    public class Articulo
    {
        public string idArticulo {get;set;}
        public string descripcion {get;set;}
        public string idRubro {get;set;}
        public double iva {get;set;}
        public decimal impuestosInternos {get;set;}
        public bool exento {get;set;}
        public decimal precio1 { get; set; }
        public decimal precio2 { get; set; }
        public decimal precio3 { get; set; }
        public decimal precio4 { get; set; }
        public decimal precio5 { get; set; }
        public decimal precio6 { get; set; }
        public decimal precio7 { get; set; }
        public decimal precio8 { get; set; }
        public decimal precio9 { get; set; }
        public decimal precio10 { get; set; }

    }
}