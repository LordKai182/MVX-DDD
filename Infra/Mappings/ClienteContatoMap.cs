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
    public class ClienteContatoMap : IEntityTypeConfiguration<ClienteContato>
    {
        public void Configure(EntityTypeBuilder<ClienteContato> builder)
        {
            builder.ToTable("ClienteContato");
            builder.HasKey(x => x.ClienteContatoId);
        }
    }
}
