using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("Cedente", Schema = "dbo")]
    public class Cedente 
    {
        public Cedente()
        {
            //this._Boleto = new HashSet<boleto>();
        }

        public int CedenteId { get; set; }
        public int EmpresaId { get; set; }
        public string NomeBanco { get; set; }
        public int NumeroBanco { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public string Carteira { get; set; }
        public string Cod { get; set; }
        public string NossoNumero { get; set; }
        public int NumeroRemessa { get; set; }




        public virtual Empresa _Empresa { get; set; }

        //public virtual ICollection<BancoVencimento> _BancoVencimento { get; set; }

    }
}
