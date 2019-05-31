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
    public class BoletoRemessaMap : IEntityTypeConfiguration<BoletoRemessa>
    {
        public BoletoRemessaMap()
        {

        }

        public void Configure(EntityTypeBuilder<BoletoRemessa> builder)
        {
            builder.ToTable("BoletoRemessa");
            builder.HasKey(x => x.BoletoRemessaId);
        }
    }
}
