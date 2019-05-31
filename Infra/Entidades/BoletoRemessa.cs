using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("BoletoRemessa", Schema = "dbo")]

    public class BoletoRemessa
    {
        public BoletoRemessa()
        {

        }

        public int BoletoRemessaId { get; set; }
        public string Cedente { get; set; }
        public string Lote { get; set; }
        public string Cnpj { get; set; }
        public int Boletos { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
        public int Cancelados { get; set; }
        public int Pagos { get; set; }
        public decimal ValorRemessa { get; set; }
        public byte[] Arquivo { get; set; }

        public virtual ICollection<Boleto> _Boleto { get; set; }

    }
}
