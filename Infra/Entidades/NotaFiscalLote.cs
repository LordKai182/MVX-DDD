using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("NotaFiscalLote", Schema = "dbo")]
    public class NotaFiscalLote
    {
        public NotaFiscalLote()
        {
            _NotaFiscal = new List<NotaFiscal>();
        }
        public string TipoImposto { get; set; }
        public int NotaFiscalLoteId { get; set; }
        public byte[] CorpoDocumento { get; set; }
        public int NumeroLote { get; set; }
        public DateTime DataCriacao { get; set; }
        public decimal ValorNota { get; set; }
        public string Cenario { get; set; }
        public string Plano { get; set; }
        public int QuantidadeRps { get; set; }
        public int EmpresaId { get; set; }
        public string Protocolo { get; set; }
        public string MensagemRetorno { get; set; }
        public virtual Empresa _Empresa { get; set; }
        public virtual ICollection<NotaFiscal> _NotaFiscal { get; set; }
    }
}
