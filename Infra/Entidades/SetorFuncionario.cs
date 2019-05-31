using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("SetorFuncionario", Schema = "dbo")]
    public class SetorFuncionario
    {
        public int SetorFuncionarioId { get; set; }
        public string SetorFuncionarioNome { get; set; }
    }
}
