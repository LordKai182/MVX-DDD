using Infra.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings
{
    public class ClienteContaMap : IEntityTypeConfiguration<ClienteConta>
    {
        public void Configure(EntityTypeBuilder<ClienteConta> builder)
        {
            builder.ToTable("ClienteConta");
            builder.HasKey();
        }
    }
}
