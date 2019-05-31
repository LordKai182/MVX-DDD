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
    public class mvx_funcionarios_perfilMap : IEntityTypeConfiguration<mvx_funcionarios_perfil>
    {
        public void Configure(EntityTypeBuilder<mvx_funcionarios_perfil> builder)
        {
            builder.ToTable("mvx_funcionarios_perfil");
            builder.HasKey(x => x.cod_perfil);
        }
    }
}
