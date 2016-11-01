using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsSysMobileREST.Areas.Api.Models
{
    public class Cliente

    {
	    public string codigo {get;set;}
	    public string codigoOpcional{get;set;}
	    public string razonSocial{get;set;}
        public string calleNroPisoDpto { get; set; }
        public string localidad { get; set; }
        public string cuit { get; set; }
        public byte iva { get; set; }
	    public byte claseDePrecio{get;set;} 
	    public double porcDto{get;set;}
        public string cpteDefault { get; set; }
        public string idVendedor { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        
    }
}