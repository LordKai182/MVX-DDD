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
    public class provisioning_configuracaoMap : IEntityTypeConfiguration<provisioning_configuracao>
    {
        public void Configure(EntityTypeBuilder<provisioning_configuracao> builder)
        {
            builder.ToTable("provisioning_configuracao");
            builder.HasKey(x => x.cod_configuracao);
        }
    }
}
