using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("MapaFaturamentoComplemento", Schema = "dbo")]
    public  class MapaFaturamentoComplemento
    {
        public int MapaFaturamentoComplementoId { get; set; }
        [ForeignKey("_Contrato"), Column("ContratoId")]
        public int? ContratoId { get; set; }
        [ForeignKey("_ContratoVinculado"), Column("ContratoNrcId")]
        public int? ContratoNrcId { get; set; }
        public int PlanoEmpresaId { get; set; }
        public double R_Cofins { get; set; }
        public double R_CSLL { get; set; }
        public double R_INSS { get; set; }
        public double R_IRPJ { get; set; }
        public double R_PIS { get; set; }
        public double R_Outros { get; set; }
         public double R_Boleto { get; set; }

        public virtual Contrato _Contrato { get; set; }
        public virtual ContratoNrc _ContratoVinculado { get; set; }
        public virtual PlanoEmpresa _PlanoEmpresa { get; set; }
    }
}
