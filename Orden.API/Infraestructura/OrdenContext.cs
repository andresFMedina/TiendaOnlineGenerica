using Microsoft.EntityFrameworkCore;
using Orden.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orden.API.Infraestructura
{
    public class OrdenContext: DbContext
    {
        public OrdenContext(DbContextOptions<OrdenContext> options): base(options)
        {

        }

        public DbSet<ItemOrden> ItemsOrden { get; set; }
        public DbSet<Models.Orden> Ordenes { get; set; }
        public DbSet<ResumenOrden> ResumenesOrden { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Orden>()
                .HasKey(o => o.NumeroOrden);

            modelBuilder.Entity<Models.Orden>()                
                .HasMany(o => o.Items)
                .WithOne();            

            base.OnModelCreating(modelBuilder);
        }
        
    }
}
