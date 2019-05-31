using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("StatusCliente", Schema = "dbo")]
    public class StatusCliente
    {
        public int StatusClienteId { get; set; }
        public string Nome { get; set; }
    }
}
