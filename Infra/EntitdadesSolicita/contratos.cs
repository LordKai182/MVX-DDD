using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntitdadesSolicita
{
    [Table("contratos", Schema = "public")]
    public class contratos
    {
        public Int64 id { get; set; }
        public Int64? codigo { get; set; }
      
       
        public Int64? idpropprod { get; set; }
       
        public virtual propostas_produtos _propostas_produtos { get; set; }

        public string n_contrato { get; set; }
        public DateTime? dt_gestao { get; set; }
        public DateTime? data_hora_entrada { get; set; }
        public DateTime? dt_venda { get; set; }
        public int? tipo_cliente { get; set; }
        [ForeignKey("_c_vencimentos"), Column("vencimento")]
        public int? vencimento { get; set; }
        public virtual c_vencimentos _c_vencimentos { get; set; }
        public int? tipo_venda { get; set; }
        public Single? porcentagem { get; set; }
        public string imunicipal { get; set; }
        public string iestadual { get; set; }
        public Int16? status { get; set; }
        public DateTime? dt_aceite { get; set; }
        public DateTime? dt_cancel { get; set; }
        public DateTime? dt_instal { get; set; }
        public DateTime? dt_cc { get; set; }
        public DateTime? dt_oper { get; set; }
        public int? recebido_gestao { get; set; }
        public string recebido_por { get; set; }
        public DateTime? dthr_recebido_gestao { get; set; }
        public int? filaenc { get; set; }
        public int? pendente { get; set; }
        public int? up_down_remoto { get; set; }
        public string num_chave { get; set; }
        public DateTime? dt_aceite_gestao { get; set; }
        public int? aceite_voz { get; set; }
        public int? nao_envolve_oper { get; set; }
        public DateTime? dt_relat_vendas { get; set; }
        public int? ok_documentacao { get; set; }
        public int? ok_boas_vindas { get; set; }
        public int? ok_noc { get; set; }
        public int? ok_ngn { get; set; }
        public int? ok_eng_sdh { get; set; }
        public int? ok_agendamento { get; set; }
        public string renova_obs { get; set; }
        public Int64? renova_contrato { get; set; }
        public int? renovado { get; set; }
        public int? auditoria { get; set; }
        public string auditoria_obs { get; set; }
        public string auditoria_por { get; set; }
        public DateTime? auditoria_data_hora { get; set; }
        public int? ok_eng_rede_optica { get; set; }


    }
}
