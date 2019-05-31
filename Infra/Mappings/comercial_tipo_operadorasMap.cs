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
    public class comercial_tipo_operadorasMap : IEntityTypeConfiguration<comercial_tipo_operadoras>
    {
        public void Configure(EntityTypeBuilder<comercial_tipo_operadoras> builder)
        {
            builder.ToTable("comercial_tipo_operadoras");
            builder.HasKey(x => x.cod_operadora);
        }
    }
}
