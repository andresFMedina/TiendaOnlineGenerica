using Catalogo.API.Infraestructura;
using Catalogo.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Catalogo.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogoController : ControllerBase
    {
        private readonly CatalogoContext _catalogoContext;

        public CatalogoController(CatalogoContext context)
        {
            _catalogoContext = context ?? throw new ArgumentNullException(nameof(context));
        }
        [HttpGet]
        [Route("items")]
        [ProducesResponseType(typeof(ItemsPaginadosViewModel<ItemCatalogo>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<ItemCatalogo>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ItemsAsync([FromQuery]int tamanoPagina = 10, [FromQuery]int indicePagina = 0, string ids = null)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = await GetItemsByIdsAsync(ids);

                if (!items.Any())
                {
                    return BadRequest("Valor de los ids invalido, deben estar separados por una coma ,");
                }

                return Ok(items);
            }

            var itemsTotales = await _catalogoContext.ItemsCatalogo
                .LongCountAsync();

            var itemsPaginados = await _catalogoContext.ItemsCatalogo
                .OrderBy(i => i.Nombre)
                .Skip(tamanoPagina * indicePagina)
                .Take(tamanoPagina)
                .ToListAsync();

            var model = new ItemsPaginadosViewModel<ItemCatalogo>(indicePagina, tamanoPagina, itemsTotales, itemsPaginados);

            return Ok(model);
        }

        private async Task<List<ItemCatalogo>> GetItemsByIdsAsync(string ids)
        {
            var numsIds = ids.Split(',').Select(id => (Ok: int.TryParse(id, out int x), Value: x));
            if(!numsIds.All(nid => nid.Ok))
            {
                return new List<ItemCatalogo>();
            }

            var idsToSelect = numsIds.Select(id => id.Value);

            var items = await _catalogoContext.ItemsCatalogo.Where(ci => idsToSelect.Contains(ci.Id)).ToListAsync();

            return items;
        }

        // GET api/v1/[controller]/items/withname/samplename[?pageSize=3&pageIndex=10]
        [HttpGet]
        [Route("items/connombre/{nombre:minlength(1)}")]
        [ProducesResponseType(typeof(ItemsPaginadosViewModel<ItemCatalogo>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ItemsPaginadosViewModel<ItemCatalogo>>> ItemsPorNombreAsync(string nombre, [FromQuery]int tamanoPagina = 10, [FromQuery]int indicePagina = 0)
        {
            var itemsTotales = await _catalogoContext.ItemsCatalogo
                .Where(i => i.Nombre.StartsWith(nombre))
                .LongCountAsync();

            var itemsPaginados = await _catalogoContext.ItemsCatalogo
                .Where(i => i.Nombre.StartsWith(nombre))
                .Skip(tamanoPagina * indicePagina)
                .Take(tamanoPagina)
                .ToListAsync();

            return new ItemsPaginadosViewModel<ItemCatalogo>(indicePagina, tamanoPagina, itemsTotales, itemsPaginados);
        }

        [HttpGet]
        [Route("items/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ItemCatalogo), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<ItemCatalogo>> GetItemByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var item = await _catalogoContext.ItemsCatalogo.SingleOrDefaultAsync(i => i.Id == id);

            if (item != null)
            {
                return item;
            }

            return NotFound();
        }

        [HttpGet]
        [Route("items/tipo/{tipoCatalogoId}/marca/{marcaCatalogoId:int?}")]
        [ProducesResponseType(typeof(ItemsPaginadosViewModel<ItemCatalogo>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ItemsPaginadosViewModel<ItemCatalogo>>> GetItemsByTipoIdYMarcaId(int tipoCatalogoId, int? marcaCatalogoId, [FromQuery]int tamanoPagina = 10, [FromQuery]int indicePagina = 0)
        {
            var root = (IQueryable<ItemCatalogo>)_catalogoContext.ItemsCatalogo;

            root = root.Where(i => i.TipoCatalogoId == tipoCatalogoId);

            if (marcaCatalogoId.HasValue)
            {
                root = root.Where(i => i.MarcaCatalogoId == marcaCatalogoId);
            }

            var itemsTotales = await root.LongCountAsync();

            var itemsPaginados = await root
                .Skip(tamanoPagina * indicePagina)
                .Take(tamanoPagina)
                .ToListAsync();

            return new ItemsPaginadosViewModel<ItemCatalogo>(indicePagina, tamanoPagina, itemsTotales, itemsPaginados);
        }

        // GET api/v1/[controller]/items/type/all/brand[?pageSize=3&pageIndex=10]
        [HttpGet]
        [Route("items/tipo/todos/marca/{marcaCatalogoId:int?}")]
        [ProducesResponseType(typeof(ItemsPaginadosViewModel<ItemCatalogo>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ItemsPaginadosViewModel<ItemCatalogo>>> GetItemsByMarcaIdAsync (int? marcaCatalogoId, [FromQuery]int tamanoPagina = 10, [FromQuery]int indicePagina = 0)
        {
            var root = (IQueryable<ItemCatalogo>)_catalogoContext.ItemsCatalogo;

            if (marcaCatalogoId.HasValue)
            {
                root = root.Where(i => i.MarcaCatalogoId == marcaCatalogoId);
            }

            var itemsTotales = await root.LongCountAsync();

            var itemsPaginados = await root
                .Skip(tamanoPagina * indicePagina)
                .Take(tamanoPagina)
                .ToListAsync();

            return new ItemsPaginadosViewModel<ItemCatalogo>(indicePagina, tamanoPagina, itemsTotales, itemsPaginados);

        }

        // GET api/v1/[controller]/CatalogTypes
        [HttpGet]
        [Route("tipocatalogo")]
        [ProducesResponseType(typeof (List<TipoCatalogo>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<TipoCatalogo>>> GetTiposCatalogoAsync()
        {
            var tiposCatalogo = await _catalogoContext.TiposCatalogo.ToListAsync();

            return tiposCatalogo;
        }


        // GET api/v1/[controller]/CatalogBrands

        [HttpGet]
        [Route("marcascatalogo")]
        [ProducesResponseType(typeof(List<MarcaCatalogo>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<MarcaCatalogo>>> GetMarcaCatologoAsync()
        {
            var marcaCatalogo = await _catalogoContext.MarcasCatalogo.ToListAsync();

            return marcaCatalogo;
        }

        [Route("items")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]

        public async Task<ActionResult> CrearItemAsync([FromBody]ItemCatalogo producto)
        {
            var item = new ItemCatalogo
            {
                MarcaCatalogoId = producto.MarcaCatalogoId,
                TipoCatalogoId = producto.TipoCatalogoId,
                Descripcion = producto.Descripcion,
                Nombre = producto.Nombre,
                NombreArchivoFoto = producto.NombreArchivoFoto,
                Precio = producto.Precio
            };

            _catalogoContext.ItemsCatalogo.Add(item);

            await _catalogoContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItemByIdAsync), new { id = item.Id }, null);
        }

        [Route("items")]       
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> ModificarItemAsync([FromBody]ItemCatalogo productoAModificar)
        {
            var itemCatalogo = await _catalogoContext.ItemsCatalogo.SingleOrDefaultAsync(i => i.Id == productoAModificar.Id);

            if(itemCatalogo == null)
            {
                return NotFound();
            }

            itemCatalogo = productoAModificar;
            _catalogoContext.ItemsCatalogo.Update(itemCatalogo);

            await _catalogoContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItemByIdAsync), new { id = productoAModificar.Id }, null);
        }

        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> BorrarItemAsync(int id)
        {
            var item = _catalogoContext.ItemsCatalogo.SingleOrDefault(i => i.Id == id);

            if(item == null)
            {
                return NotFound();
            }

            _catalogoContext.ItemsCatalogo.Remove(item);

            await _catalogoContext.SaveChangesAsync();

            return NoContent();
        }



    }
}
