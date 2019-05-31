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
    public class mvx_empresasMap : IEntityTypeConfiguration<mvx_empresas>
    {
        public void Configure(EntityTypeBuilder<mvx_empresas> builder)
        {
            builder.ToTable("mvx_empresas");
            builder.HasKey(x => x.cod_empresa);
        }
    }
}
