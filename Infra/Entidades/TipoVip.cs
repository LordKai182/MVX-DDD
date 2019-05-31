using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("TipoVip", Schema = "dbo")]
    public class TipoVip
    {
        public int TipoVipId { get; set; }
        public string Nome { get; set; }
    }
}
