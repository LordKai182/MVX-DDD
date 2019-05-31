using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("Faturamento_nrc", Schema = "dbo")]

    public class Faturamento_nrc
    {
        public int Faturamento_nrc_id { get; set; }
        public int Faturamento_nrc_tipo { get; set; }
        public decimal Faturamento_nrc_valor { get; set; }
        public string Faturamento_nrc_nome { get; set; }
        public DateTime Faturamento_nrc_data_alt { get; set; }
        public DateTime Faturamento_dt_ini { get; set; }
        public DateTime Faturamento_dt_fim { get; set; }
        public int Faturamento_status { get; set; }
        public string Faturamento_us_cr { get; set; }
    }
}
