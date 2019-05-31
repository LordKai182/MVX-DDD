using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("ProdutoDetalhe", Schema = "dbo")]
    public class ProdutoDetalhe
    {
        public int ProdutoDetalheId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
        public int ProdutoId { get; set; }
        public int Faturamento { get; set; }
        public string Tipo { get; set; }
        public string PropriedadeNaoFaturada { get; set; }
        public string PropriedadeFaturada { get; set; }
    }
}
