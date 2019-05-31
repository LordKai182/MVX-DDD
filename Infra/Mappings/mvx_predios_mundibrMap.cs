using Infra.EntidadesMundidata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Mappings
{
    public class mvx_predios_mundibrMap : IEntityTypeConfiguration<mvx_predios_mundibr>
    {
        public void Configure(EntityTypeBuilder<mvx_predios_mundibr> builder)
        {
            builder.ToTable("mvx_predios_mundibr");
            builder.HasKey(x => x.area);
        }
    }
}
