using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntidadesMundidata
{
   public class comercial_tipo_classes
    {
        public int cod_tipo_cliente { get; set; }
        public string dsc_tipo_cliente { get; set; }
        public int vlr_mensalidade_icms { get; set; }
        public int vlr_mensalidade_iss { get; set; }
        public decimal vlr_mensalidade_pis_cofins { get; set; }
        public decimal vlr_instalacao_icms { get; set; }
        public int vlr_instalacao_iss { get; set; }
        public decimal vlr_instalacao_pis_cofins { get; set; }
        public int cod_classe_contabil { get; set; }
    }
}
