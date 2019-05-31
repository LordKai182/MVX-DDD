using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("EmpresaEndereco", Schema = "dbo")]
    public class EmpresaEndereco
    {

        public int EmpresaEnderecoId { get; set; }
        public string Logradouro { get; set; }
        public string Cep { get; set; }
        public int Numero { get; set; }
        public string Municipio { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Complemento { get; set; }
        public int TipoEndereco { get; set; }
        public int? TipoLogradouroId { get; set; }
        public int BairroId { get; set; }
        public int UsuarioId { get; set; }
        public int EmpresaId { get; set; }


        public virtual Empresa _Empresa { get; set; }
        public virtual Usuario _Usuario { get; set; }
        public virtual Bairro _Bairro { get; set; }
        public virtual TipoLogradouro _TipoLogradouro { get; set; }
    }
}
