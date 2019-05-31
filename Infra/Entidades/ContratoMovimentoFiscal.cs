using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("ContratoMovimentoFiscal", Schema = "dbo")]
    public class ContratoMovimentoFiscal
    {
        public int ContratoMovimentoFiscalId { get; set; }
        public int MapaFaturamentoId { get; set; }
        public DateTime DataMovimentacao { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal ValorContrato { get; set; }
       // public decimal ValorPago { get; set; }
        public decimal Desconto { get; set; }
        public decimal Acrescimo { get; set; }
        public string ContratoCodigo { get; set; }
        public string CpfCnpj { get; set; }
        public bool Refaturado { get; set; }
        public int ContratoId { get; set; }
       // public decimal Encargos { get; set; }
        public string Tipo { get; set; }
        public int LinhaCobrancaId { get; set; }

    }
}
