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
    public class ClienteRelacaoMap : IEntityTypeConfiguration<ClienteRelacao>
    {
        public void Configure(EntityTypeBuilder<ClienteRelacao> builder)
        {
            builder.ToTable("ClienteRelacao");
            builder.HasKey(x => x.ClienteRelacaoId);
        }
    }
}
