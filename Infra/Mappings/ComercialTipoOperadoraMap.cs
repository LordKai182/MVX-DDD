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
    public class ComercialTipoOperadoraMap : IEntityTypeConfiguration<ComercialTipoOperadora>
    {
        public void Configure(EntityTypeBuilder<ComercialTipoOperadora> builder)
        {
            builder.ToTable("ComercialTipoOperadora");
            builder.HasKey(x => x.id);
        }
    }
}
