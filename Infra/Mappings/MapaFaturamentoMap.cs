using Infra.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Mappings
{
    public class MapaFaturamentoMap : IEntityTypeConfiguration<MapaFaturamento>
    {
        public void Configure(EntityTypeBuilder<MapaFaturamento> builder)
        {
            builder.ToTable("MapaFaturamento");
            builder.HasKey(x => x.MapaFaturamentoId);
        }
    }
}
