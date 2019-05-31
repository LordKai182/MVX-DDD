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
    public class MapeamentoEventoMap : IEntityTypeConfiguration<MapeamentoEvento>
    {
        public void Configure(EntityTypeBuilder<MapeamentoEvento> builder)
        {
            builder.ToTable("MapeamentoEvento");
            builder.HasKey(x => x.MapeamentoEventoId);
        }
    }
}
