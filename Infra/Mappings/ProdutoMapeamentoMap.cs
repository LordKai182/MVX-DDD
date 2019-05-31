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
    public class ProdutoMapeamentoMap : IEntityTypeConfiguration<ProdutoMapeamento>
    {
        public void Configure(EntityTypeBuilder<ProdutoMapeamento> builder)
        {
            builder.ToTable("ProdutoMapeamento");
            builder.HasKey(x => x.ProdutoMapeamentoId);
        }
    }
}
