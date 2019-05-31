using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("TipoImposto", Schema = "dbo")]
    public class TipoImposto
    {
        public int TipoImpostoId { get; set; }
        public string Nome { get; set; }
    }
}
