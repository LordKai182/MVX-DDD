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
    public class mvx_bairrosMap : IEntityTypeConfiguration<mvx_bairros>
    {
        public void Configure(EntityTypeBuilder<mvx_bairros> builder)
        {
            builder.ToTable("mvx_bairros");
            builder.HasKey(x => x.cod_bairro);
        }
    }
}
