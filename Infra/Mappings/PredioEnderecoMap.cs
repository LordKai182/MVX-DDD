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
    public class PredioEnderecoMap : IEntityTypeConfiguration<PredioEndereco>
    {
        public void Configure(EntityTypeBuilder<PredioEndereco> builder)
        {
            builder.ToTable("PredioEndereco");
            builder.HasKey(x => x.PredioEnderecoId);
        }
    }
}
