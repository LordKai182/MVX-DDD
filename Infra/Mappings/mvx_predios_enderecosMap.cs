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
    public class mvx_predios_enderecosMap : IEntityTypeConfiguration<mvx_predios_enderecos>
    {
        public void Configure(EntityTypeBuilder<mvx_predios_enderecos> builder)
        {
            builder.ToTable("mvx_predios_enderecos");
            builder.HasKey(x => x.cod_endereco_predio);
        }
    }
}
