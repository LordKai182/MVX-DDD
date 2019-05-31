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
    public class BancoVencimentoMap : IEntityTypeConfiguration<BancoVencimento>
    {
        public void Configure(EntityTypeBuilder<BancoVencimento> builder)
        {
            builder.ToTable("BancoVencimento");
            builder.HasKey(x => x.BancoVencimentoId);
        }
    }
}
