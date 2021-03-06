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
    public class BauMap : IEntityTypeConfiguration<Bau>
    {
        public void Configure(EntityTypeBuilder<Bau> builder)
        {
            builder.ToTable("Bau");
            builder.HasKey(x => x.BauId);
        }
    }
}
