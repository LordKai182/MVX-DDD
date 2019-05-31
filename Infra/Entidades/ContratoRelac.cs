using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("ContratoRelac", Schema = "dbo")]
    public class ContratoRelac
    {
        public int ContratoRelacionamentoId { get; set; }
        public int? ContratoId { get; set; }
        public int? ContratoNrcId { get; set; }
        public int MundibrId { get; set; }
        public int DadosBabelId { get; set; }
        public string CodEmpresaServico { get; set; }
        public int CodPar { get; set; }
        public string Empresa { get; set; }
        public string Imposto { get; set; }
        //public bool Vinculado { get; set; }
        public virtual Contrato _Contrato { get; set; }
        public virtual ContratoNrc _ContratoVinculado { get; set; }
    }
}
