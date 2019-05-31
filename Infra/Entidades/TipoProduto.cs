using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("TipoProduto", Schema = "dbo")]
    public class TipoProduto
    {
        public int TipoProdutoId { get; set; }
        public int ProdutoId { get; set; }
        public string Faturamento { get; set; }
        public string Propriedade { get; set; }




    }
}
