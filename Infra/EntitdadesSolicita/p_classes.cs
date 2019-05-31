using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntitdadesSolicita
{
    [Table("p_classes", Schema = "public")]
    public class p_classes
    {
        public int id { get; set; }

        public string classe { get; set; }

        public string tipo { get; set; }

        public string classeab { get; set; }

        public int tipo_caso_oc { get; set; }

        //public virtual IList<SoServico> Servicos { get; set; }

        //public virtual IList<SoPropostaProduto> PropostasProdutos { get; set; }
    }
}
