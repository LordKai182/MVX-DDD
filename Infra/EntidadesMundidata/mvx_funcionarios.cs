using System;

namespace Infra.EntidadesMundidata
{
    public class mvx_funcionarios 
    {
        public int? cod_funcionario { get; set; }
        public string nom_funcionario { get; set; }
        public int? cod_perfil { get; set; }
        public int? ind_ativo { get; set; }
        public int? num_matricula { get; set; }
        public int? cod_nivel { get; set; }
        public string dsc_iniciais { get; set; }
        public string username { get; set; }
        public string senha { get; set; }
        public DateTime? dat_nascimento { get; set; }
        public DateTime? dat_admissao { get; set; }
        public string num_ramal { get; set; }
        public string num_telefone { get; set; }
        public string num_celular1 { get; set; }
        public string num_celular2 { get; set; }
        public int? ind_bloqueio { get; set; }
        public int? cod_atendente { get; set; }
        public string dsc_foto { get; set; }
        public string dsc_mail { get; set; }
        public string dsc_observacao { get; set; }
        public int? cod_func_cargo { get; set; }
        public int? cod_func_chefia { get; set; }
        public string dsc_rg { get; set; }
        public string dsc_funcao { get; set; }
        public DateTime? data_senha_expiracao { get; set; }
        public string nom_skype { get; set; }
        public string setor { get; set; }
        public int? cod_cidade { get; set; }

    }
}
