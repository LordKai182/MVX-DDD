using Infra.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Mappings
{
    public class StatusClienteMap : IEntityTypeConfiguration<StatusCliente>
    {
        public void Configure(EntityTypeBuilder<StatusCliente> builder)
        {
            builder.ToTable("StatusCliente");
            builder.HasKey(x => x.StatusClienteId);
        }
    }
}
