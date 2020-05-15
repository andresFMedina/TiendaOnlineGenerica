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

        private Task<List<ItemCatalogo>> GetItemsByIdsAsync(string ids)
        {
            throw new NotImplementedException();
        }
    }
}
