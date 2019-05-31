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
    public class PrediosRelacionamentoMap : IEntityTypeConfiguration<PrediosRelacionamento>
    {
        public void Configure(EntityTypeBuilder<PrediosRelacionamento> builder)
        {
            builder.ToTable("PrediosRelacionamento");
            builder.HasKey(x => x.PredioRelacionamentoId);
        }
    }
}
