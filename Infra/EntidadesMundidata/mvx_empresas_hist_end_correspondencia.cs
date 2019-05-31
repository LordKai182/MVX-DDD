using System;

namespace Infra.EntidadesMundidata
{
    public class mvx_empresas_hist_end_correspondencia
    {
        public int cod_emp_hist_end_corr { get; set; }
        public int cod_empresa { get; set; }
        public int cod_predio { get; set; }
        public int cod_pref_logradouro { get; set; }
        public int cod_logradouro { get; set; }
        public int num_endereco { get; set; }
        public int cod_bairro { get; set; }
        public int cod_cidade { get; set; }
        public int cod_ufe { get; set; }
        public int num_bloco_old { get; set; }
        public int num_andar { get; set; }
        public string dsc_sala { get; set; }
        public string dsc_cep { get; set; }
        public string num_bloco { get; set; }

        public int igual_comercial { get; set; }
        public int igual_cobranca { get; set; }
        public int cod_funcionario { get; set; }
        public DateTime data { get; set; }
        public string complemento { get; set; }
    }
}
