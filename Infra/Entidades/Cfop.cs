using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("Cfop", Schema = "dbo")]
    public class Cfop
    {
        public int CfopId { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public string Codigo { get; set; }

    }
}
