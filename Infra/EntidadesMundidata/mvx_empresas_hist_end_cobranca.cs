using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntidadesMundidata
{
    public class mvx_empresas_hist_end_cobranca
    {
        public int? cod_emp_hist_end_cob { get; set; }
        public int? cod_empresa { get; set; }
        public int? cod_pref_logradouro { get; set; }
        public int? cod_logradouro { get; set; }
        public int? num_endereco { get; set; }
        public int? cod_bairro { get; set; }
        public int? cod_cidade { get; set; }
        public int? cod_ufe { get; set; }
        public int? num_bloco_old { get; set; }
        public int? num_andar { get; set; }
        public string dsc_sala { get; set; }
        public string dsc_cep { get; set; }
        public string num_bloco { get; set; }
        public string complemento { get; set; }
    }
}
