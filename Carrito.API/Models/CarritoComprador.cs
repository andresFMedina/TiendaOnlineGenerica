using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carrito.API.Models
{
    public class CarritoComprador
    {
        public string CompradorId { get; set; }
        public List<ItemCarrito> Items { get; set; }
        public CarritoComprador()
        {

        }
        public CarritoComprador(string clienteId)
        {
            CompradorId = clienteId;
        }
    }
}
