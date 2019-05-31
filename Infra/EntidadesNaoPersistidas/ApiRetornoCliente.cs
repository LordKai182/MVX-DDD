using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntidadesNaoPersistidas
{
    public class ApiRetornoCliente
    {
        public string Codigo { get; set; }
        public int ClienteId { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string IE { get; set; }
        public int Solicitacoes { get; set; }
        public int Ativos { get; set; }
        public int Cancelados { get; set; }
    }
}
