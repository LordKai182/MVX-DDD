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
    public class mvx_predios_aluguelMap : IEntityTypeConfiguration<mvx_predios_aluguel>
    {
        public void Configure(EntityTypeBuilder<mvx_predios_aluguel> builder)
        {
            builder.ToTable("mvx_predios_aluguel");
            builder.HasKey(x => x.cod_predio_aluguel);
        }
    }
}
