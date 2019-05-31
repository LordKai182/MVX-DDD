using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("nf_resposta", Schema = "dbo")]
    public class nf_resposta
    {
        public int NfRespostaId { get; set; }
        public int Numero { get; set; }
        public int Serie { get; set; }
        public int Tipo { get; set; }
        public string Codigo { get; set; }
        public string Mensagem { get; set; }
        //public virtual nota_fiscal _NotaFiscal { get; set; }
    }
}
