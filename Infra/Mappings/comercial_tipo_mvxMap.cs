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
    public class comercial_tipo_mvxMap : IEntityTypeConfiguration<comercial_tipo_mvx>
    {
        public void Configure(EntityTypeBuilder<comercial_tipo_mvx> builder)
        {
            builder.ToTable("comercial_tipo_mvx");
            builder.HasKey(x => x.cod_tipo_mvx);
        }
    }
}
