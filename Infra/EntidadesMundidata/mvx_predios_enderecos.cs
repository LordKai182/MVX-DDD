using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntidadesMundidata
{
   public class mvx_predios_enderecos
    {
        public int? cod_endereco_predio { get; set; }
        public Int32? num_endereco { get; set; }
        public string num_cep { get; set; }
        public int? num_andar { get; set; }
        public string dsc_sala { get; set; }
        public int? cod_pref_logradouro { get; set; }
        public int? cod_logradouro { get; set; }
        public int? cod_bairro { get; set; }
        public int? cod_cidade { get; set; }
        public int? cod_ufe { get; set; }
        public int? cod_predio { get; set; }
        public int? num_bloco { get; set; }
        public string geolocal { get; set; }

    }
}
