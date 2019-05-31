using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("ClienteEndereco", Schema = "dbo")]
    public class ClienteEndereco
    {
        public int ClienteEnderecoId { get; set; }
        public string Logradouro { get; set; }
        public string Cep { get; set; }
        public int Numero { get; set; }
        public string Municipio { get; set; }
        public string Complemento { get; set; }
        public int? TipoLogradouroId { get; set; }
        public int PredioId { get; set; }
        public int BairroId { get; set; }
        public int ClienteId { get; set; }
        public int? ContratoId { get; set; }
        public string Sala { get; set; }
        public string Andar { get; set; }
        public string Bloco { get; set; }
        public string Ponta { get; set; }
        public bool Migrado { get; set; }
        public virtual Cliente _Cliente { get; set; }
        public virtual Bairro _Bairro { get; set; }
        public virtual TipoLogradouro _TipoLogradouro { get; set; }
        public virtual Predio _Predio { get; set; }
        public virtual Contrato _Contrato { get; set; }
    }
}
