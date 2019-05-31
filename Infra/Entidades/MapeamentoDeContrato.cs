using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("MapeamentoDeContrato", Schema = "dbo")]

    public class MapeamentoDeContrato
    {
        public MapeamentoDeContrato()
        {
        }
        public int MapeamentoDeContratoId { get; set; }
        public DateTime Datainstalacao { get; set; }
        public DateTime DataValidade { get; set; }
        public bool RenovacaoAutomatica { get; set; }
        public string TipoDeContrato { get; set; }
        public int DiasAtraso { get; set; }
        public int ContratoId { get; set; }
        public bool Movido { get; set; }
        public DateTime DataMovido { get; set; }
        public int? MapaFaturamentoId { get; set; }
        public decimal ValorContrato { get; set; }
        public virtual Contrato _Contrato { get; set; }
        public virtual MapaFaturamento _MapaFaturamento { get; set; }

    }
}
