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
    public class ProdutoHistoricoMap : IEntityTypeConfiguration<ProdutoHistorico>
    {
        public void Configure(EntityTypeBuilder<ProdutoHistorico> builder)
        {
            builder.ToTable("ProdutoHistorico");
            builder.HasKey(x => x.ProdutoHistoricoId);
        }
    }
}
