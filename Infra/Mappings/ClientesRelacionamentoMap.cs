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
    public class ClientesRelacionamentoMap : IEntityTypeConfiguration<ClientesRelacionamento>
    {
        public void Configure(EntityTypeBuilder<ClientesRelacionamento> builder)
        {
            builder.ToTable("ClientesRelacionamento");
            builder.HasKey(x => x.ClientesRelacionamentoId);
        }
    }
}
