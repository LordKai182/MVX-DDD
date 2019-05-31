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
    public class PlanoEmpresaMap : IEntityTypeConfiguration<PlanoEmpresa>
    {
        public void Configure(EntityTypeBuilder<PlanoEmpresa> builder)
        {
            builder.ToTable("PlanoEmpresa");
            builder.HasKey(x => x.PlanoEmpresaId);
        }
    }
}
