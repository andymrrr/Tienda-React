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
    public class ConfiguracionPedidoArticulo : IEntityTypeConfiguration<PedidoArticulo>
    {
        public void Configure(EntityTypeBuilder<PedidoArticulo> builder)
        {
            builder.Property(p => p.Precio).HasColumnType("decimal(10,2)");
        }
    }
}
