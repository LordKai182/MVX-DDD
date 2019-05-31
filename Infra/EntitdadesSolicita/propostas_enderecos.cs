using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntitdadesSolicita
{

    [Table("propostas_enderecos", Schema = "public")]
    public class propostas_enderecos
    {
       
        public long idpropend { get; set; }
        [ForeignKey("_propostas_produtos_resumo"), Column("idpropprod")]
        public Int64? idpropprod { get; set; }
        public virtual propostas_produtos_resumo _propostas_produtos_resumo { get; set; }

        public string classif { get; set; }

        public string predio { get; set; }

        public string pop { get; set; }

        public int filial { get; set; }

        public int cod_predio { get; set; }

        public string cep { get; set; }

        public string endereco { get; set; }

        public int numero { get; set; }

        public string complemento { get; set; }

        public string bairro { get; set; }

        public string cidade { get; set; }

        public string uf { get; set; }

        public string geolocal { get; set; }
    }
}
