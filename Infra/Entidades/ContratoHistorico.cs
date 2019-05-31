using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("ContratoHistorico", Schema = "dbo")]
    public class ContratoHistorico
    {
        public int ContratoHistoricoId { get; set; }
        public DateTime DataHoraOcorrencia { get; set; }
        public string Acao { get; set; }
        public string Campo { get; set; }
        public string ValorAntigo { get; set; }
        public string ValorNovo { get; set; }
        public string NumeroSO { get; set; }
        public int UsuarioId { get; set; }
        public int ContratoId { get; set; }
        public virtual Usuario _Usuario { get; set; }
        public virtual Contrato _Contrato { get; set; }
    }
}
