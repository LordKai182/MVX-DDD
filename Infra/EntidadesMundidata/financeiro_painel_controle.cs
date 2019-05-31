using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntidadesMundidata
{
    public class financeiro_painel_controle
    {
        public int cod_painel_controle { get; set; }
        public DateTime? data_atual { get; set; }
        public int? cod_funcionario { get; set; }
        public int? cod_empresa { get; set; }
        public int? dia_vencimento { get; set; }
        public int? ind_mau_pagador { get; set; }
        public string cod_contabil { get; set; }
        public string dsc_observacao { get; set; }
        public int? ind_pagamento_adiantado { get; set; }
        public int? cod_banco { get; set; }
        public string agencia { get; set; }
        public string conta { get; set; }
        public int? cedente_mvx { get; set; }
        public int? ind_especial { get; set; }
        public string obs_icms { get; set; }
        public string obs_iss { get; set; }
        public int? desconto { get; set; }
        public DateTime? dtfim_desconto { get; set; }
        //public int? ind_recibo { get; set; }
        //public int? ind_sub { get; set; }
        public int? cod_porcentagem { get; set; }
        public string obs_debito { get; set; }
        public string obs_mse { get; set; }
        public int? ind_debito { get; set; }
        //public double? p_irrf { get; set; }
        //public double? p_pis { get; set; }
        //public double? p_cofins { get; set; }
        //public double? p_csll { get; set; }
        //public double? p_inss { get; set; }
        public int? ind_multa { get; set; }
    }
}
