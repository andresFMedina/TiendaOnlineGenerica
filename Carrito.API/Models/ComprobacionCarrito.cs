using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carrito.API.Models
{
    public class ComprobacionCarrito
    {
        public string Ciudad { get; set; }
        public string Direccion { get; set; }
        public string Departamento { get; set; }
        public string Pais { get; set; }
        public string Comprador { get; set; }
        public Guid PeticionId { get; set; }
    }
}
