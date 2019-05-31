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
    public class BaseCDRMap : IEntityTypeConfiguration<BaseCDR>
    {
        public void Configure(EntityTypeBuilder<BaseCDR> builder)
        {
            builder.ToTable("BaseCDR");
            builder.HasKey(x => x.base_id);
        }
    }
}
