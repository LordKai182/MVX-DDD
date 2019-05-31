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
    public class mvx_cidadesMap : IEntityTypeConfiguration<mvx_cidades>
    {
        public void Configure(EntityTypeBuilder<mvx_cidades> builder)
        {
            builder.ToTable("mvx_cidades");
            builder.HasKey(x => x.cod_cidade);
        }
    }
}
