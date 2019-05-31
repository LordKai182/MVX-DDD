using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Entidades
{
    [Table("NotaFiscal", Schema = "dbo")]
    public class NotaFiscal
    {
        public int NotaFiscalId { get; set; }
        public int CenarioId { get; set; }
        public DateTime DataAtual { get; set; }
        public string NumeroNota { get; set; }
        public int ClienteId { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime MesAnoPrestacao { get; set; }
        public decimal ValorAliquota { get; set; }
        public int NumeroRps { get; set; }
        public int NumeroLote { get; set; }
        public string NumeroNotaPrefeitura { get; set; }
        public string CodigoVerificacao { get; set; }
        public decimal ValorServicos { get; set; }
        public decimal ValorIss { get; set; }
        public decimal Aliquota { get; set; }
        public decimal ItemListaServico { get; set; }
        public string CodigoTributacaoMunicipio { get; set; }
        public string Discriminacao { get; set; }
        public string TipoImposto { get; set; }
        public byte[] PDF { get; set; }
        public int NotaFiscalLoteId { get; set; }
        public string Observacao { get; set; }
        public int LinhaCobrancaId { get; set; }
        public virtual NotaFiscalLote _NotaFiscalLote { get; set; }
    


    }
}
