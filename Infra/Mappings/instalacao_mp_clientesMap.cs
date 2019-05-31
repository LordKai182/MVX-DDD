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
    class instalacao_mp_clientesMap : IEntityTypeConfiguration<instalacao_mp_clientes>
    {
        public void Configure(EntityTypeBuilder<instalacao_mp_clientes> builder)
        {
            builder.ToTable("instalacao_mp_clientes");
            builder.HasKey(x => x.cod_mp_cliente);
        }
    }
}
