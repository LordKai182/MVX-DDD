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
    public class PropriedadeProdutoMap : IEntityTypeConfiguration<PropriedadeProduto>
    {
        public void Configure(EntityTypeBuilder<PropriedadeProduto> builder)
        {
            builder.ToTable("PropriedadeProduto");
            builder.HasKey(x => x.PropriedadeProdutoId);
        }
    }
}
