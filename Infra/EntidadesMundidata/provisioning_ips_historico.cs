using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntidadesMundidata
{
    public class provisioning_ips_historico
    {
        public int? cod_ip_historico { get; set; }
        public int? cod_funcionario { get; set; }
        public DateTime? data_atual { get; set; }
        public int? cod_empresa { get; set; }
        public int? cod_servico_par { get; set; }
        public string dscp_ip { get; set; }
        public int? cod_ip_hist_tipo_acao { get; set; }
        public int? import { get; set; }
    }
}
