using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntidadesMundidata
{
     public class comercial_hist_st_empresas_serv
     {
        public int cod_hist_empresa_serv { get; set; }
        public int cod_empresa_servico { get; set; }
        /// <summary>
        ///  THIAGO BOLA
        /// </summary>
        public int cod_status_serv { get; set; }
        public DateTime? data_status { get; set; }
        public DateTime? data_atual { get; set; }
        public int? desc_faturamento { get; set; }
        public DateTime? data_contratacao { get; set; }

    }
}
