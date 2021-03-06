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
    public class ContratoDetalheMap : IEntityTypeConfiguration<ContratoDetalhe>
    {
        public void Configure(EntityTypeBuilder<ContratoDetalhe> builder)
        {
            builder.ToTable("ContratoDetalhe");
            builder.HasKey(x => x.ContratoDetalheId);
        }
    }
}
