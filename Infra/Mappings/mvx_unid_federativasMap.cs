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
    public class mvx_unid_federativasMap : IEntityTypeConfiguration<mvx_unid_federativas>
    {
        public void Configure(EntityTypeBuilder<mvx_unid_federativas> builder)
        {
            builder.ToTable("mvx_unid_federativas");
            builder.HasKey(x => x.cod_ufe);
        }
    }
}
