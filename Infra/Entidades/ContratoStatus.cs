using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("ContratoStatus", Schema = "dbo")]
    public class ContratoStatus
    {

        public int ContratoStatusId { get; set; }
        public string ContratoStatusNome { get; set; }
    }
}
