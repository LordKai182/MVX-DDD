using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntidadesMundidata
{
   public class mvx_predios
    {
        public int? cod_predio { get; set; }
        public string nom_predio { get; set; }
        public int? num_quarteirao { get; set; }
        public DateTime? dat_pedido { get; set; }
        public int? cod_prospect { get; set; }
        public int? cod_vendedor { get; set; }
        public int? cod_link { get; set; }
        public int? cod_stu_prospect { get; set; }
        public string dsc_observacao { get; set; }
        public int? cod_resp_contato { get; set; }
        public int? cod_anel { get; set; }
        public int? cod_func_neg_p { get; set; }
        public int? cod_supervisor_agenda { get; set; }
        public int? cod_supervisor_vendedor { get; set; }
        public string dsc_cnpj { get; set; }
        public int? num_andares { get; set; }
        public int? num_sala { get; set; }
        public string  dsc_area { get; set; }    
    }
}
