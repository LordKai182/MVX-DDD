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
    public class comercial_tipo_contratacaoMap : IEntityTypeConfiguration<comercial_tipo_contratacao>
    {
        public void Configure(EntityTypeBuilder<comercial_tipo_contratacao> builder)
        {
            builder.ToTable("comercial_tipo_contratacao");
            builder.HasKey(x => x.cod_tipo_contratacao);
        }
    }
}
