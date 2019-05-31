using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("ContratoGrupo", Schema = "dbo")]
    public class ContratoGrupo
    {
        public ContratoGrupo()
        {
            DataCriacao = DateTime.Now;
            this._Contrato = new List<Contrato>();
        }
        public int ContratoGrupoId { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public string Codigo { get; set; }
        public string RazaoNota  { get; set; }
        public string Observacoes { get; set; }
        public int LogradouroFaturamentoId { get; set; }
        public virtual ICollection<Contrato> _Contrato { get; set; }
    }
}
