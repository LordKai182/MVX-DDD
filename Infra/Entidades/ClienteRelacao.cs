using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("ClienteRelacao", Schema = "dbo")]
    public class ClienteRelacao
    {
        public int ClienteRelacaoId { get; set; }
        public int ClienteId { get; set; }
        public int RelacionamentoId { get; set; }
        public int TipoRelacionamentoId { get; set; }
        public string Codigo { get; set; }
    }
}
