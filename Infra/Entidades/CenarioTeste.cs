using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("CenarioTeste", Schema = "dbo")]
    public class CenarioTeste
    {
        public int CenarioTesteID { get; set; }
        public byte[] Cenario { get; set; }
        public string TextoDoCenario { get; set; }

    }
}
