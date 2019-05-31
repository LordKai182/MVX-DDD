using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Entidades
{
    [Table("FaturamentoIdentificado", Schema = "dbo")]
    public  class FaturamentoIdentificado
    {
        public int FaturamentoIdentificadoId { get; set; }                
        public long NossoNumeroBoleto { get; set; }
        public string CodigoBoleto { get; set; }
        public DateTime? Vencimento { get; set; }
        public DateTime? Vencimento2 { get; set; }
        public decimal ValorBoleto { get; set; }
        public decimal ValorPago { get; set; }
        public decimal JurosDeMora { get; set; }
        public string CpfCnpjSacado { get; set; }
        public DateTime? DataPagamento { get; set; }
        public int? StatusPagamento { get; set; }        
        public int? FaturamentoRegraId { get; set; }
        public int? BoletoId { get; set; }

        public DateTime DataCadastro { set; get; }
        public int? UsuarioId { set; get; }

        //Foreing Keys
        public virtual Usuario Usuario { get; set; }
        public virtual Boleto Boleto { get; set; }
       // public virtual FaturamentoRegra FaturamentoRegra { get; set; }        
    }
}
