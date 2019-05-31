using Infra.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Mappings
{
    public class LinhaCobrancaMap : IEntityTypeConfiguration<LinhaCobranca>
    {   
        

        public void Configure(EntityTypeBuilder<LinhaCobranca> builder)
        {
            builder.ToTable("LinhaCobranca");
            builder.HasKey(x => x.LinhaCobrancaId);
        }
    }
}
