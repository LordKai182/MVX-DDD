using Infra;
using Infra.EntidadesNaoPersistidas;
using NFSE.Net.Layouts.BHISS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Utils
{
    public class ICMS
    {


        private Class1 db = new Class1(true);
        public void GeraICMS(NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvio _nfse, ref List<Icms.Mestre> ListaMestre, ref List<Icms.Item> ListaItem, ref List<Icms.Dados> ListaDados)
        {

            #region ASSOCIACOES 
            int coutt = 0;

            foreach (var item in _nfse.LoteRps.ListaRps)
            {
                if (coutt == 106)
                {

                }
                ListaMestre.Add(GeraMestreUnico(item, _nfse.LoteRps.Cnpj));
                ListaItem.Add(GeraItemUnico(item));
                ListaDados.Add(GeraDadosUnico(item));
                coutt++;
            }
            string CaminhoMestre = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, HttpContext.Current.Server.MapPath("~/App_Data/ICMS_Arquivo/" + _nfse.LoteRps.NumeroLote + _nfse.LoteRps.Cnpj + "M.txt"));
            StreamWriter writer = new StreamWriter(CaminhoMestre);
            foreach (var item in ListaMestre)
            {
                writer.WriteLine(
                    item.CnpjCpf +
                    item.IE +
                    item.RazaoSocial +
                    item.Uf +
                    item.ClasseDeConsumo +
                    item.FaseOuTipoDeUltilizacao +
                    item.GrupoDeTensao +
                    item.CodigoDeIdentificacaoDoConsumidorOuAssinante +
                    item.DataDeEmissao +
                    item.Modelo +
                    item.Serie +
                    item.Numero +
                    item.CodigoDeAutenticacaoDigitalDoDocumentoFiscal +
                    item.ValorTotal +
                    item.BcIcms +
                    item.IcmsDestacado +
                    item.OperacoesIsentasOuNaoTributadas +
                    item.OutrosValores +
                    item.SituacaoDoDocumento +
                    item.AnoMesDeReferenciaDeApuracao +
                    item.ReferenciaAoItemDaNf +
                    item.NumeroDoTerminalTelefonicoOuDaUnidadeConsumidora +
                    item.IdentificacaoDoTipoDeInformacaoContidaCampo1 +
                    item.TipoDeCliente +
                    item.SubClasseDeConsumo +
                    item.NumeroDoTerminalTelefonicoPrincipal +
                    item.CnpjDoEmitente +
                    item.NumeroDoCodigoOuFaturaComercial +
                    item.ValorTotalDaFaturaComercial +
                    item.DataDeLeituraAnterior +
                    item.DataDeLeituraAtual +
                    item.Brancos1 +
                    item.Brancos2 +
                    item.InformacoesAdicionais +
                    item.Brancos3 +
                    item.CodigoDeAutenticacaoDIgitalDoRegistro
                    );
            }
            writer.Close();

            string CaminhoItem = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, HttpContext.Current.Server.MapPath("~/App_Data/ICMS_Arquivo/" + _nfse.LoteRps.NumeroLote + _nfse.LoteRps.Cnpj + "I.txt"));
            StreamWriter writerItem = new StreamWriter(CaminhoItem);
            foreach (var item in ListaItem)
            {
                writerItem.WriteLine(
                    item.CnpjCpf +
                    item.UF +
                    item.ClasseDeConsumo +
                    item.FaseOuTipoDeUltilizacao +
                    item.GrupoDeTensao +
                    item.DataDeEmissao +
                    item.Modelo +
                    item.Serie +
                    item.Numero +
                    item.CFOP +
                    item.NDeOrdemDoItem +
                    item.CodigoDoItem +
                    item.DescricaoDoItem +
                    item.CodigoDeClassificacaoDoItem +
                    item.Unidade +
                    item.QuantidadeContratada +
                    item.QuantidadeMedida +
                    item.Total +
                    item.DescontosRedutores +
                    item.AcrescimosDespesasAcessorias +
                    item.BcIcms +
                    item.Icms +
                    item.OperacoesIsentasOuNaoTributadas +
                    item.Outrosvalores +
                    item.AliquotaIcms +
                    item.Situacao +
                    item.AnoMesDeReferenciaDeApuracao +
                    item.NumeroDoContrato +
                    item.QuantidadeFaturada +
                    item.TarifaAplicada +
                    item.AliquotaPis +
                    item.Pis +
                    item.AliquotaCofins +
                    item.Cofins +
                    item.IndicadorDeDescontoJudicial +
                    item.TipoDeIsencao +
                    item.Branco +
                    item.CodigoDeAutenticacaoDigitalDoRegistro
                    );
            }
            writerItem.Close();

            string CaminhoDados = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, HttpContext.Current.Server.MapPath("~/App_Data/ICMS_Arquivo/" + _nfse.LoteRps.NumeroLote + _nfse.LoteRps.Cnpj + "D.txt"));
            StreamWriter writerDados = new StreamWriter(CaminhoDados);
            foreach (var item in ListaDados)
            {
                writerDados.WriteLine(
                    item.CnpjCpf +
                    item.Ie +
                    item.RazaoSocial +
                    item.Logradouro +
                    item.NumeroEndereco +
                    item.Complemento +
                    item.Cep +
                    item.Bairro +
                    item.Municipio +
                    item.Uf +
                    item.TelefoneDeContato +
                    item.CodigoDeIdentificacaoDoConsumidor +
                    item.NumeroDoterminaltelefonicoOuUnidadeConsumidora +
                    item.UfDeHabilitacaoDoTerminal +
                    item.DataDeEmissao +
                    item.Serie +
                    item.Numero +
                    item.CodigoMunicipio +
                    item.Brancos +
                    item.CodigoDeAutenticacaoDoRegistro
                    );
            }
            writerDados.Close();

            #endregion

        }
        public void EscreveTxt()
        {

        }
        public Icms.Mestre GeraMestreUnico(EnviarLoteRpsEnvioLoteRpsRps nota, string emitente)
        {
            string Razao = nota.InfRps.Tomador.RazaoSocial;
            Icms.Mestre _icms = new Icms.Mestre();
            _icms.CnpjCpf = nota.InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cnpj.ToString().PadLeft(14, ' ');
            _icms.IE = db.cliente.First(v => v.RazaoSocial.Contains(Razao)).InscEstadual == null ? "ISENTO" : db.cliente.First(v => v.RazaoSocial.Contains(Razao)).InscEstadual.Replace(".", "").Replace("-", "").Replace("_", "").Replace("/", "").PadRight(14, ' ');                                                                     //cli.InscEstadual == null ? "ISENTO" : cli.InscEstadual.Replace(".", "").Replace("-", "").Replace("_", "").Replace("/", "").PadRight(14, ' ');
            _icms.RazaoSocial = Razao.ToUpper().PadRight(35, ' ');
            _icms.Uf = nota.InfRps.Tomador.Endereco.Uf;
            _icms.ClasseDeConsumo = PegarValorEnum(TiposEnum.TipoICMSClasseConsumo.zero);
            _icms.FaseOuTipoDeUltilizacao = PegarValorEnum(TiposEnum.TipoICMSUltilizacao.ProvimentoDeAcessoInternet);
            _icms.GrupoDeTensao = PegarValorEnum(TiposEnum.TipoICMSGrupoDeTensao.Zeros);
            _icms.CodigoDeIdentificacaoDoConsumidorOuAssinante = db.cliente.First(v => v.RazaoSocial.Contains(Razao)).CodRelacionamento.PadRight(12, ' ');
            _icms.DataDeEmissao = DateTime.Now.ToString("yyyyMMdd");
            _icms.Modelo = PegarValorEnum(TiposEnum.TipoICMSModelo.NotaFiscalDeServicoDeTelecomunicacoes_Modelo22);
            _icms.Serie = nota.InfRps.IdentificacaoRps.Serie.PadRight(3, ' ');
            _icms.Numero = nota.InfRps.IdentificacaoRps.Numero.PadLeft(9, '0');
            _icms.CodigoDeIdentificacaoDoConsumidorOuAssinante = new Utils.VerificacaoValidacao().GerarHashMd5(_icms.CnpjCpf + _icms.Numero + _icms.ValorTotal + _icms.BcIcms + _icms.IcmsDestacado + _icms.DataDeEmissao + _icms.CnpjDoEmitente).PadRight(32, ' ');//01, 12, 14, 15, 16, 09 e 27
            _icms.BcIcms = nota.InfRps.Servico.Valores.ValorServicos.ToString().Replace(".", "").Replace(",", "").PadLeft(12, '0');
            _icms.IcmsDestacado = new Utils.Calculos().CalculoPorcentagem((double)nota.InfRps.Servico.Valores.ValorServicos, 38).ToString().PadLeft(12, '0');
            _icms.OperacoesIsentasOuNaoTributadas = "000000000000";
            _icms.OutrosValores = "000000000000";
            _icms.SituacaoDoDocumento = PegarValorEnum(TiposEnum.TipoICMSStatus.Demais_Documentos);
            _icms.AnoMesDeReferenciaDeApuracao = DateTime.Now.ToString("yyMM");
            _icms.ReferenciaAoItemDaNf = "000000001";
            _icms.NumeroDoTerminalTelefonicoOuDaUnidadeConsumidora = "            ";
            _icms.IdentificacaoDoTipoDeInformacaoContidaCampo1 = db.cliente.First(v => v.RazaoSocial.Contains(Razao)).CpfCnpj.Length == 18 ? PegarValorEnum(TiposEnum.TipoICMSTipoCampo1.CNPJ) : PegarValorEnum(TiposEnum.TipoICMSTipoCampo1.CPF);
            _icms.TipoDeCliente = PegarValorEnum(TiposEnum.TipoICMSTipoCliente.Comercial);
            _icms.SubClasseDeConsumo = "00";
            _icms.NumeroDoTerminalTelefonicoPrincipal = "            ";
            _icms.CnpjDoEmitente = emitente.Replace(".", "").Replace("-", "").Replace("/", "").Replace("_", "").PadRight(14, ' ');
            _icms.NumeroDoCodigoOuFaturaComercial = " ".PadRight(20, ' ');
            _icms.ValorTotalDaFaturaComercial = nota.InfRps.Servico.Valores.ValorServicos.ToString().Replace(",", "").Replace(".", "").PadLeft(9, '0');
            _icms.DataDeLeituraAnterior = "        ";
            _icms.DataDeLeituraAtual = "        ";
            _icms.Brancos1 = "".PadRight(50, ' ');
            _icms.Brancos2 = "".PadRight(8, ' ');
            _icms.InformacoesAdicionais = "".PadRight(38, ' ');
            _icms.Brancos3 = "".PadRight(5, ' ');
            _icms.CodigoDeAutenticacaoDIgitalDoRegistro = new Utils.VerificacaoValidacao().GerarHashMd5
                (

            #region Campos
                 _icms.CnpjCpf +
                 _icms.IE +
                 _icms.RazaoSocial +
                 _icms.Uf +
                 _icms.ClasseDeConsumo +
                 _icms.FaseOuTipoDeUltilizacao +
                 _icms.GrupoDeTensao +
                 _icms.CodigoDeIdentificacaoDoConsumidorOuAssinante +
                 _icms.DataDeEmissao +
                 _icms.Modelo +
                 _icms.Serie +
                 _icms.Numero +
                 _icms.CodigoDeAutenticacaoDigitalDoDocumentoFiscal +
                 _icms.ValorTotal +
                 _icms.BcIcms +
                 _icms.IcmsDestacado +
                 _icms.OperacoesIsentasOuNaoTributadas +
                 _icms.OutrosValores +
                 _icms.SituacaoDoDocumento +
                 _icms.AnoMesDeReferenciaDeApuracao +
                 _icms.ReferenciaAoItemDaNf +
                 _icms.NumeroDoTerminalTelefonicoOuDaUnidadeConsumidora +
                 _icms.IdentificacaoDoTipoDeInformacaoContidaCampo1 +
                 _icms.TipoDeCliente +
                 _icms.SubClasseDeConsumo +
                 _icms.NumeroDoTerminalTelefonicoPrincipal +
                 _icms.CnpjDoEmitente +
                 _icms.NumeroDoCodigoOuFaturaComercial +
                 _icms.ValorTotalDaFaturaComercial +
                 _icms.DataDeLeituraAnterior +
                 _icms.DataDeLeituraAtual +
                 _icms.Brancos1 +
                 _icms.Brancos2 +
                 _icms.InformacoesAdicionais +
                 _icms.Brancos3
            #endregion

                 ).PadRight(32, ' ');
            return _icms;
        }
        public Icms.Item GeraItemUnico(EnviarLoteRpsEnvioLoteRpsRps nota)
        {
            Icms.Item _item = new Icms.Item();
            _item.CnpjCpf = nota.InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cnpj.ToString().PadLeft(14, ' ');
            _item.UF = nota.InfRps.Tomador.Endereco.Uf;
            _item.ClasseDeConsumo = TiposEnum.TipoICMSClasseConsumo.zero;
            _item.FaseOuTipoDeUltilizacao = TiposEnum.TipoICMSUltilizacao.ProvimentoDeAcessoInternet;
            _item.GrupoDeTensao = TiposEnum.TipoICMSGrupoDeTensao.Zeros;
            _item.DataDeEmissao = DateTime.Now.ToString("yyyMMdd");
            _item.Modelo = TiposEnum.TipoICMSModelo.NotaFiscalDeServicoDeTelecomunicacoes_Modelo22;
            _item.Serie = nota.InfRps.IdentificacaoRps.Serie.PadRight(3, ' ');
            _item.Numero = nota.InfRps.IdentificacaoRps.Numero.PadLeft(9, '0');
            _item.CFOP = "";
            _item.NDeOrdemDoItem = "001";
            _item.CodigoDoItem = "".PadRight(10, ' ');
            _item.DescricaoDoItem = "MUNDIACCESS - ACESSO A INTERNET".PadRight(40, ' ');
            _item.CodigoDeClassificacaoDoItem = "0104";
            _item.Unidade = "".PadRight(6, ' ');
            _item.QuantidadeContratada = "000000000000";
            _item.QuantidadeMedida = "000000000000";
            _item.Total = nota.InfRps.Servico.Valores.ValorServicos.ToString().Replace(".", "").Replace(",", "").PadLeft(11, '0');
            _item.DescontosRedutores = "00000000000";
            _item.AcrescimosDespesasAcessorias = "00000000000";
            _item.BcIcms = nota.InfRps.Servico.Valores.ValorServicos.ToString().Replace(".", "").Replace(",", "").PadLeft(11, '0');
            _item.Icms = new Utils.Calculos().CalculoPorcentagem((double)nota.InfRps.Servico.Valores.ValorServicos, 38).ToString().PadLeft(11, '0');
            _item.OperacoesIsentasOuNaoTributadas = "00000000000";
            _item.Outrosvalores = "00000000000";
            _item.AliquotaIcms = "38";
            _item.Situacao = PegarValorEnum(TiposEnum.TipoICMSStatus.Demais_Documentos);
            _item.AnoMesDeReferenciaDeApuracao = DateTime.Now.ToString("yyMM");
            _item.NumeroDoContrato = "".PadRight(15, ' ');
            _item.QuantidadeFaturada = "000000008000";
            _item.TarifaAplicada = "00000000000";
            _item.AliquotaPis = "006500";
            _item.Pis = "00000000117";
            _item.AliquotaCofins = "030000";
            _item.Cofins = "00000000540";
            _item.IndicadorDeDescontoJudicial = " ";
            _item.TipoDeIsencao = "00";
            _item.Branco = "     ";
            _item.CodigoDeAutenticacaoDigitalDoRegistro = new Utils.VerificacaoValidacao().GerarHashMd5
                (
                 _item.CnpjCpf +
                 _item.UF +
                _item.ClasseDeConsumo +
                 _item.FaseOuTipoDeUltilizacao +
                 _item.GrupoDeTensao +
                 _item.DataDeEmissao +
                 _item.Modelo +
                 _item.Serie +
                 _item.Numero +
                 _item.CFOP +
                 _item.NDeOrdemDoItem +
                 _item.CodigoDoItem +
                 _item.DescricaoDoItem +
                 _item.CodigoDeClassificacaoDoItem +
                 _item.Unidade +
                 _item.QuantidadeContratada +
                 _item.QuantidadeMedida +
                 _item.Total +
                 _item.DescontosRedutores +
                 _item.AcrescimosDespesasAcessorias +
                 _item.BcIcms +
                 _item.Icms +
                 _item.OperacoesIsentasOuNaoTributadas +
                 _item.Outrosvalores +
                 _item.AliquotaIcms +
                 _item.Situacao +
                 _item.AnoMesDeReferenciaDeApuracao +
                 _item.NumeroDoContrato +
                 _item.QuantidadeFaturada +
                 _item.TarifaAplicada +
                 _item.AliquotaPis +
                 _item.Pis +
                 _item.AliquotaCofins +
                 _item.Cofins +
                 _item.IndicadorDeDescontoJudicial +
                 _item.TipoDeIsencao +
                 _item.Branco
                 );


            return _item;
        }
        public Icms.Dados GeraDadosUnico(EnviarLoteRpsEnvioLoteRpsRps nota)
        {
            Icms.Dados _dados = new Icms.Dados();
            string Ender = nota.InfRps.Tomador.Endereco.Endereco; ;
            string Razao = nota.InfRps.Tomador.RazaoSocial;
            _dados.CnpjCpf = nota.InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cnpj.ToString().PadLeft(14, ' ');
            _dados.Ie = db.cliente.First(v => v.RazaoSocial.Contains(Razao)).InscEstadual == null ? "ISENTO" : db.cliente.First(v => v.RazaoSocial.Contains(Razao)).InscEstadual.Replace(".", "").Replace("-", "").Replace("_", "").Replace("/", "").PadRight(14, ' ');
            _dados.RazaoSocial = Razao.PadRight(35, ' ');
            _dados.Logradouro = nota.InfRps.Tomador.Endereco.Endereco.PadRight(45, ' ');
            _dados.Numero = nota.InfRps.Tomador.Endereco.Numero.ToString().PadLeft(5, '0');
            _dados.Complemento = nota.InfRps.Tomador.Endereco.Complemento == null ? "NAO DEFINIDO".PadRight(15, ' ') : nota.InfRps.Tomador.Endereco.Complemento.PadRight(15, ' ');
            _dados.Cep = nota.InfRps.Tomador.Endereco.Cep;
            _dados.Bairro = nota.InfRps.Tomador.Endereco.Bairro.PadRight(15, ' ');
            _dados.Municipio = nota.InfRps.Tomador.Endereco.Bairro.PadRight(30, ' ');
            _dados.Uf = nota.InfRps.Tomador.Endereco.Uf;
            _dados.TelefoneDeContato = "            ";
            _dados.CodigoDeIdentificacaoDoConsumidor = db.cliente.First(v => v.RazaoSocial.Contains(Razao)).CodRelacionamento.PadRight(12, ' ');
            _dados.NumeroDoterminaltelefonicoOuUnidadeConsumidora = "            ";
            _dados.UfDeHabilitacaoDoTerminal = "  ";
            _dados.DataDeEmissao = DateTime.Now.ToString("yyyyMMdd");
            _dados.Modelo = PegarValorEnum(TiposEnum.TipoICMSModelo.NotaFiscalDeServicoDeTelecomunicacoes_Modelo22);
            _dados.Serie = nota.InfRps.IdentificacaoRps.Serie.PadRight(3, ' ');
            _dados.Numero = nota.InfRps.IdentificacaoRps.Numero.PadLeft(9, '0');
            _dados.CodigoMunicipio = nota.InfRps.Tomador.Endereco.CodigoMunicipio.ToString();
            _dados.Brancos = "     ";
            _dados.CodigoDeAutenticacaoDoRegistro = new Utils.VerificacaoValidacao().GerarHashMd5
                (

            _dados.CnpjCpf +
            _dados.Ie +
            _dados.RazaoSocial +
            _dados.Logradouro +
            _dados.Numero +
            _dados.Complemento +
            _dados.Cep +
            _dados.Bairro +
            _dados.Municipio +
            _dados.Uf +
            _dados.TelefoneDeContato +
            _dados.CodigoDeIdentificacaoDoConsumidor +
            _dados.NumeroDoterminaltelefonicoOuUnidadeConsumidora +
            _dados.UfDeHabilitacaoDoTerminal +
            _dados.DataDeEmissao +
            _dados.Modelo +
            _dados.Serie +
            _dados.Numero +
            _dados.CodigoMunicipio +
            _dados.Brancos
                );

            return _dados;
        }
        public void GeraMestre(Icms Mestre)
        {

        }


        public static string PegarValorEnum(Enum val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }


    }
}
