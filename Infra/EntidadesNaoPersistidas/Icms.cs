using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiposEnum;

namespace Infra.EntidadesNaoPersistidas
{
    public class Icms
    {

        public class Mestre
        {
            public string CnpjCpf { get; set; }
            public string IE { get; set; }
            public string RazaoSocial { get; set; }
            public string Uf { get; set; }
            public string ClasseDeConsumo { get; set; }
            public string FaseOuTipoDeUltilizacao { get; set; }
            public string GrupoDeTensao { get; set; }
            public string CodigoDeIdentificacaoDoConsumidorOuAssinante { get; set; }
            public string DataDeEmissao { get; set; }
            public string Modelo { get; set; }
            public string Serie { get; set; }
            public string Numero { get; set; }
            public string CodigoDeAutenticacaoDigitalDoDocumentoFiscal { get; set; }
            public string ValorTotal { get; set; }
            public string BcIcms { get; set; }
            public string IcmsDestacado { get; set; }
            public string OperacoesIsentasOuNaoTributadas { get; set; }
            public string OutrosValores { get; set; }
            public string SituacaoDoDocumento { get; set; }
            public string AnoMesDeReferenciaDeApuracao { get; set; }
            public string ReferenciaAoItemDaNf { get; set; }
            public string NumeroDoTerminalTelefonicoOuDaUnidadeConsumidora { get; set; }
            public string IdentificacaoDoTipoDeInformacaoContidaCampo1 { get; set; }
            public string TipoDeCliente { get; set; }
            public string SubClasseDeConsumo { get; set; }
            public string NumeroDoTerminalTelefonicoPrincipal { get; set; }
            public string CnpjDoEmitente { get; set; }
            public string NumeroDoCodigoOuFaturaComercial { get; set; }
            public string ValorTotalDaFaturaComercial { get; set; }
            public string DataDeLeituraAnterior { get; set; }
            public string DataDeLeituraAtual { get; set; }
            public string Brancos1 { get; set; }
            public string Brancos2 { get; set; }
            public string InformacoesAdicionais { get; set; }
            public string Brancos3 { get; set; }
            public string CodigoDeAutenticacaoDIgitalDoRegistro { get; set; }



        }

        public class Item
        {
            public string CnpjCpf { get; set; }
            public string UF { get; set; }
            public TipoICMSClasseConsumo ClasseDeConsumo { get; set; }
            public TipoICMSUltilizacao FaseOuTipoDeUltilizacao { get; set; }
            public TipoICMSGrupoDeTensao GrupoDeTensao { get; set; }
            public string DataDeEmissao { get; set; }
            public TipoICMSModelo Modelo { get; set; }
            public string Serie { get; set; }
            public string Numero { get; set; }
            public string CFOP { get; set; }
            public string NDeOrdemDoItem { get; set; }
            public string CodigoDoItem { get; set; }
            public string DescricaoDoItem { get; set; }
            public string CodigoDeClassificacaoDoItem { get; set; }
            public string Unidade { get; set; }
            public string QuantidadeContratada { get; set; }
            public string QuantidadeMedida { get; set; }
            public string Total { get; set; }
            public string DescontosRedutores { get; set; }
            public string AcrescimosDespesasAcessorias { get; set; }
            public string BcIcms { get; set; }
            public string Icms { get; set; }
            public string OperacoesIsentasOuNaoTributadas { get; set; }
            public string Outrosvalores { get; set; }
            public string AliquotaIcms { get; set; }
            public string Situacao { get; set; }
            public string AnoMesDeReferenciaDeApuracao { get; set; }
            public string NumeroDoContrato { get; set; }
            public string QuantidadeFaturada { get; set; }
            public string TarifaAplicada { get; set; }
            public string AliquotaPis { get; set; }
            public string Pis { get; set; }
            public string AliquotaCofins { get; set; }
            public string Cofins { get; set; }
            public string IndicadorDeDescontoJudicial { get; set; }
            public string TipoDeIsencao { get; set; }
            public string Branco { get; set; }
            public string CodigoDeAutenticacaoDigitalDoRegistro { get; set; }


        }

        public class Dados
        {
            public string CnpjCpf { get; set; }
            public string Ie { get; set; }
            public string RazaoSocial { get; set; }
            public string Logradouro { get; set; }
            public string NumeroEndereco { get; set; }
            public string Complemento { get; set; }
            public string Cep { get; set; }
            public string Bairro { get; set; }
            public string Municipio { get; set; }
            public string Uf { get; set; }
            public string TelefoneDeContato { get; set; }
            public string CodigoDeIdentificacaoDoConsumidor { get; set; }
            public string NumeroDoterminaltelefonicoOuUnidadeConsumidora { get; set; }
            public string UfDeHabilitacaoDoTerminal { get; set; }
            public string DataDeEmissao { get; set; }
            public string Modelo { get; set; }
            public string Serie { get; set; }
            public string Numero { get; set; }
            public string CodigoMunicipio { get; set; }
            public string Brancos { get; set; }
            public string CodigoDeAutenticacaoDoRegistro { get; set; }


        }

    }
}
