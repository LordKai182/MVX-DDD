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
    public class MapaFaturamentoComplementoMap : IEntityTypeConfiguration<MapaFaturamentoComplemento>
    {
        public void Configure(EntityTypeBuilder<MapaFaturamentoComplemento> builder)
        {
            builder.ToTable("MapaFaturamentoComplemento");
            builder.HasKey(x => x.MapaFaturamentoComplementoId);
        }
    }
}
