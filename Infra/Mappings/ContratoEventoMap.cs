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
    public class ContratoEventoMap : IEntityTypeConfiguration<ContratoEvento>
    {
        public void Configure(EntityTypeBuilder<ContratoEvento> builder)
        {
            builder.ToTable("ContratoEvento");
            builder.HasKey(x => x.ContratoEventoId);
        }
    }
}
