using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntidadesMundidata
{
    public class mvx_predios_pnc
    {
        public int cod_predio_pnc { get; set; }
        public string logradouro { get; set; }
        public int numero { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string cep { get; set; }
        public int ind_ativo { get; set; }
        public int id_end { get; set; }
        public Int64 cod_so { get; set; }

    }
}
