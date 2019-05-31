using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("VendedorRelacionamento", Schema = "dbo")]
    public class VendedorRelacionamento
    {
        public int VendedorRelacionamentoId { get; set; }
        public int VendedorId { get; set; }
        public string Nome { get; set; }
        public int? VendedorMundisoId { get; set; }
        public string Status { get; set; }
        public string VendedorMundidataRjId { get; set; }
        public string VendedorMundidataSpId { get; set; }
        public string VendedorMundidataBhId { get; set; }
    }
}
