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
    public class ContratoHistoricoMap : IEntityTypeConfiguration<ContratoHistorico>
    {
        public void Configure(EntityTypeBuilder<ContratoHistorico> builder)
        {
            builder.ToTable("ContratoHistorico");
            builder.HasKey(x => x.ContratoHistoricoId);
        }
    }
}
