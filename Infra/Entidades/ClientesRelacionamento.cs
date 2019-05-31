using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("ClientesRelacionamento", Schema = "dbo")]
    public class ClientesRelacionamento
    {

        public int ClientesRelacionamentoId { get; set; }
        public string VozbabelId { get; set; }
        public string DadosbabelId { get; set; }
        public int ClienteMundibrId { get; set; }
    }
}
