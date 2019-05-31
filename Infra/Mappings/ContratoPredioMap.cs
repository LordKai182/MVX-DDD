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
    public class ContratoPredioMap : IEntityTypeConfiguration<ContratoPredio>
    {
        public void Configure(EntityTypeBuilder<ContratoPredio> builder)
        {
            builder.ToTable("ContratoPredio");
            builder.HasKey(x => x.ContratoPredioId);
        }
    }
}
