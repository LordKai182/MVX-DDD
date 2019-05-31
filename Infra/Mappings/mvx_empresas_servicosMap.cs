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
    public class mvx_empresas_servicosMap : IEntityTypeConfiguration<mvx_empresas_servicos>
    {
        public void Configure(EntityTypeBuilder<mvx_empresas_servicos> builder)
        {
            builder.ToTable("mvx_empresas_servicos");
            builder.HasKey(x => x.cod_empresa_servico);
        }
    }
}
