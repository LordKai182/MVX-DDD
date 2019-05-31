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
    public class mvx_empresas_historico_enderecoMap : IEntityTypeConfiguration<mvx_empresas_historico_endereco>
    {
        public void Configure(EntityTypeBuilder<mvx_empresas_historico_endereco> builder)
        {
            builder.ToTable("mvx_empresas_historico_endereco");
            builder.HasKey(x => x.cod_hist_end_empresas);
        }
    }
}
