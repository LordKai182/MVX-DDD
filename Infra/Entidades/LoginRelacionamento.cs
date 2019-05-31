using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("LoginRelacionamento", Schema = "dbo")]

    public class LoginRelacionamento
    {
        public int LoginRelacionamentoId { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public DateTime DataCadastro { get; set; }
        //PEGAR RESTANTES DA TABELA MVX FUNCIONARIOS
    }
}
