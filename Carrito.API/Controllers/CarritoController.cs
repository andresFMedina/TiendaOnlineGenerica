using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Carrito.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Carrito.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        private readonly IRepositorioCarrito _repositorio;
        private readonly ILogger<CarritoController> _logger;

        public CarritoController(IRepositorioCarrito repositorio, ILogger<CarritoController> logger)
        {
            _repositorio = repositorio;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof (CarritoComprador), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<CarritoComprador>> GetCarritoCompradorByIdAsync(string id)
        {
            var carrito = await _repositorio.GetCarritoAsync(id);

            return Ok(carrito ?? new CarritoComprador(id));
        }

        [HttpPost]
        [ProducesResponseType(typeof (CarritoComprador), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<CarritoComprador>> ActualizarCarritoAsync([FromBody] CarritoComprador carrito) 
        {
            return Ok(await _repositorio.ActualizarCarritoAsync(carrito));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.OK)]
        public async Task BorrarCarritoAsync(string id)
        {
            await _repositorio.BorrarCarritoAsync(id);
        }
    }
}