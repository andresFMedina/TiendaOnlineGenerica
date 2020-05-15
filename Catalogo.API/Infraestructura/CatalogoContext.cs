using Catalogo.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogo.API.Infraestructura
{
    public class CatalogoContext : DbContext
    {
        public CatalogoContext(DbContextOptions<CatalogoContext> options) : base(options)
        {
        }
        public DbSet<ItemCatalogo> ItemsCatalogo { get; set; }
        public DbSet<MarcaCatalogo> MarcasCatalogo { get; set; }
        public DbSet<TipoCatalogo> TiposCatalogo { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
        }
    }
}
