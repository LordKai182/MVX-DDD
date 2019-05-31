using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("Usuario", Schema = "dbo")]
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Login { get; set; }
        public string Nome { get; set; }
        public int Status { get; set; }
        public DateTime? DataCadastro { get; set; }
        public string Responsavel { get; set; }
        public string Email { get; set; }
        public int? SetorFuncionarioId { get; set; }
        public string GuidIdentity { get; set; }
        public string Observacao { get; set; }
        public int? FuncionarioCargoId { get; set; }

        //Foreing Keys
        public virtual SetorFuncionario _SetorFuncionario { get; set; }
        
        public virtual FuncionarioCargo _FuncionarioCargo { get; set; }
        

    }
}
