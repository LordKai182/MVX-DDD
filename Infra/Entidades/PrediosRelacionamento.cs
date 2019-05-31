using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("PrediosRelacionamento", Schema = "dbo")]
    public class PrediosRelacionamento
    {
        public int PredioRelacionamentoId { get; set; }
        public int PredioBabelId { get; set; }
        public int PredioMundibrId { get; set; }
        public string UF { get; set; }
    }
}
