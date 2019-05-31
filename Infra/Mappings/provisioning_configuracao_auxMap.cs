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
    public class provisioning_configuracao_auxMap : IEntityTypeConfiguration<provisioning_configuracao_aux>
    {
        public void Configure(EntityTypeBuilder<provisioning_configuracao_aux> builder)
        {
            builder.ToTable("provisioning_configuracao_aux");
            builder.HasKey(x => x.cod_aux_configuracao);
        }
    }
}
