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
    public class ProdutoDetalheMap : IEntityTypeConfiguration<ProdutoDetalhe>
    {
        public void Configure(EntityTypeBuilder<ProdutoDetalhe> builder)
        {
            builder.ToTable("ProdutoDetalhe");
            builder.HasKey(x => x.ProdutoDetalheId);
        }
    }
}
