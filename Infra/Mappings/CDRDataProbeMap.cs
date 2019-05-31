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
    public class CDRDataProbeMap : IEntityTypeConfiguration<CDRDataProbe>
    {
        public void Configure(EntityTypeBuilder<CDRDataProbe> builder)
        {
            builder.ToTable("CDRDataProbe");
            builder.HasKey(x => x.base_id);
        }
    }
}
