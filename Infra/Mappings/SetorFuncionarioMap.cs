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
    public class SetorFuncionarioMap : IEntityTypeConfiguration<SetorFuncionario>
    {
        public void Configure(EntityTypeBuilder<SetorFuncionario> builder)
        {
            builder.ToTable("SetorFuncionario");
            builder.HasKey(x => x.SetorFuncionarioId);
        }
    }
}
