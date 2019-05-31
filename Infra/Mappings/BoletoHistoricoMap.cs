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
    public class BoletoHistoricoMap : IEntityTypeConfiguration<BoletoHistorico>
    {
        public void Configure(EntityTypeBuilder<BoletoHistorico> builder)
        {
            builder.ToTable("BoletoHistorico");
            builder.HasKey(x => x.BoletoHistoricoId);
        }
    }
}
