using Impactro.Cobranca;
using Infra.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{

    public class Nota
    {
        //

        public string Codigo { get; set; }
        public string Contrato { get; set; }
        public string Cedente { get; set; }
        public string Sacado { get; set; }
        public decimal valorDocumento { get; set; }
        public string Propriedades { get; set; }
        public string Imposto { get; set; }

        public int ContratoId { get; set; }

        public bool Vinculado { get; set; }

        public double Retencao { get; set; }

    }
    public class Classes
    {
        public static List<Remessa> ListaRemessa { get; set; }

    }
    public class Agrupado
    {
        public static List<Remessa> ListaRemessaAgrupado { get; set; }
    }

    public class Remessa
    {
        public CedenteInfo Cedente { get; set; }
        public string _NossoNumero { get; set; }
        public Sacado SacadoCleinte { get; set; }

    }
    public class Sacado
    {
        public SacadoInfo _SacadoInfo { get; set; }
        public List<BoletoInfo> ListaBoleto { get; set; }
        public MapaFaturamento _MapaFaturamento { get; set; }
        public PlanoEmpresa _PlanoEmpresa { get; set; }
        public List<MapaFaturamentoComplemento> _Retencao { get; set; }

    }
}
