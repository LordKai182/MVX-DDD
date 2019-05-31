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
    public class FuncionarioCargoMap : IEntityTypeConfiguration<FuncionarioCargo>
    {
        public void Configure(EntityTypeBuilder<FuncionarioCargo> builder)
        {
            builder.ToTable("FuncionarioCargo");
            builder.HasKey(x => x.FuncionarioCargoId);
        }
    }
}
