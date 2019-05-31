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
    public class EventosDeContratoMap : IEntityTypeConfiguration<EventosDeContrato>
    {
        public void Configure(EntityTypeBuilder<EventosDeContrato> builder)
        {
            builder.ToTable("EventosDeContrato");
            builder.HasKey(x => x.EventosDeContratoId);
        }
    }
}
