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
    public class ContratoMovimentoFiscalMap : IEntityTypeConfiguration<ContratoMovimentoFiscal>
    {
        public void Configure(EntityTypeBuilder<ContratoMovimentoFiscal> builder)
        {
            builder.ToTable("ContratoMovimentoFiscal");
            builder.HasKey(x => x.ContratoMovimentoFiscalId);
        }
    }
}
