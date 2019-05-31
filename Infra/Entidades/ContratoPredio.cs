using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("ContratoPredio", Schema = "dbo")]
    public class ContratoPredio
    {
        public ContratoPredio()
        {
            this.DataCadastro = DateTime.Now;
        }
        public int ContratoPredioId { get; set; }
        public int ClienteId { get; set; }
        public int PredioId { get; set; }
        public int TipoLogradouroId { get; set; }
        public string Complemento { get; set; }
        public string CodigoContrato { get; set; }
        public DateTime DataCadastro { get; set; }

        public virtual Predio _Predio { get; set; }
        public virtual Cliente _Cliente { get; set; }
        public virtual TipoLogradouro _TipoLogradouro { get; set; }
       
    }
}
