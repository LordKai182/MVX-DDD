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
    public class ContratoNrcMap : IEntityTypeConfiguration<ContratoNrc>
    {
        public void Configure(EntityTypeBuilder<ContratoNrc> builder)
        {
            builder.ToTable("ContratoVinculado");
            builder.HasKey(x => x.ContratoNrcId);
        }
    }
}
