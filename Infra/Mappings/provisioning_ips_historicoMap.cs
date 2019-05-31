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
    public class provisioning_ips_historicoMap : IEntityTypeConfiguration<provisioning_ips_historico>
    {
        public void Configure(EntityTypeBuilder<provisioning_ips_historico> builder)
        {
            builder.ToTable("provisioning_ips_historico");
            builder.HasKey(x => x.cod_ip_historico);
        }
    }
}
