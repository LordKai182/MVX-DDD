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
    public class mvx_logradouros_prefMap : IEntityTypeConfiguration<mvx_logradouros_pref>
    {
        public void Configure(EntityTypeBuilder<mvx_logradouros_pref> builder)
        {
            builder.ToTable("mvx_logradouros_pref");
            builder.HasKey(x => x.cod_pref_logradouro);
        }
    }
}
