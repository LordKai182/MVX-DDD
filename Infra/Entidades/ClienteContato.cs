using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Entidades
{
    [Table("ClienteContato", Schema = "dbo")]
    public class ClienteContato
    {
        public int ClienteContatoId { get; set; }
        public string Nome { get; set; }
        public string TelefoneFixo { get; set; }
        public string TelefoneCelular { get; set; }
        public string Email { get; set; }
        public bool AvisoCobranca { get; set; }
        public string Endereco { get; set; }
        public string Observacao { get; set; }
        public string TipoFuncionario { get; set; }
        public string FuncionarioCargo { get; set; }
        public int ContratoId { get; set; }
        public int ClienteId { get; set; }
        public bool Ativo { get; set; }
        public virtual Contrato _Contrato { get; set; }
        public virtual Cliente _Cliente { get; set; }
    }
}
