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
    public class EmpresaEnderecoMap : IEntityTypeConfiguration<EmpresaEndereco>
    {
        public void Configure(EntityTypeBuilder<EmpresaEndereco> builder)
        {
            builder.ToTable("EmpresaEndereco");
            builder.HasKey(x => x.EmpresaEnderecoId);
        }
    }
}
