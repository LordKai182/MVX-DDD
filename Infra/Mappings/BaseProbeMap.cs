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
    public class BaseProbeMap : IEntityTypeConfiguration<BaseProbe>
    {
        public void Configure(EntityTypeBuilder<BaseProbe> builder)
        {
            builder.ToTable("BaseProbe");
            builder.HasKey(x => x.base_id);
        }
    }
}
