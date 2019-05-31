using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntidadesMundidata
{
    public class provisioning_configuracao_aux
    {
        public int? cod_aux_configuracao { get; set; }
        public int? cod_empresa { get; set; }
        public int? cod_servico_par { get; set; }
        public int? num_velocidade { get; set; }
        public string nom_equipamento { get; set; }
        public int? ind_instalado { get; set; }
        public int? ind_cpe { get; set; }
        public int? ind_dslam { get; set; }
        public int? ind_psax { get; set; }
        public int? ind_terminador { get; set; }
        public DateTime? hora_inicio { get; set; }
        public DateTime? hora_termino { get; set; }
        public string dsc_observacao { get; set; }
        public Int16? ind_mrtg { get; set; }
        public int? ind_e1 { get; set; }
        public int? ind_pabx { get; set; }
        public int? asn { get; set; }

    }
}
