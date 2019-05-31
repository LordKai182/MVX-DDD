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
    public class comercial_status_servicosMap : IEntityTypeConfiguration<comercial_status_servicos>
    {
        public void Configure(EntityTypeBuilder<comercial_status_servicos> builder)
        {
            builder.ToTable("comercial_status_servicos");
            builder.HasKey(x => x.cod_status_serv);
        }
    }
}
