using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("Plano", Schema = "dbo")]
    public class Plano
    {
        public Plano()
        {
            this._PlanoEmpresa = new List<PlanoEmpresa>();
            this._Contrato = new List<Contrato>();
        }
        public int PlanoId { get; set; }
        public string PlanoNome { get; set; }
        public string PlanoTipo { get; set; }
        public bool Ativado { get; set; }
        public string Boleto { get; set; }
        public bool Protocolado { get; set; }
       // public bool Prorrata { get; set; }
        public string NotaFiscal { get; set; }

        public virtual ICollection<PlanoEmpresa> _PlanoEmpresa { get; set; }
        public virtual ICollection<Contrato> _Contrato { get; set; }

    }
}
