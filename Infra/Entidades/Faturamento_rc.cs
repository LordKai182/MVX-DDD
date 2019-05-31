using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("Faturamento_rc", Schema = "dbo")]
    public  class Faturamento_rc
    {
        public int Faturamento_rc_id { get; set; }
        public int Faturamento_rc_tipo { get; set; }
        public string Faturamento_rc_nome { get; set; }
        public decimal Faturamento_rc_valor { get; set; }
        public DateTime Faturamento_rc_data_alt { get; set; }
        public DateTime Faturamento_rc_dt_ini { get; set; }
        public DateTime Faturamento_rc_dt_fim { get; set; }
        public int Faturamento_rc_status { get; set; }
        public string Faturamento_rc_us_cr { get; set; }
        public decimal Faturamento_rc_valor2 { get; set; }
        public decimal Faturamento_rc_valor3 { get; set; }
    }
}
