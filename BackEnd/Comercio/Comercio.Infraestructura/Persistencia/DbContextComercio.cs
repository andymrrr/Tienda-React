using Comercio.Dominio.Modelos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Infraestructura.Persistencia
{
    public class DbContextComercio : IdentityDbContext<Usuario>
    {
        public DbContextComercio(DbContextOptions<DbContextComercio> options): base(options)
        {}
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var nombreUsuario = "sistema";
            foreach (var entidad in ChangeTracker.Entries<ModeloBase>())
            {
                switch (entidad.State)
                {
                    case EntityState.Added:
                        entidad.Entity.FechaCreacion = DateTime.Now;
                        entidad.Entity.CreadoPor = nombreUsuario;
                        break;
                    case EntityState.Modified:
                        entidad.Entity.FechaUltimaModificacion = DateTime.Now;
                        entidad.Entity.ModificadoPor = nombreUsuario;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
           
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Categoria>()
                .HasMany(p => p.Productos)
                .WithOne(c => c.Categoria)
                .HasForeignKey(l => l.Idcategoria)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Producto>()
                .HasMany(C => C.Comentarios)
                .WithOne(P => P.Producto)
                .HasForeignKey(l => l.IdProducto)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Producto>()
                .HasMany(C => C.Imagenes)
                .WithOne(P => P.Producto)
                .HasForeignKey(l => l.IdProducto)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CarritoCompra>()
                .HasMany(C => C.CarritoCompraArticulos)
                .WithOne(C => C.CarritoCompra)
                .HasForeignKey(l => l.IdCarritoCompra)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Usuario>().Property(d => d.Id).HasMaxLength(36);
            builder.Entity<Usuario>().Property(d => d.NormalizedUserName).HasMaxLength(90);
            builder.Entity<IdentityRole>().Property(d => d.Id).HasMaxLength(36);
            builder.Entity<IdentityRole>().Property(d => d.NormalizedName).HasMaxLength(90);
        }
        public DbSet<Producto>? Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Imagen> Imagenes { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoArticulo> PedidoArticulos { get; set; }
        public DbSet<PedidoDireccion> PedidoDirecciones { get; set; }
        public DbSet<Comentario>? Comentarios { get; set; }
        public DbSet<CarritoCompra> CarritoCompras { get; set; }
        public DbSet<CarritoCompraArticulo> CarritoCompraArticulos { get; set; }
        public DbSet<Pais> Pais { get; set; }



    }
}
