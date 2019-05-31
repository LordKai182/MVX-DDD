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
    public class NfHistoricoMap : IEntityTypeConfiguration<NfHistorico>
    {
        public void Configure(EntityTypeBuilder<NfHistorico> builder)
        {
            builder.ToTable("NfHistorico");
            builder.HasKey(x => x.NfHistoricoId);
        }
    }
}
