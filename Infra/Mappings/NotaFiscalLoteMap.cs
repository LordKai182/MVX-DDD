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
    public class NotaFiscalLoteMap : IEntityTypeConfiguration<NotaFiscalLote>
    {
        public void Configure(EntityTypeBuilder<NotaFiscalLote> builder)
        {
            builder.ToTable("NotaFiscalLote");
            builder.HasKey(x => x.NotaFiscalLoteId);
        }
    }
}
