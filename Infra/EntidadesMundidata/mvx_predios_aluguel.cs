using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntidadesMundidata
{
    public class mvx_predios_aluguel
    {
        public int cod_predio_aluguel { get; set; }
        public int forma_cobranca { get; set; }
        public int cod_responsavel { get; set; }
        public DateTime data_atual { get; set; }
        public int cod_predio { get; set; }
        public string obs { get; set; }
        public int contato_aluguel { get; set; }
        public int cod_empresa_parceria { get; set; }
        public DateTime dat_desativacao { get; set; }
        public DateTime dat_ativacao { get; set; }
        public int dia_vencimento { get; set; }
        public int cod_empresa_parceria2 { get; set; }
        public string dsc_conta { get; set; }
        public int ind_gabinete { get; set; }
        public int ind_furacao { get; set; }
        public int ind_grupo_mvx { get; set; }

    }
}
