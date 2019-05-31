using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Entidades
{
    [Table("Cenario", Schema = "dbo")]
    public class Cenario
    {

        public int CenarioId { get; set; }
        public string CenarioCodigo { get; set; }
        public string TipoCenario { get; set; }
        public decimal TotalFaturado { get; set; }
        public int QtdFaturado { get; set; }
        public decimal TotalSemFatura { get; set; }
        public int QtdSemFatura { get; set; }
        public int Vencimento { get; set; }
        public decimal SaldoEmAberto { get; set; }
        public int QtdEmAberto { get; set; }
        public int ProdutosNovos { get; set; }
        public int ProdutosModificados { get; set; }
        public DateTime DataCenario { get; set; }
        public virtual ICollection<Boleto> _BoletosDoCenario { get; set; }
        public virtual ICollection<NotaFiscal> _NotaFiscalDoCenario { get; set; }
        public virtual ICollection<CenarioArquivo> _CenarioArquivo{ get; set; }


    }
}
