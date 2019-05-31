using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntidadesMundidata
{
    public class instalacao_mp_clientes
    {
        public int? cod_mp_cliente { get; set; }
        public DateTime data_atual { get; set; }
        public int? cod_funcionario { get; set; }
        public int? site_suverey { get; set; }
        public int? cod_empresa { get; set; }
        public int? cod_servico_par { get; set; }
        public int? cod_equipámento { get; set; }
        public int? num_porta { get; set; }
        public int? num_posicao_bloco { get; set; }
        public int? cod_tipo_modulacao { get; set; }
        public int? cod_status_porta { get; set; }
        public string dsc_contagem { get; set; }
        public string dsc_observacao { get; set; }
        public int? num_telco { get; set; }
        public int? num_slot { get; set; }
        public int? ind_colocation { get; set; }
        public int? cod_estrutura { get; set; }
        public string dsc_andar_vert { get; set; }
        public string dsc_pos_bloco { get; set; }
        public string dsc_pos_concentrador_vert { get; set; }
        public string dsc_bloco_interconexao_vert { get; set; }
    }
}
