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
    public class TipoLogradouroMap : IEntityTypeConfiguration<TipoLogradouro>
    {
        public void Configure(EntityTypeBuilder<TipoLogradouro> builder)
        {
            builder.ToTable("TipoLogradouro");
            builder.HasKey(x => x.TipoLogradouroId);
        }
    }
}
