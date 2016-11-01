using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsSysMobileREST.Areas.Api.Models
{
    public class CtaCte
    {
        public string fecha { get; set; }
        public string tc { get; set; }
        public string sucNroLetra { get; set; }
        public string detalle { get; set; }
        public decimal importe { get; set; }
    }
}