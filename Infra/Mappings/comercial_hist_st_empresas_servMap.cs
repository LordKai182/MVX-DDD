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
    public class comercial_hist_st_empresas_servMap : IEntityTypeConfiguration<comercial_hist_st_empresas_serv>
    {
        public void Configure(EntityTypeBuilder<comercial_hist_st_empresas_serv> builder)
        {
            builder.ToTable("comercial_hist_st_empresas_serv");
            builder.HasKey(x => x.cod_hist_empresa_serv);
        }
    }
}
