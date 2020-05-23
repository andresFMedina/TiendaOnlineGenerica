using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;

namespace Orden.API.Models
{
    public class Orden
    {
        public int NumeroOrden { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Pais { get; set; }
        public List<ItemOrden> Items { get; set; }
        public decimal Total { get; set; }
    }
}
