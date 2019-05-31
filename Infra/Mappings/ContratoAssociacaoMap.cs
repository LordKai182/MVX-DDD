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
    public class ContratoAssociacaoMap : IEntityTypeConfiguration<ContratoAssociacao>
    {
        public void Configure(EntityTypeBuilder<ContratoAssociacao> builder)
        {
            builder.ToTable("ContratoAssociacao");
            builder.HasKey(x => x.ContratoAssociacaoId);
        }
    }
}
