using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Entidades
{
    [Table("CDRDataProbe", Schema = "dbo")]
    public  class CDRDataProbe
    {
                    public Int64 cdr_id { get; set; }
                    public string MyProperty { get; set; }
                    public string id_clie    { get; set; }
                    public string id_mund    { get; set; }
                    public string nr_assa    { get; set; }
                    public string nr_assb    { get; set; }
                    public string nr_part    { get; set; }
                    public string hr_inic    { get; set; }
                    public string hr_dura    { get; set; }
                    public string dt_inic    { get; set; }
                    public string nr_tele    { get; set; }
                    public decimal? vl_ligacao { get; set; }
                    public int? file       { get; set; }
                    public int? central    { get; set; }
                    public DateTime? data       { get; set; }
                    public TimeSpan? hora       { get; set; }
                    public Int64? id_fatd    { get; set; }
                    public int? id_fatm    { get; set; }
                    public Int64? base_id    { get; set; }
                    public string file_name { get; set; }
    }
}
