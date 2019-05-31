using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("LinhaCobranca", Schema = "dbo")]
    public class LinhaCobranca
    {
        public LinhaCobranca()
        {
            this.DataCadastro = DateTime.Now;
        }


        public int LinhaCobrancaId { get; set; }
        public int CenarioId { get; set; }
        public decimal ValorFaturado { get; set; }
        public string Tipo { get; set; }
        public int ClienteId { get; set; }
        public bool Refaturado { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
        public decimal ValorPago { get; set; }
        public decimal Encargos { get; set; }
        public DateTime DataPagamento { get; set; }
        public bool Complemento { get; set; }
        public DateTime DataComplemento { get; set; }
        public string MesCompetencia { get; set; }
        public string AnoCompetencia { get; set; }



        public virtual Cliente _Cliente { get; set; }
        public virtual Cenario _Cenario { get; set; }
        public virtual ICollection<Boleto> _Boleto { get; set; }
        public virtual ICollection<NotaFiscal> _NotaFiscal { get; set; }
        public virtual ICollection<ContratoMovimentoFiscal> _ContratoMovimentoFiscal { get; set; }


    }
}
