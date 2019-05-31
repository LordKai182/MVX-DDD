using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntidadesApi
{
    [Table("Funcionarios")]
    public  class ApiFuncionario
    {
        [Key]
        public int FuncionarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Sexo { get; set; }
        public int Salario { get; set; }
        public string Setor { get; set; }
    }
}
