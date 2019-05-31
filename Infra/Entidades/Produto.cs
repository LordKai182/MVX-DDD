using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Entidades
{
    [Table("Produto", Schema = "dbo")]
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public string Status { get; set; }
        public string Codigo { get; set; }
        public DateTime DataCriacao { get; set; }
        public int UsuarioId { get; set; }
        public string Propriedades { get; set; }
        public string Faturamento { get; set; }
        public string Tipo { get; set; }
        public string Classe { get; set; }
        public bool Associavel { get; set; }


        #region RELACIONAMENTO
        public virtual Usuario _Usuario { get; set; }
        public virtual IEnumerable<ProdutoMapeamento> _MapeamentoFiscal { get; set; }
        #endregion
    }
}
