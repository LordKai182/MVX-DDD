using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntidadesMundidata
{
    public class mvx_empresas_historico_endereco
    {
        public int cod_hist_end_empresas { get; set; }
        public int cod_empresa { get; set; }
        public string dsc_sala { get; set; }
        public int cod_endereco_predio { get; set; }
        public int ind_endereco { get; set; }
        public string dsc_cep { get; set; }
        public int num_andar { get; set; }
        public int num_bloco_old { get; set; }
        public string num_bloco { get; set; }
        public string complemento { get; set; }

    }
}
