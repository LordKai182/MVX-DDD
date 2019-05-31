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
    class mvx_empresas_dados_atuaisMap : IEntityTypeConfiguration<mvx_empresas_dados_atuais>
    {
        public void Configure(EntityTypeBuilder<mvx_empresas_dados_atuais> builder)
        {
            builder.ToTable("mvx_empresas_dados_atuais");
            builder.HasKey(x => x.cod_dado_atual);
        }
    }
}
