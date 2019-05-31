using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("TipoPessoa", Schema = "dbo")]
    public class TipoPessoa
    {
        public int TipopessoaId { get; set; }
        public string Nome { get; set; }
    }
}
