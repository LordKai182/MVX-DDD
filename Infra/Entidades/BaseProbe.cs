using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Entidades
{
    [Table("BaseProbe", Schema = "dbo")]
    public class BaseProbe
    {
      
                 public int base_id { get; set; }
                 public string base_cmp_12 { get; set; }
                 public string base_cmp_13 { get; set; }
                 public string base_cmp_14 { get; set; }
                 public string base_cmp_15 { get; set; }
                 public string base_cmp_16 { get; set; }
                 public string base_cmp_17 { get; set; }
                 public string base_cmp_18 { get; set; }
                 public string base_cmp_19 { get; set; }
                 public string base_cmp_20 { get; set; }
                 public string base_cmp_21 { get; set; }
                 public string base_cmp_22 { get; set; }
                 public string base_cmp_23 { get; set; }
                 public string base_cmp_24 { get; set; }
                 public string base_cmp_25 { get; set; }
                 public string base_cmp_26 { get; set; }
                 public string base_cmp_27 { get; set; }
                 public string base_cmp_28 { get; set; }
                 public string base_cmp_29 { get; set; }
                 public string base_cmp_30 { get; set; }
                 public string base_cmp_31 { get; set; }
                 public string base_cmp_32 { get; set; }
                 public string base_cmp_33 { get; set; }
                 public string base_cmp_34 { get; set; }
                 public DateTime data_carga { get; set; }
                 public string file_name { get; set; }
                 public int nao_faturavel { get; set; }
    }
}
