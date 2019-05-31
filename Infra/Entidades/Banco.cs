using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Entidades
{
    [Table("Banco", Schema = "dbo")]
    public class Banco
    {
        public Banco()
        {
            this._BancoVencimento = new List<BancoVencimento>();
        }
        public int BancoId { get; set; }
        public string BancoNome { get; set; }
        public string BancoCodigo { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public string Carteira { get; set; }
        public string CedenteCodigo { get; set; }
        public string Modalidade { get; set; }
        public string Convenio { get; set; }
        public string NossoNumero { get; set; }
        public int EmpresaId { get; set; }

        public virtual ICollection<BancoVencimento> _BancoVencimento { get; set; }
        public virtual Empresa _Empresa { get; set; }

    }
}
