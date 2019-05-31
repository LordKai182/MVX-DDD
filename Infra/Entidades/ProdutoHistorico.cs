using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("ProdutoHistorico", Schema = "dbo")]
    public class ProdutoHistorico
    {
        public int ProdutoHistoricoId { get; set; }
        public DateTime DataHoraOcorrencia { get; set; }
        public string Acao { get; set; }
        public string Campo { get; set; }
        public string ValorAntigo { get; set; }
        public string ValorNovo { get; set; }
        public string NumeroSO { get; set; }
        public int UsuarioId { get; set; }
        public int ProdutoId { get; set; }
        public virtual Usuario _Usuario { get; set; }
        public virtual Produto _Produto { get; set; }
    }
}
