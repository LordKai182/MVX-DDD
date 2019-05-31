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
    public class CenarioTesteMap : IEntityTypeConfiguration<CenarioTeste>
    {
        public void Configure(EntityTypeBuilder<CenarioTeste> builder)
        {
            builder.ToTable("CenarioTeste");
            builder.HasKey(x => x.CenarioTesteID);
        }
    }
}
