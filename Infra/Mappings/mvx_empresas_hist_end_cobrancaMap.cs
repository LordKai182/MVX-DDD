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
    class mvx_empresas_hist_end_cobrancaMap : IEntityTypeConfiguration<mvx_empresas_hist_end_cobranca>
    {
        public void Configure(EntityTypeBuilder<mvx_empresas_hist_end_cobranca> builder)
        {
            builder.ToTable("mvx_empresas_hist_end_cobranca");
            builder.HasKey(x => x.cod_emp_hist_end_cob);
        }
    }
}
