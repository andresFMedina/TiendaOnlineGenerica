using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orden.API.Models
{
    public class ResumenOrden
    {
        public int NumeroOrden { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
    }
}
