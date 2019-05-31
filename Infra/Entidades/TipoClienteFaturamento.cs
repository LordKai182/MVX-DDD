using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("TipoClienteFaturamento", Schema = "dbo")]
    public class TipoClienteFaturamento
    {

        public int TipoClienteFaturamentoId { get; set; }
        public string Descricao { get; set; }
        public int? IndicadorComercial { get; set; }


    }
}
