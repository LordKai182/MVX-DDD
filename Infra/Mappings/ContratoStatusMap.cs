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
    public class ContratoStatusMap : IEntityTypeConfiguration<ContratoStatus>
    {
        public void Configure(EntityTypeBuilder<ContratoStatus> builder)
        {
            builder.ToTable("ContratoStatus");
            builder.HasKey(x => x.ContratoStatusId);
        }
    }
}
