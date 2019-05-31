using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("MapeamentoFiscal", Schema = "dbo")]
    public class MapeamentoFiscal
    {
        public int MapeamentoFiscalId { get; set; }
        public int Numero { get; set; }
        public string Serie { get; set; }
        public int NumeroIcms { get; set; }
        public int PercentualIcms { get; set; }
        public string SerieIcms { get; set; }
        public string NumeroDebito { get; set; }
        public int NaturezaOperacao { get; set; }
        public int RegimeEspecialTributacao { get; set; }
        public int OptanteSimplesNacional { get; set; }
        public int IncentivadorCultural { get; set; }
        public int EmpresaId { get; set; }
        public int LoteIcms { get; set; }
        public int LoteDebito { get; set; }
        public int Lote { get; set; }
        //Código de natureza da operação
        //1 – Tributação no município
        //2 - Tributação fora do município
        //3 - Isenção
        //4 - Imune
        //5 –Exigibilidade suspensa por decisão judicial
        //6 – Exigibilidade suspensa por procedimento administrativo

        //Código de identificação do regime especial de tributação
        //1 – Microempresa municipal
        //2 - Estimativa
        //3 – Sociedade de profissionais
        //4 – Cooperativa
        //5 – MEI – Simples Nacional
        //6 – ME EPP – Simples Nacional

        public virtual Empresa _Empresa { get; set; }
    }
}
