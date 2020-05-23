using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Orden.API.Models
{
    public class ItemOrden
    {
        public string NombreProducto { get; set; }
        public int Unidades { get; set; }
        public int PrecioUnidad { get; set; }
        public string ProductoUrl { get; set; }
    }
}
