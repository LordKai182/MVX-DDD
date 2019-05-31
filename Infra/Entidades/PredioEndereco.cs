using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("PredioEndereco", Schema = "dbo")]
    public class PredioEndereco
    {
        public int PredioEnderecoId { get; set; }
        public string Logradouro { get; set; }
        public string Cep { get; set; }
        public int? Numero { get; set; }
        public string Municipio { get; set; }
        public string Complemento { get; set; }
        public int? TipoLogradouroId { get; set; }
        public int? BairroId { get; set; }
        public int? PredioId { get; set; }
        public string GeoLocal { get; set; }
        public virtual Predio _Predio { get; set; }
        public virtual Bairro _Bairro { get; set; }
        public virtual TipoLogradouro _TipoLogradouro { get; set; }
    }
}
