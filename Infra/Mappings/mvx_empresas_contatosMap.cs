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
    class mvx_empresas_contatosMap : IEntityTypeConfiguration<mvx_empresas_contatos>
    {
        public void Configure(EntityTypeBuilder<mvx_empresas_contatos> builder)
        {
            builder.ToTable("mvx_empresas_contatos");
            builder.HasKey(x => x.cod_empresa_contato);
        }
    }
}
