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
    public class comercial_tipo_ser_genericoMap : IEntityTypeConfiguration<comercial_tipo_ser_generico>
    {
        public void Configure(EntityTypeBuilder<comercial_tipo_ser_generico> builder)
        {
            builder.ToTable("comercial_tipo_ser_generico");
            builder.HasKey(x => x.cod_tipo_ser_generico);
        }
    }
}
