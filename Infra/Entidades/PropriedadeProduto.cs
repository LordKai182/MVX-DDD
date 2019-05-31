using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Entidades
{
    [Table("PropriedadeProduto", Schema = "dbo")]
    public class PropriedadeProduto 
    {
        public int PropriedadeProdutoId { get; set; }
        public string PropriedadeNome { get; set; }
        public string TipoValor { get; set; }
        public string AlteradoPor { get; set; }
        public bool ExigeSO { get; set; }
        public bool GeraUpDown { get; set; }
        public bool Obrigatorio { get; set; }
        public string Mascara { get; set; }
        public string ValorDigitado { get; set; }
        public string NumeroSO { get; set; }
    }
}
