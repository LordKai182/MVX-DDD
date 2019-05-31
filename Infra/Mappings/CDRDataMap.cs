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
    public class CDRDataMap : IEntityTypeConfiguration<CDRData>
    {
        public void Configure(EntityTypeBuilder<CDRData> builder)
        {
            builder.ToTable("CDRData");
            builder.HasKey(x => x.cdr_id);
        }
    }
}
