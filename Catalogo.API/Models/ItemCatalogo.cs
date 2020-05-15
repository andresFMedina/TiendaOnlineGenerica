using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogo.API.Models
{
    public class ItemCatalogo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string NombreArchivoFoto { get; set; }
        public string UriFoto { get; set; }
        public int TipoCatalogoId { get; set; }
        public TipoCatalogo TipoCatalogo { get; set; }
        public int MarcaCatalogoId { get; set; }
        public MarcaCatalogo MarcaCatalogo { get; set; }
        public int CantidadDisponibles { get; set; }
        public int LimiteReabastecimiento { get; set; }
        public int CantidadMaximaInventario { get; set; }

    }
}
