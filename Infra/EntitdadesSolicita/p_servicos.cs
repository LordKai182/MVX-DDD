using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntitdadesSolicita
{
    [Table("p_servicos", Schema = "public")]
    public class p_servicos
    {
        
        public int id { get; set; }

       
        public string servico { get; set; }

        [ForeignKey("_p_classes"), Column("classe")]
        public int classe { get; set; }

        public virtual p_classes _p_classes { get; set; }

        public float? valor { get; set; }
    }
}
