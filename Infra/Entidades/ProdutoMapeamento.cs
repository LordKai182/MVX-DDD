using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("ProdutoMapeamento", Schema = "dbo")]
    public class ProdutoMapeamento
    {
        public int ProdutoMapeamentoId { get; set; }
        public string ItemListaServico { get; set; }
        public string CodigoTributacaoMunicipio { get; set; }
        public string Discriminacao { get; set; }
        public string Aliquota { get; set; }
        public int ProdutoId { get; set; }
        public int CidadeId { get; set; }

        #region REFERENCIAS
        public virtual Produto _Produto { get; set; }
        public virtual Cidade _Cidade { get; set; }

        #endregion
    }
}
