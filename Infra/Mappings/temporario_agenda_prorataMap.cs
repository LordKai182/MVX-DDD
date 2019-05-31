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
    public class temporario_agenda_prorataMap : IEntityTypeConfiguration<temporario_agenda_prorata>
    {
        public void Configure(EntityTypeBuilder<temporario_agenda_prorata> builder)
        {
            builder.ToTable("temporario_agenda_prorata");
            builder.HasKey(x => x.cod_empresa);
        }
    }
}
