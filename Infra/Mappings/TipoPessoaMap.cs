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
    public class TipoPessoaMap : IEntityTypeConfiguration<TipoPessoa>
    {
        public void Configure(EntityTypeBuilder<TipoPessoa> builder)
        {
            builder.ToTable("TipoPessoa");
            builder.HasKey(x => x.TipopessoaId);
        }
    }
}
