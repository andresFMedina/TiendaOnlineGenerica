using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orden.API.Infraestructura;
using Orden.API.Models;

namespace Orden.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenController : ControllerBase
    {
        private readonly OrdenContext _context;

        public OrdenController(OrdenContext context)
        {
            _context = context;
        }

        // GET: api/Orden
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ResumenOrden>), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ResumenOrden>>> GetResumenesOrdenAsync()
        {
            var items = await _context.ResumenesOrden.OrderBy(ro => ro.NumeroOrden).ToListAsync();
            return Ok(items);
        }

        // GET: api/Orden/5
        [HttpGet("{numeroorden}")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Models.Orden), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<Models.Orden>> GetOrdenByNumeroOrdenAsync(int numeroorden)
        {
            var orden = await _context.Ordenes.FirstOrDefaultAsync(o => o.NumeroOrden == numeroorden);

            if(orden == null)
            {
                return NotFound();
            }

            return Ok(orden);
        }

        // POST: api/Orden
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        public async Task<ActionResult> PostOrdenAsync([FromBody] Models.Orden orden)        
        {
            _context.Ordenes.Add(orden);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrdenByNumeroOrdenAsync), new { numeroorden = orden.NumeroOrden }, null);
        }

        // PUT: api/Orden/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
