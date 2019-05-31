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
    public class mvx_logradourosMap : IEntityTypeConfiguration<mvx_logradouros>
    {
        public void Configure(EntityTypeBuilder<mvx_logradouros> builder)
        {
            builder.ToTable("mvx_logradouros");
            builder.HasKey(x => x.cod_logradouro);
        }
    }
}
