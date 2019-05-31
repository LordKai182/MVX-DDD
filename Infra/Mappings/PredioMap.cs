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
    public class PredioMap : IEntityTypeConfiguration<Predio>
    {
        public void Configure(EntityTypeBuilder<Predio> builder)
        {
            builder.ToTable("Predio");
            builder.HasKey(x => x.PredioId);
        }
    }
}
