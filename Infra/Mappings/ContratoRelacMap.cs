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
    public class ContratoRelacMap : IEntityTypeConfiguration<ContratoRelac>
    {
        public void Configure(EntityTypeBuilder<ContratoRelac> builder)
        {
            builder.ToTable("ContratoRelac");
            builder.HasKey(x => x.ContratoRelacionamentoId);
        }
    }
}
