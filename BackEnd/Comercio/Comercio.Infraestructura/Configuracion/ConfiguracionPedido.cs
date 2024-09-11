using Comercio.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Infraestructura.Configuracion
{
    public class ConfiguracionPedido : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.OwnsOne(o => o.PedidoDireccion, x =>
            {
                x.WithOwner();
            });

            builder.HasMany(o => o.PedidoArticulos)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(e => e.Estatus)
                .HasConversion(
                    C => C.ToString(),
                    C => (EstatusPedidos)Enum.Parse(typeof(EstatusPedidos), C)
                );
        }
    }
}
