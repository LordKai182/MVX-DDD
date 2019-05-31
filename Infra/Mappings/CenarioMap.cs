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
    public class CenarioMap : IEntityTypeConfiguration<Cenario>
    {
        public void Configure(EntityTypeBuilder<Cenario> builder)
        {
            builder.ToTable("Cenario");
            builder.HasKey(x => x.CenarioId);
        }
    }
}
