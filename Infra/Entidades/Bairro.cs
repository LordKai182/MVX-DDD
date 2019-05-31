
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Entidades
{
    [Table("Bairro", Schema = "dbo")]
    public class Bairro
    {
        public int BairroId { get; set; }
        public string Nome { get; set; }
        public int? CidadeId { get; set; }

        public virtual Cidade _Cidade { get; set; }
    }
}
