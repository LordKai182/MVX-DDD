using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntidadesMundidata
{
    public class mvx_predios_historicos
    {
        public int cod_hist_predio { get; set; }
        public DateTime dat_inicio { get; set; }
        public DateTime dat_fim { get; set; }
        public string dsc_justificativa { get; set; }
        public int cod_funcionario { get; set; }
        public int cod_predio { get; set; }
        public int cod_stu_predio { get; set; }

    }
}
