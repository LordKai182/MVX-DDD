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
    public class provisioning_ipv6_historicoMap : IEntityTypeConfiguration<provisioning_ipv6_historico>
    {
        public void Configure(EntityTypeBuilder<provisioning_ipv6_historico> builder)
        {
            builder.ToTable("provisioning_ipv6_historico");
            builder.HasKey(x => x.cod_ip_historico);
        }
    }
}
