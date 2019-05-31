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
    public class comercial_tipo_clienteMap : IEntityTypeConfiguration<comercial_tipo_cliente>
    {
        public void Configure(EntityTypeBuilder<comercial_tipo_cliente> builder)
        {
            builder.ToTable("comercial_tipo_cliente");
            builder.HasKey(x => x.cpd_tipo_cliente);
        }
    }
}
