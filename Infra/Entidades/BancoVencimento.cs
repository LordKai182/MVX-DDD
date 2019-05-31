using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("Banco", Schema = "dbo")]
    public class BancoVencimento
    {
        public int BancoVencimentoId { get; set; }
        public int BancoId { get; set; }
     
        public int Vecimento { get; set; }
        public string TipoCenario { get; set; }
        public bool Multa { get; set; }
        public double Juros { get; set; }


        #region RELACIONAMENTO

        public virtual Banco _Banco { get; set; }
     
        #endregion

    }
}
