using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntidadesMundidata
{
    public class provisioning_ipv4_historico
    {
        public int? cod_ip_historico { get; set; }
        public int? cod_ip { get; set; }
        public string dsc_ip { get; set; }
        public int? cod_rede { get; set; }
        public string dsc_rede { get; set; }
        public string dsc_mascara { get; set; }
        public DateTime? data_atual { get; set; }
        public int? cod_tipo_rede { get; set; }
        public int? cod_ip_hist_tipo_acao { get; set; }
        public int? cod_empresa_servico { get; set; }
        public int? cod_servico_par { get; set; }
        public int? cod_tipo_ser_generico { get; set; }
        public int? cod_empresa { get; set; }
        public int? cod_funcionario { get; set; }
        public string tipo { get; set; }
        public int? cod_predio { get; set; }

    }
}
