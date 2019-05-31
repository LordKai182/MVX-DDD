using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntidadesMundidata
{
    public class comercial_tipo_servicos
    {
        public int? cod_tipo_servico { get; set; }
        public string dsc_tipo_servico { get; set; }
        public string dsc_abrev_servico { get; set; }
        public decimal? vlr_mensalidade { get; set; }
        public decimal? vlr_instalacao { get; set; }
        public int? cod_tipo_classe { get; set; }
        public int? ind_ativo { get; set; }
        public int? cod_tipo_ser_generico { get; set; }
        public int? cod_tipo_velocidade { get; set; }
        public int? cat_servico { get; set; }
        public int? n_faturar { get; set; }
        public string tipo { get; set; }
        public int? cod_tipo_mvx { get; set; }
        public Single? banda { get; set; }
    }
}
