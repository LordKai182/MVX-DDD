using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("Cidade", Schema = "dbo")]
    public class Cidade
       {

            public Cidade()
            {
                _Bairro = new List<Bairro>();
            } 
           public int CidadeId { get; set; }
           public string Nome { get; set; }
           public Int32 CodIbge { get; set; }
           public int? EstadoId { get; set; }
           public virtual Estado _Estado { get; set; }
           public virtual IEnumerable<Bairro> _Bairro { get; set; }
    }      

   }
