using Carrito.API.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Carrito.API.Infraestructura.Repositorio
{
    public class RepositorioCarrito : IRepositorioCarrito
    {
        private readonly ILogger<RepositorioCarrito> _logger;
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public RepositorioCarrito(ILoggerFactory loggerFactory, ConnectionMultiplexer redis)
        {
            _logger = loggerFactory.CreateLogger<RepositorioCarrito>();
            _redis = redis;
            _database = redis.GetDatabase();
        }
        public async Task<CarritoComprador> ActualizarCarritoAsync(CarritoComprador carrito)
        {
            var created = await _database.StringSetAsync(carrito.CompradorId, JsonConvert.SerializeObject(carrito));

            if (created)
            {
                _logger.LogInformation("Problema en persistencia");
                return null;
            }

            _logger.LogInformation("Persistencia Correcta");
            return await GetCarritoAsync(carrito.CompradorId);
        }

        public async Task<bool> BorrarCarritoAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public async Task<CarritoComprador> GetCarritoAsync(string compradorId)
        {
            var data = await _database.StringGetAsync(compradorId);

            if (data.IsNullOrEmpty)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<CarritoComprador>(data);
        }

        public IEnumerable<string> GetUsuarios()
        {
            var server = GetServer();
            var data = server.Keys();

            return data?.Select(k => k.ToString());
        }

        private IServer GetServer()
        {
            var endpoint = _redis.GetEndPoints();
            return _redis.GetServer(endpoint.First());
        }
    }
}
