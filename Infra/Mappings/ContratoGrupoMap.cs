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
    public class ContratoGrupoMap : IEntityTypeConfiguration<ContratoGrupo>
    {
        public void Configure(EntityTypeBuilder<ContratoGrupo> builder)
        {
            builder.ToTable("ContratoGrupo");
            builder.HasKey(x => x.ContratoGrupoId);
        }
    }
}
