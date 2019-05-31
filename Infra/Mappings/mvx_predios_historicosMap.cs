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
    public class mvx_predios_historicosMap : IEntityTypeConfiguration<mvx_predios_historicos>
    {
        public void Configure(EntityTypeBuilder<mvx_predios_historicos> builder)
        {
            builder.ToTable("mvx_predios_historicos");
            builder.HasKey(x => x.cod_hist_predio);
        }
    }
}
