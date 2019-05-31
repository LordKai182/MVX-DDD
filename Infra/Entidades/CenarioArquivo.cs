using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Entidades
{
    [Table("CenarioArquivo", Schema = "dbo")]
    public class CenarioArquivo
    {
        public int CenarioArquivoId { get; set; }
        public int CenarioId { get; set; }
        public DateTime dataCriacao { get; set; }
        public string faturado { get; set; }
        public string responsavel { get; set; }
        public string plano { get; set; }
        public string cnpjCpf { get; set; }
        public string cliente { get; set; }
        public decimal Valor { get; set; }
        public string tipoArquivo { get; set; }
        public string RemessaLote { get; set; }
        public string NumeroDocumento { get; set; }
        public byte[] CorpoDocumento { get; set; }
        public string Observacao { get; set; }
        public int? Vencimento { get; set; }

        #region RELCIONAMENTO
        public virtual Cenario _CenarioDados { get; set; }
        #endregion
    }
}
