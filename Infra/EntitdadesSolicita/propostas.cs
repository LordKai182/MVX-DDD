using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntitdadesSolicita
{
    [Table("propostas", Schema = "public")]
    public class propostas
    {
      
        public long codprop { get; set; }

        public string consultor { get; set; }

        public string cpf_cnpj { get; set; }

        public DateTime data_abertura { get; set; }

        public DateTime prazo_proposta { get; set; }

        public string tipo_insc { get; set; }

        public int setor { get; set; }
       
    }
}
