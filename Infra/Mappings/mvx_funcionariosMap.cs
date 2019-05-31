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
    public class mvx_funcionariosMap : IEntityTypeConfiguration<mvx_funcionarios>
    {
        public void Configure(EntityTypeBuilder<mvx_funcionarios> builder)
        {
            throw new NotImplementedException();
        }
    }
}
