using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Entidades
{
    [Table("BaseCDR", Schema = "dbo")]
    public class BaseCDR
    {
      public int base_id { get; set; }
      public string base_cmp_01 { get; set; }
      public string base_cmp_02 { get; set; }
      public string base_cmp_03 { get; set; }
      public string base_cmp_04 { get; set; }
      public string base_cmp_05 { get; set; }
      public string base_cmp_06 { get; set; }
      public string base_cmp_07 { get; set; }
      public string base_cmp_08 { get; set; }
      public string base_cmp_09 { get; set; }
      public string base_cmp_10 { get; set; }
      public string base_cmp_11 { get; set; }
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
      public int base_file { get; set; }
      public int base_central { get; set; }
      public DateTime data_carga { get; set; }
      public string us_cr { get; set; }
      public int base_file_reset { get; set; }
      public string ano { get; set; }
      public string file_name { get; set; }
    }
}
