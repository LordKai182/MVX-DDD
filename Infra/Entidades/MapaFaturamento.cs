using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Entidades
{
    [Table("MapaFaturamento", Schema = "dbo")]
    public class MapaFaturamento
    {
        public MapaFaturamento()
        {
            this.DataCriacao = DateTime.Now;
            this._ContratoMovimentoFiscal = new List<ContratoMovimentoFiscal>();
        }
        public int MapaFaturamentoId { get; set; }
        public string Codigo { get; set; }
        public int? ContratoNrcId { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int Vencimento { get; set; }
        public int CodigoFiscal { get; set; }
        public int MesParcelas { get; set; }
        public int MesValidade { get; set; }
        public bool RenovacaoAutomatica { get; set; }
        public string RegraFaturamento { get; set; }
        public double Desconto { get; set; }
        public double Retensao { get; set; }
        public string Observacao { get; set; }
        public int? ContratoId { get; set; }
        public int PlanoId { get; set; }
        //public int? LgrFaturamrentoId { get; set; }
        //public int? LgrCorrespondenciaId { get; set; }
        public bool PrePago { get; set; }
        //public bool Vinculado { get; set; }
        public int? TipoClienteFaturamentoId { get; set; }

        public int? Tipo { get; set; } // 1= Debito , 2= Deposito 
        public int? ClienteContaId { get; set; }


        #region RELACIONAMENTOS
        public virtual Contrato _Contrato { get; set; }
        public virtual Plano _Plano { get; set; }
        public virtual TipoClienteFaturamento _TipoClienteFaturamento { get; set; }
        public virtual ICollection<ContratoMovimentoFiscal> _ContratoMovimentoFiscal { get; set; }
        #endregion

    }
}
