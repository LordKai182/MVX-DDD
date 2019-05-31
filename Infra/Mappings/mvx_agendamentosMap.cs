using Infra.EntidadesMundidata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Mappings
{
    public class mvx_agendamentosMap : IEntityTypeConfiguration<mvx_agendamentos>
    {
        public void Configure(EntityTypeBuilder<mvx_agendamentos> builder)
        {
            builder.ToTable("mvx_agendamentos");
            builder.HasKey(x => x.cod_agendamento);
        }
    }
}
