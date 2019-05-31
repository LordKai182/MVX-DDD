using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("ContratoRelacionamentoBR", Schema = "dbo")]
    public class ContratoRelacionamentoBR
    {
        public int ContratoRelacionamentoId { get; set; }
        public int ContratoId { get; set; }
        public int MundibrId { get; set; }
        public int DadosBabelId { get; set; }
        public string CodEmpresaServico { get; set; }
        public int CodPar { get; set; }
        



        public virtual Contrato _Contrato { get; set; }
    }
}
