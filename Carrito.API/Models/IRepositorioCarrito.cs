using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Carrito.API.Models
{
    public interface IRepositorioCarrito
    {
        public Task<bool> BorrarCarritoAsync(string id);
        public IEnumerable<string> GetUsuarios();
        public Task<CarritoComprador> GetCarritoAsync(string compradorId);
        public Task<CarritoComprador> ActualizarCarritoAsync(CarritoComprador carrito);
    }
}
