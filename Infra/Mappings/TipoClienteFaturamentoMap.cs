﻿using Infra.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Mappings
{
    public class TipoClienteFaturamentoMap : IEntityTypeConfiguration<TipoClienteFaturamento>
    {
        public void Configure(EntityTypeBuilder<TipoClienteFaturamento> builder)
        {
            builder.ToTable("TipoClienteFaturamento");
            builder.HasKey(x => x.TipoClienteFaturamentoId);
                
        }
    }
}
