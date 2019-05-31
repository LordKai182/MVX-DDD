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
    public class MapeamentoFiscalMap : IEntityTypeConfiguration<MapeamentoFiscal>
    {
        public void Configure(EntityTypeBuilder<MapeamentoFiscal> builder)
        {
            builder.ToTable("MapeamentoFiscal");
            builder.HasKey(x => x.MapeamentoFiscalId);
        }
    }
}
