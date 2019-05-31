using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntidadesNaoPersistidas
{
    public class AcessoUsuario
    {
        public int UsuarioId { get; set; }
        public string UsuarioNome { get; set; }
        public string UsuarioToken { get; set; }
        public string UsuarioEmail { get; set; }

    }
}
