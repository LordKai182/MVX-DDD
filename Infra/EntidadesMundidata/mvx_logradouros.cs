using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntidadesMundidata
{
    public class mvx_logradouros
    {
        public int cod_logradouro { get; set; }
        public string dsc_logradouro { get; set; }
        public int cod_bairro { get; set; }
        public int cod_cidade { get; set; }
        public int cod_ufe { get; set; }
        public int ind_ativo { get; set; }

    }
}
