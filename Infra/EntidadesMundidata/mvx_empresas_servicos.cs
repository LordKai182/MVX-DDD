using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntidadesMundidata
{
    public class mvx_empresas_servicos
    {

        public int? cod_empresa_servico { get; set; }
        public int? cod_empresa { get; set; }
        public int? cod_tipo_servico { get; set; }
        public int? cod_antigo_servico  { get; set; }
        public decimal? vlr_mensalidade { get; set; }
        public decimal? vlr_mensalidade_icms { get; set; }
        public decimal? vlr_mensalidade_iss { get; set; }
        public decimal? vlr_mensalidade_pis_cofins { get; set; }
        public decimal? vlr_instalacao { get; set; }
        public decimal? vlr_instalacao_icms { get; set; }
        public decimal? vlr_instalacao_iss { get; set; }
        public decimal? vlr_instalacao_pis_cofins { get; set; }
        public int? cod_servico_par { get; set; }
        public int? cod_responsavel { get; set; }
        public int? cod_vendedor { get; set; }
        public string dsc_observacao { get; set; }
        public string dsc_observacao_provisioning { get; set; }
        public double? num_desconto { get; set; }
        public int? cod_par_antigo_serv { get; set; }
        public int? cod_tipo_contratacao { get; set; }
        public int? cod_mes_gratuito { get; set; }
        public int? cod_resp_desconto { get; set; }
        public int? cod_motivo_proposta { get; set; }
        public int? prepago { get; set; }
        public int? velocidade_down { get; set; }
        public int? velocidade_up { get; set; }
        public int? cod_tecnologia { get; set; }
        public string ponta_b { get; set; }
        public string observacao_compl { get; set; }
        public DateTime? data_migracao { get; set; }
        public int? cod_tipo_mvx { get; set; }
        public int? cod_func_neg_predial { get; set; }
        public DateTime? data_exec_sbd { get; set; }

    }
}
