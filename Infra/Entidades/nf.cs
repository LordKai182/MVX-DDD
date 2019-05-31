using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("nf", Schema = "dbo")]
    public class nf
    {
        public int id { get; set; }
        public Int64 nf_id { get; set; }
        public int CenarioId { get; set; }
        public string id_clie { get; set; }
        public int cod_empresa { get; set; }
        public string nr_mcdu { get; set; }
        public DateTime data_ger { get; set; }
        public DateTime data_ini { get; set; }
        public DateTime data_fim { get; set; }
        public DateTime data_fem { get; set; }
        public DateTime data_venc { get; set; }
        public decimal valor_total { get; set; }
        public int status { get; set; }
        public DateTime data_pag { get; set; }
        public string us_pag { get; set; }
        public decimal valor_pago { get; set; }
        public char tipo { get; set; }
        public string obs_canc { get; set; }
        public string servico { get; set; }
        public Int64 nr_rps { get; set; }
        public int nr_lote { get; set; }
        public DateTime data_emissao_pref { get; set; }
        public Int64 num_nota_prefeitura { get; set; }
        public string cod_verificacao { get; set; }
        public Int64 num_nota_danfe { get; set; }
        public int re { get; set; }
        public Int64 num_multa { get; set; }
        public int plan_multa { get; set; }
        public string obs_multa1 { get; set; }
        public string obs_multa2 { get; set; }
        public string obs_multa3 { get; set; }
        public string obs_multa4 { get; set; }

        public virtual Cenario _CenarioDados { get; set; }
    }
}
