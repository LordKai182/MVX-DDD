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
    public class financeiro_painel_controleMap : IEntityTypeConfiguration<financeiro_painel_controle>
    {
        public void Configure(EntityTypeBuilder<financeiro_painel_controle> builder)
        {
            builder.ToTable("financeiro_painel_controle");
            builder.HasKey(x => x.cod_painel_controle);
        }
    }
}
