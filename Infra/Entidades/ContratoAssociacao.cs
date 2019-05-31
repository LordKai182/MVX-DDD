using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("ContratoAssociacao", Schema = "dbo")]
    public class ContratoAssociacao
    {

        public ContratoAssociacao()
        {
            this.DataCriacao = DateTime.Now;
        }


        public int ContratoAssociacaoId { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public int ContratoId { get; set; }
        public int ProdutoId { get; set; }
        public decimal Valor { get; set; }
        public string Propriedade { get; set; }


        #region REFERENCIA

        public virtual Produto _Produto { get; set; }

        public virtual Contrato _Contrato { get; set; }

        #endregion
    }
}
