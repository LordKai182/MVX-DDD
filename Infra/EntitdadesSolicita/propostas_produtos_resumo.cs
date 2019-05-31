using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntitdadesSolicita
{
    [Table("propostas_produtos_resumo", Schema = "public")]
    public class propostas_produtos_resumo
    {
      
        public long idresumo { get; set; }

        
        public Int64? idpropprod { get; set; }
        

     
        public DateTime data_ass_contrato { get; set; }

     
        public DateTime data_mvx { get; set; }

       
        public string nome_contratante { get; set; }

      
        public string tel_contratante { get; set; }

     
        public string email_contratante { get; set; }

     
        public int? so_pe { get; set; }
        
        public int? so_portas { get; set; }

     
        public int? so_rp { get; set; }

     
        public int? tecnologia { get; set; }

    
        public int serasa { get; set; }

    
        public int? dados_ips { get; set; }

     
        public decimal? dados_ips_mensal { get; set; }

     
        public decimal? dados_ips_instal { get; set; }

      
        public decimal? dados_ips_mensal_si { get; set; }

   
        public decimal? dados_ips_instal_si { get; set; }

        
        public string dados_outros_tipo { get; set; }

       
        public decimal? dados_outros_mensal { get; set; }

    
        public decimal? dados_outros_instal { get; set; }

    
        public decimal? dados_outros_mensal_si { get; set; }

       
        public decimal? dados_outros_instal_si { get; set; }

       
        public int? voz_linhas_adic_qtd { get; set; }

        
        public decimal? voz_linhas_adic_mensal { get; set; }

        public decimal? voz_linhas_adic_instal { get; set; }

        public int? voz_portab { get; set; }

        public string voz_portab_nchave { get; set; }

        public string voz_portab_operadora { get; set; }

        public string voz_portab_nportados { get; set; }

        public string vozcb_tipo_serv { get; set; }

        public decimal? vozcb_mensal { get; set; }

        public decimal? vozcb_instal { get; set; }

        public int? vozcb_portab { get; set; }

        public string vozcb_nportado { get; set; }

        public string vozcb_operadora { get; set; }

        public int? host_qtd_mail { get; set; }

        public int? host_qtd_dominios { get; set; }

        public float? host_disco { get; set; }

        public float? host_trafego { get; set; }

        public string utm_equip { get; set; }

        public int? utm_licenca { get; set; }

        public string sec_equip { get; set; }

        public string sec_serv_op_tipo { get; set; }

        public decimal? sec_serv_op_mensal { get; set; }

        public decimal? sec_serv_op_instal { get; set; }

        public decimal? tx_predio_mensal { get; set; }

        public decimal? tx_predio_instal { get; set; }

        public int pre_pago { get; set; }

        public int isencao { get; set; }

        public int? link_inter { get; set; }

        public int? link_promo { get; set; }

        public string resp_tecnico { get; set; }

        public string tel_resp_tecnico { get; set; }

        public string email_resp_tecnico { get; set; }

        public float? multa_contratual { get; set; }

        public string obs { get; set; }

        public string utm_serv_op_tipo { get; set; }

        public decimal? utm_serv_op_mensal { get; set; }

        public decimal? utm_serv_op_instal { get; set; }

        public DateTime? dt_sol_mud { get; set; }

        public int? prazo_inst { get; set; }

        public decimal? voz_linhas_adic_mensal_si { get; set; }

        public decimal? voz_linhas_adic_instal_si { get; set; }

        public decimal? vozcb_mensal_si { get; set; }

        public decimal? vozcb_instal_si { get; set; }

        public decimal? sec_serv_op_mensal_si { get; set; }

        public decimal? sec_serv_op_instal_si { get; set; }

        public decimal? tx_predio_mensal_si { get; set; }

        public decimal? tx_predio_instal_si { get; set; }

        public decimal? utm_serv_op_mensal_si { get; set; }

        public decimal? utm_serv_op_instal_si { get; set; }

        public int? confaz { get; set; }

        public string idwholesale { get; set; }

        public decimal? remanejado { get; set; }

        public int? renova_auto { get; set; }

        public int? air_tipo { get; set; }

        public int? air_qtd { get; set; }

        public int? air_periodo { get; set; }

        public string resp_cobranca { get; set; }

        public string tel_resp_cobranca { get; set; }

        public string email_resp_cobranca { get; set; }

        public int? projeto_padrao { get; set; }

        public int? aviso_previo { get; set; }
    }
}
