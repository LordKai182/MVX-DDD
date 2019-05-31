using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntidadesMundidata
{
    public class provisioning_configuracao
    {
        public int? cod_configuracao { get; set; }
        public int? cod_funcionario { get; set; }
        public int? cod_empresa { get; set; }
        public int? cod_servico_par { get; set; }
        public int? cod_dslam_link { get; set; }
        public int? cod_tipo_rede { get; set; }
        public int? cod_ip { get; set; }
        public string dsc_ip { get; set; }
        public string dsc_mascara { get; set; }
        public string dsc_gateway { get; set; }
        public int? num_vpi_cpe { get; set; }
        public int? num_vci_cpe { get; set; }
        public int? num_vpi_dslam { get; set; }
        public int? num_vci_dslam { get; set; }
        public string dsc_observacao { get; set; }
        public int? ind_ipv6 { get; set; }
        public int? vlan { get; set; }

    }
}
