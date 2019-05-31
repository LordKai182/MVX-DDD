using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("BoletoHistorico", Schema = "dbo")]
    public class BoletoHistorico
    {
        public int BoletoHistoricoId { get; set; }

        public DateTime DataHoraOcorrencia { get; set; }
        public string Acao { get; set; }
        public string Campo { get; set; }
        public string ValorAntigo { get; set; }
        public string ValorNovo { get; set; }
        public string NumeroSO { get; set; }
        public int UsuarioId { get; set; }
        public int BoletoId { get; set; }
        public virtual Usuario _Usuario { get; set; }
        public virtual Boleto _Boleto { get; set; }
    }
}
