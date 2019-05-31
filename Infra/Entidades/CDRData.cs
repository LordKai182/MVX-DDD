using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("CDRData", Schema = "dbo")]
    public  class CDRData
    {
        public Int64 cdr_id { get; set; }
        public string id_clie { get; set; }
        public string id_mund { get; set; }
        public string nr_assa { get; set; }
        public string nr_assb { get; set; }
        public string nr_part { get; set; }
        public string hr_inic { get; set; }
        public string hr_dura { get; set; }
        public string dt_inic { get; set; }
        public string nr_tele { get; set; }
        public decimal vl_ligacao { get; set; }
        public int file { get; set; }
        public int central { get; set; }
        public DateTime data { get; set; }
        public DateTime hora { get; set; }
        public Int64 id_fatd { get; set; }
        public int id_fatm { get; set; }
        public int file_reset { get; set; }
        public string file_name { get; set; }

    }
}
