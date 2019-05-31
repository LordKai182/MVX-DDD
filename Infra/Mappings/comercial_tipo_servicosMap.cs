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
    public class comercial_tipo_servicosMap : IEntityTypeConfiguration<comercial_tipo_servicos>
    {
        public void Configure(EntityTypeBuilder<comercial_tipo_servicos> builder)
        {
            builder.ToTable("comercial_tipo_servicos");
            builder.HasKey(x => x.cod_tipo_servico);
        }
    }
}
