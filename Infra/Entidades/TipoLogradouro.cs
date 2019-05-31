using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("TipoLogradouro", Schema = "dbo")]
    public  class TipoLogradouro
    {
        public int TipoLogradouroId { get; set; }
        public string Nome { get; set; }

    }
}
