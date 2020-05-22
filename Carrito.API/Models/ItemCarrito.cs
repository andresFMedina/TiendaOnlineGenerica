using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Carrito.API.Models
{
    public class ItemCarrito: IValidatableObject
    {
        public string Id { get; set; }
        public int ProductoId { get; set; }
        public string NombreProducto { get; set; }
        public decimal PrecioUnidad { get; set; }
        public decimal PrecioUnidadAntiguo { get; set; }
        public int Cantidad { get; set; }
        public string UrlImagen { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (Cantidad < 1)
            {
                results.Add(new ValidationResult("Numero inválido de unidades", new[] { "Cantidad" }));
            }

            return results;

        }
    }
}
