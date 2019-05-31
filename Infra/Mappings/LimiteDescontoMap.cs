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
    public class LimiteDescontoMap : IEntityTypeConfiguration<LimiteDesconto>
    {
        public void Configure(EntityTypeBuilder<LimiteDesconto> builder)
        {
            builder.ToTable("LimiteDesconto");
            builder.HasKey(x => x.LimiteDescontoId);
        }
    }
}
