using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntidadesMundidata
{
    public class mvx_empresas_contatos
    {
        public int cod_empresa_contato { get; set; }
        public int cod_empresa { get; set; }
        public string nom_contato { get; set; }
        public int cod_empresa_tipo_contato { get; set; }
        public int num_ddd_telefone { get; set; }
        public int num_telefone { get; set; }
        public int num_ddd_fax { get; set; }
        public int num_fax { get; set; }
        public string dsc_mail { get; set; }
        public string dsc_comentario { get; set; }
        public string dsc_cargo { get; set; }


    }
}
