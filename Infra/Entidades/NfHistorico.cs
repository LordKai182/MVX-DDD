using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("NfHistorico", Schema = "dbo")]
    public class NfHistorico
    {
        public int NfHistoricoId { get; set; }

        public DateTime DataHoraOcorrencia { get; set; }
        public string Acao { get; set; }
        public string Campo { get; set; }
        public string ValorAntigo { get; set; }
        public string ValorNovo { get; set; }
        public string NumeroSO { get; set; }
        public int UsuarioId { get; set; }
        public int NotaFiscalId { get; set; }
        public virtual Usuario _Usuario { get; set; }
        public virtual NotaFiscal _NotaFiscal { get; set; }

    }
}
