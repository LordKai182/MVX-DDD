using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntidadesNaoPersistidas
{
    public class CenarioDetalhes
    {

        public class SumarioDados
        {
            public string Codigo { get; set; }
            public decimal? Totalfaturado { get; set; }
            public int QtdFaturado { get; set; }
            public decimal? TotalSemfatura { get; set; }
            public int QtdSemFatura { get; set; }
            public decimal? SaldoEmAberto { get; set; }
            public int QtdEmAberto { get; set; }
            public int ProdutosNovos { get; set; }
            public int ProdutosModificados { get; set; }

        }

        public class Detalhes
        {

            public string TipoDoFaturamento { get; set; }
            public int ContratoId { get; set; }
            public int? StatusId { get; set; }
            public string Codigo { get; set; }
            public string Produto { get; set; }
            public string NomeRazao { get; set; }
            public string TipoCliente { get; set; }
            public string CnpjCpf { get; set; }
            public decimal ValorMensalidade { get; set; }
            public double? Desconto { get; set; }
            public string Plano { get; set; }
            public string Cidade { get; set; }
            public DateTime? DataInstalacao { get; set; }
            public DateTime? DataValidade { get; set; }
            public string Validade { get; set; }
            public string RenovacaoAutomatica { get; set; }
            public string PeriodoRenovacao { get; set; }
            public decimal ValorNf { get; set; }
            public decimal ValorBoleto { get; set; }
            public int Vencimento { get; set; }
            public string BancoEmissor { get; set; }
            public string HistoricoBoleto { get; set; }
            public int DiasAtraso { get; set; }
            public decimal SaldoAberto { get; set; }
            public string GerarNovoBoleto { get; set; }
            public string UF { get; set; }
            public int? TipoClienteId { get; set; }
            public int StatusCenario { get; set; }
            public int StatusContrato { get; set; }
            public int PlanoId { get; set; }
            public DateTime? DataStatus { get; set; }
            public bool PossuiRc { get; set; }
            public decimal? ValorTotalRc { get; set; }
            public bool Historico { get; set; }
            public bool Prorrata { get; set; }
            public string CodigoMundiData { get; set; }
            public string CodPar { get; set; }
            public decimal ValorCalculado { get; set; }
            public string Mensagem { get; set; }
            public int VencimentoCorrente { get; set; }

            public string Grupo { get; set; }
            public int GrupoId { get; set; }
            public int GrupoQtd { get; set; }
            public string TipoPagamento { get; set; }
            public string Competencia { get; set; }
            public string VencidoVencimento { get; set; }
        }

    }
}
