using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Entidades
{
    [Table("Bau", Schema = "dbo")]
    public class Bau
    {
        public int BauId { get; set; }
        public string TipoBau { get; set; }
        public string TipoArquivo { get; set; }
        public string Remessa { get; set; }
        public string Cenario { get; set; }
        public string CodigoBr { get; set; }
        public string CNPJ { get; set; }
        public string Cidade { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public string Detalhes { get; set; }
        public string SetorSolicitante { get; set; }
        public string Usuario { get; set; }
        public string Status { get; set; }
        public string Plano { get; set; }
        public string ObsDocumento { get; set; }
        public string Lote { get; set; }
        public DateTime? DataStatus { get; set; }
        public DateTime? DataSolicitada { get; set; }
    }
}
