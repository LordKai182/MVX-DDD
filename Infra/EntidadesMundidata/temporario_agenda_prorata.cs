using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntidadesMundidata
{
    public class temporario_agenda_prorata
    {
        public int cod_empresa { get; set; }
        public int cod_servico_par { get; set; }
        public decimal mensalidade { get; set; }
        public decimal instalacao { get; set; }
        public DateTime data_status { get; set; }
    }
}
