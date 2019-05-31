using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("ClienteConta", Schema = "dbo")]

    public class ClienteConta
    {
        public int ClienteContaId { get; set; }
        public int ClienteId { get; set; }
        public int Banco { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public string BancoNome { get; set; }

        public virtual Cliente _Cliente { get; set; }
    }
}
