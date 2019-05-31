using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("Estado", Schema = "dbo")]
    public class Estado
    {
       public Estado()
       {
         this.Cidades = new List<Cidade>();
       }
        public int EstadoId { get; set; }
        public string Estadonome { get; set; }
        public string Estadosigla { get; set; }
        public virtual ICollection<Cidade> Cidades { get; set; }
    }
}
