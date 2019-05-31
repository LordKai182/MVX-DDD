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
    public class TipoVipMap : IEntityTypeConfiguration<TipoVip>
    {
        public void Configure(EntityTypeBuilder<TipoVip> builder)
        {
            builder.ToTable("TipoVip");
            builder.HasKey(x => x.TipoVipId);
        }
    }
}
