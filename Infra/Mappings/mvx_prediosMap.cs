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
    public class mvx_prediosMap : IEntityTypeConfiguration<mvx_predios>
    {
        public void Configure(EntityTypeBuilder<mvx_predios> builder)
        {
            builder.ToTable("mvx_predios");
            builder.HasKey(x => x.cod_predio);
        }
    }
}
