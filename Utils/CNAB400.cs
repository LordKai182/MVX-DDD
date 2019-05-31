using Impactro.Cobranca;
using Impactro.WindowsControls;
using Infra;
using Infra.Entidades;
using Infra.EntidadesNaoPersistidas;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;

namespace Utils
{
    public class CNAB400
    {
     
        /// <summary>
        /// CLASSE DE BOLETO
        /// </summary>
        public class Boleto : CNAB400
        {
            Class1 db = new Class1(true);
            string _CenarioCod = string.Empty;
            string _Plano = string.Empty;
            string _Lote = string.Empty;
            public void GeraBoleto(CedenteInfo objCedente, SacadoInfo objSacado, BoletoInfo objBoleto, string cCenarioCod, string pPlano, string Lote, List<LinhaCobranca> ListaLinhaCobranca, int RemessaId)
            {
                _CenarioCod = cCenarioCod;
                _Lote = Lote;
                _Plano = pPlano;
                BoletoForm bltFrm = new BoletoForm();
                bltFrm.MakeBoleto(objCedente, objSacado, objBoleto);
                bltFrm.Boleto.Escala = (double)6;
                FieldDraw.FontCampoName = "Verdana";
                FieldDraw.FontCampoSize = (float)(5 * 6 / 3);
                FieldDraw.FontCampoStyle = FontStyle.Regular;

                // Tamanho dos valores dos campos
                FieldDraw.FontValorName = "Arial";
                FieldDraw.FontValorSize = (float)(7 * 6 / 3);
                FieldDraw.FontValorStyle = FontStyle.Bold;

                // Tamanho da linha digitável
                FieldDraw.FontLinhaSize = (float)(9 * 6 / 3);
                FieldDraw.FontLinhaName = "Arial";
                FieldDraw.FontLinhaStyle = FontStyle.Bold;

                // Recria as instancias dos fontes
                FieldDraw.Reset();

                //bltFrm.Boleto.Escala = (double)6;
                //if(File.Exists(HttpContext.Current.Server.MapPath("~/App_Data/BoletosPDF/CodigoDeBarras.jpg")))
                //{
                //File.Delete(HttpContext.Current.Server.MapPath("~/App_Data/BoletosPDF/CodigoDeBarras.jpg"));
                //}
                //bltFrm.Boleto.CalculaBoleto();
                //CobUtil.BarCodeImage(bltFrm.Boleto.CodigoBarras).Save(HttpContext.Current.Server.MapPath("~/App_Data/BoletosPDF/CodigoDeBarras.jpg"));
                //new MVX.Print.Boleto().Gera(bltFrm);
                //bltFrm.Boleto.LocalPagamento = "Pague Preferencialmente no BANCO NOSSA CAIXA S.A. ou na rede bancária até o vencimento";
                //bltFrm.Boleto.Save(HttpContext.Current.Server.MapPath("~/App_Data/Boletos/" + bltFrm.Boleto.NumeroDocumento+".jpg"));
                //ImagensParaPDF(HttpContext.Current.Server.MapPath("~/App_Data/Boletos/"), HttpContext.Current.Server.MapPath("~/App_Data/BoletosPDF/"), bltFrm.Boleto.NumeroDocumento);
                //File.Delete(HttpContext.Current.Server.MapPath("~/App_Data/Boletos/" + bltFrm.Boleto.NumeroDocumento + ".jpg"));
                GravaBoleto(bltFrm, bltFrm.Boleto.NumeroDocumento,ListaLinhaCobranca, RemessaId);

            }

            #region POG

            public string SetNewName(string Dir, string OldFileName)
            {
                System.Collections.ArrayList fileList = new System.Collections.ArrayList();
                string NewFileName = string.Empty;
                string[] files = Directory.GetFiles(Dir);
                for (int i = 0; i < files.Length; i++)
                {
                    if(files[i].Contains(OldFileName))
                    {
                        fileList.Add(files[i]);
                    }
                }
                NewFileName = Dir + "\\" + OldFileName + " _ " + fileList.Count + ".pdf";

                return NewFileName;
            }
       
            public string[] GetImageFiles(string ImageSource)
            {
                System.Collections.ArrayList Files = new System.Collections.ArrayList();
                string[] FilesArray = Directory.GetFiles(ImageSource);
                foreach (string file in FilesArray)
                {
                    string extension = file.Substring(file.LastIndexOf(".")).ToUpper();
                    if (extension.CompareTo(".JPG") == 0 || extension.CompareTo(".JPEG") == 0 || extension.CompareTo(".GIF") == 0)
                    {
                        Files.Add(file);
                    }
                }

                string[] returnFiles = new string[Files.Count];
                for (int i = 0; i < Files.Count; i++)
                {
                    returnFiles[i] = Files[i].ToString();
                }

                return returnFiles;
            }
            #endregion

            private void GravaBoleto(BoletoForm bltFrm, string CaminhoBoleto,List<LinhaCobranca> ListaLinhaCobranca, int RemessaId)
            {
                string cnnpj = bltFrm.Boleto.SacadoDocumento.Replace("CNPJ: ","").Replace(".", "").Replace("/", "").Replace("-", "");
                var Cliente = db.cliente.First(x => x.CpfCnpj == cnnpj);
                 Infra.Entidades.Boleto _boleto = new Infra.Entidades.Boleto();
                _boleto.CodigoBanco = bltFrm.Boleto.BancoCodigo;
                _boleto.DataCriacao = DateTime.Now;
                _boleto.DataEmissao = bltFrm.Boleto.DataDocumento;
                _boleto.DataVencimentoOriginal = bltFrm.Boleto.DataVencimento;
                _boleto.ValorBoleto = (decimal)bltFrm.Boleto.ValorDocumento;
                _boleto.ValorMulta = (decimal)bltFrm.Boleto.ValorMoraMulta;
                _boleto.NossoNumero = bltFrm.Boleto.NossoNumero;
                _boleto.Cedente = bltFrm.Boleto.Cedente;
                _boleto.CnpjCedente = "CNPJ: " + @String.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToInt64(bltFrm.Boleto.CedenteDocumento.Replace(".", "").Replace("-", "").Replace("/", "")));
                _boleto.Sacado = bltFrm.Boleto.Sacado;
                _boleto.CnpJCpfSacado = bltFrm.Boleto.SacadoDocumento;
                _boleto.NumeroDocumento = bltFrm.Boleto.NumeroDocumento;
                _boleto.Aceite = bltFrm.Boleto.Aceite;
                _boleto.Agencia = bltFrm.Boleto.Agencia;
                _boleto.AutenticacaoCodigo = bltFrm.Boleto.LinhaDigitavel;
                _boleto.Observacao = bltFrm.Boleto.CodigoBarras;
                _boleto.EspecieDocumento = "DM";
                _boleto.EspecieMoeda = bltFrm.Boleto.MoedaEspecie;
                _boleto.localPagamento = bltFrm.Boleto.LocalPagamento;
                _boleto.NossoNumero = bltFrm.Boleto.NossoNumero;
                _boleto.NumeroDocumento = bltFrm.Boleto.NumeroDocumento;
                _boleto.SacadoNome = bltFrm.Boleto.Sacado;
                _boleto.BoletoRemessaId = RemessaId;
                _boleto.Carteira = bltFrm.Boleto.Carteira;
                _boleto.CenarioId = 1;
                _boleto.UsuarioId = 1;
                _boleto.LinhaCobrancaId = ListaLinhaCobranca.First(x => x.ClienteId == Cliente.ClienteId).LinhaCobrancaId;
                db.Boleto.Add(_boleto);
                db.SaveChanges();
                //GuardaBau(_boleto, bltFrm);
            }
            public void GuardaBau(Infra.Entidades.Boleto _Boleto, BoletoForm bltFrml)
            {
                CenarioArquivo _cenarioArq = new CenarioArquivo();

                _cenarioArq.CenarioId = 1;
                _cenarioArq.cliente = bltFrml.Boleto.Sacado;
                _cenarioArq.cnpjCpf = bltFrml.Boleto.SacadoDocumento;
                _cenarioArq.dataCriacao = DateTime.Now;
                _cenarioArq.plano = _Plano;
                _cenarioArq.responsavel = "Teste";
                _cenarioArq.NumeroDocumento = bltFrml.Boleto.NumeroDocumento;
                _cenarioArq.tipoArquivo ="BL";
                _cenarioArq.Valor = Convert.ToDecimal(bltFrml.Boleto.ValorDocumento);
                _cenarioArq.RemessaLote = _Lote;
                _cenarioArq.Vencimento = _Boleto.DataVencimentoOriginal.Day;
                db.CenarioArquivos.Add(_cenarioArq);
                db.SaveChanges();
            }
            public static byte[] BitmapToByteArray(string jpg)
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath("~/App_Data/BoletosPDF/" + jpg + ".pdf"));
                byte[] bytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    bytes = ms.ToArray();
                }

                return bytes;
            }
            public static Image byteArrayToImage(byte[] byteArrayIn)
            {
                MemoryStream ms = new MemoryStream(byteArrayIn);
                Image returnImage = Image.FromStream(ms);
                return returnImage;
            }
        }

        public class NFSe : CNAB400
        {
            Class1 db = new Class1(true);
            #region METODOS PRIVADOS
            protected string NFCenarioCod { get; set; }
            protected string NFPlano { get; set; }

            private NFSE.Net.Core.Empresa RetornaEmpresa(bool criptografado)
            {

                var empresa = new NFSE.Net.Core.Empresa();
                empresa.Nome = "MUNDIVOX COMUNICACOES LTDA ";
                empresa.CNPJ = "18522913000303";
                empresa.Certificado = @"MUNDIVOX.";

                empresa.InscricaoMunicipal = "1037708001X";

                empresa.CertificadoArquivo = HttpContext.Current.Server.MapPath("~/App_Data/Certificados/MUNDIVOX.pfx");
                if (criptografado)
                    empresa.CertificadoSenha = NFSE.Net.Certificado.Criptografia.criptografaSenha("Mund1v0x!@#");
                else
                    empresa.CertificadoSenha = "Mund1v0x!@#";

                empresa.tpAmb = 2;
                empresa.tpEmis = 1;
                empresa.CodigoMunicipio = 3106200;
                return empresa;

            }
            private void GuardarBauNota(string caminhoXml)
            {

                XmlSerializer serializer = new XmlSerializer(typeof(NFSE.Net.Layouts.BHISS.ConsultarLoteRpsResposta));
                StreamReader reader = new StreamReader(Path.Combine(caminhoXml, "nota.xml"));
                NFSE.Net.Layouts.BHISS.ConsultarLoteRpsResposta retorno = (NFSE.Net.Layouts.BHISS.ConsultarLoteRpsResposta)serializer.Deserialize(reader);

                #region BAU
                Bau re = new Bau();
                List<Infra.Entidades.CenarioArquivo> lstBau = new List<Infra.Entidades.CenarioArquivo>();
                foreach (var item in retorno.ListaNfse)
                {

                    Infra.Entidades.CenarioArquivo _bau = new Infra.Entidades.CenarioArquivo();
                    _bau.CenarioId = 1;
                    //_bau.plano = "NIKOLA";
                    //_bau.responsavel = "Henrique";
                    //_bau.arquivo =     new Utils.ultArquivosPastas().ArquivoStringParaByteArea(ObjParaString<NFSE.Net.Layouts.BHISS.ConsultarLoteRpsRespostaCompNfse>(item));
                    //_bau.tipoArquivo = TiposEnum.TipoArquivo.ISS.ToString();
                    //_bau.faturado = "Sim";
                    //_bau.cliente = item.Nfse.InfNfse.TomadorServico.RazaoSocial;
                    //_bau.cnpjCpf = item.Nfse.InfNfse.TomadorServico.IdentificacaoTomador.CpfCnpj.Cnpj.ToString();


                    lstBau.Add(_bau);
                }

                //re.AdicionaBau(lstBau);


                #endregion



            }
            private string ObjParaString<T>(T objeto)
            {


                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add("", "");
                XmlSerializer infoSerializer = new XmlSerializer(typeof(T));
                StringWriter myWriter = new StringWriter();
                infoSerializer.Serialize(myWriter, objeto, namespaces);
                string forma = myWriter.ToString().Replace("q1:", "").Replace(":q1", "").Replace("utf-16", "utf-8");

                return forma;

            }
            #endregion
            public string GeraDiscriminacao(List<Nota> lstBoleto, string cliente, string empresa, string imposto)
            {
                string retorno = string.Empty;

                foreach (var item in lstBoleto.Where(x => x.Cedente.Contains(cliente) && x.Sacado.Contains(empresa) && x.Imposto.Contains(imposto)).GroupBy(x => x.Contrato))
                {

                    retorno = retorno + item.Count().ToString() + "-" + item.First().Contrato + "|" + item.Sum(x => x.valorDocumento) + ";";
                   
                }

                return retorno;
            }
            public string GeraDiscriminacaoGroup(List<Nota> lstBoleto, string cliente, string empresa, string imposto)
            {
                string retorno = string.Empty;

                foreach (var item in lstBoleto.Where(x => x.Cedente.Contains(cliente) && x.Sacado.Contains(empresa) && x.Imposto.Contains(imposto)).GroupBy(x => x.Contrato))
                {
                    retorno = retorno + item.Count().ToString() + "-" + item.First().Contrato+"|"+item.Sum(x=>x.valorDocumento)+";";
                   
                    ///retorno = retorno + Environment.NewLine;
                }

                return retorno;
            }
            public string GeraDiscriminacao(List<Nota> lstBoleto, string cliente, string empresa)
            {
                string retorno = string.Empty;

                foreach (var item in lstBoleto.Where(x => x.Cedente.Contains(cliente) && x.Sacado.Contains(empresa)).GroupBy(x => x.Contrato))
                {

                    retorno = retorno + item.Count().ToString() + "-" + item.First().Contrato+"|"+item.Sum(x=>x.valorDocumento)+";";
                    //foreach (var prop in item.OrderBy(x => x.Propriedades))
                    //{
                    //    retorno = retorno + prop.Propriedades + "M, ";
                    //}

                    //retorno = retorno + Environment.NewLine;
                }

                return retorno;
            }

            public void ConstroiDebito(List<Nota> lstBoleto, List<Utils.Remessa> ListaRemessa, string cenario, string plano,List<LinhaCobranca> ListaLinhaCobranca)
            {

                NFCenarioCod = cenario;
                NFPlano = plano;
                NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvio _nfse = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvio();
                foreach (var _nota in ListaRemessa.GroupBy(x => new { x.Cedente.CNPJ ,x.SacadoCleinte._MapaFaturamento.Vencimento}))
                {

                    Empresa _Cedente = new Empresa();
                    string raz = _nota.First().Cedente.CNPJ.Replace(".", "").Replace("/", "").Replace("-", "");
                    _Cedente = db.Empresa.First(x => x.Cnpj.Contains(raz));
                    MapeamentoFiscal _Mapeamento = new MapeamentoFiscal();
                    _Mapeamento = db.MapeamentoFiscal.First(x => x.EmpresaId == _Cedente.EmpresaId);

                    #region IDENTIFICACAO

                    _nfse = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvio();
                    _nfse.LoteRps = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRps();

                    #region LOTE
                    _nfse.LoteRps.Cnpj = _Cedente.Cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
                    _nfse.LoteRps.versao = 1.00M;
                    _nfse.LoteRps.Id = "lote";
                    _nfse.LoteRps.NumeroLote = _Mapeamento.LoteDebito.ToString();
                    _nfse.LoteRps.InscricaoMunicipal = _Cedente.InscMunicipal;
                    _nfse.LoteRps.QuantidadeRps = Convert.ToInt32(_nota.GroupBy(x => new { x.SacadoCleinte._SacadoInfo.Documento }).Count()).ToString();
                    #endregion

                    #endregion
                    try
                    {

                        #region RPS

                        _nfse.LoteRps.ListaRps = Enumerable.Range(0, (_nota.GroupBy(x => new { x.SacadoCleinte._SacadoInfo.Documento }).Count())).Select(g => new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRps()).ToArray();

                        int i = 0;
                        foreach (var item in _nota.GroupBy(x => new { x.SacadoCleinte._SacadoInfo.Documento }))
                        {


                            Cliente _Cliente = new Cliente();
                            string cn = item.First().SacadoCleinte._SacadoInfo.Documento;
                            _Cliente = db.cliente.First(x => x.CpfCnpj.Contains(cn));
                            #region INFRPS

                            _nfse.LoteRps.ListaRps[i].InfRps = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRps();
                            _nfse.LoteRps.ListaRps[i].InfRps.Id = "nfse" + (i + 1).ToString();

                            
                            _nfse.LoteRps.ListaRps[i].InfRps.DataEmissao = DateTime.Now;

                            _nfse.LoteRps.ListaRps[i].InfRps.RegimeEspecialTributacao = Convert.ToByte(_Mapeamento.RegimeEspecialTributacao).ToString();

                            _nfse.LoteRps.ListaRps[i].InfRps.IncentivadorCultural = Convert.ToByte(_Mapeamento.IncentivadorCultural);

                            _nfse.LoteRps.ListaRps[i].InfRps.NaturezaOperacao = Convert.ToByte(_Mapeamento.NaturezaOperacao);

                            _nfse.LoteRps.ListaRps[i].InfRps.OptanteSimplesNacional = Convert.ToByte(_Mapeamento.OptanteSimplesNacional);

                            _nfse.LoteRps.ListaRps[i].InfRps.Status = Convert.ToByte(item.First().SacadoCleinte._MapaFaturamento.Vencimento);

                            #endregion

                            #region SERVIÇO
                            _nfse.LoteRps.ListaRps[i].InfRps.Servico = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsServico();

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.CodigoMunicipio = Convert.ToUInt32(RetornaEmpresa(true).CodigoMunicipio);

                            
                            DateTime Vencimento = new DateTime();
                            DateTime Prestacao = new DateTime();
                            int Diavenci = item.First().SacadoCleinte._MapaFaturamento.Vencimento;
                            new Utils.retornos().RetornaPrestacao(item.First().SacadoCleinte._MapaFaturamento.MesParcelas, ref Prestacao, ref Vencimento);

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.CodigoTributacaoMunicipio = Diavenci+"/"+Vencimento.Month+"/"+Vencimento.Year;

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.ItemListaServico = Convert.ToDecimal(14.01);

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.Discriminacao = GeraDiscriminacaoGroup(lstBoleto, item.First().Cedente.CNPJ, item.First().SacadoCleinte._SacadoInfo.Documento, "DEBITO");


                            #region SERVIÇO VALORES
                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.Valores = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsServicoValores();

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.Valores.ValorServicos = Convert.ToDecimal(item.Sum(x => x.SacadoCleinte.ListaBoleto.Sum(c => c.ValorDocumento)));

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.Valores.IssRetido = Convert.ToByte(2);

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.Valores.Aliquota = Convert.ToDecimal(0.05);


                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.Valores.ValorIss = Convert.ToByte(new Utils.Calculos().CalculoPorcentagem(0.05, (double)Convert.ToDecimal(item.Sum(x => x.SacadoCleinte.ListaBoleto.Sum(c => c.ValorDocumento)))));


                            #endregion


                            #endregion

                            #region INDENTIFICACAO
                            _nfse.LoteRps.ListaRps[i].InfRps.IdentificacaoRps = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsIdentificacaoRps();
                            _nfse.LoteRps.ListaRps[i].InfRps.IdentificacaoRps.Numero = Convert.ToInt32(Convert.ToInt32(_Mapeamento.NumeroDebito) + 1).ToString();
                            _Mapeamento.NumeroDebito = _nfse.LoteRps.ListaRps[i].InfRps.IdentificacaoRps.Numero.ToString();
                            _nfse.LoteRps.ListaRps[i].InfRps.IdentificacaoRps.Serie = "DBT";

                            _nfse.LoteRps.ListaRps[i].InfRps.IdentificacaoRps.Tipo = Convert.ToByte(1);

                            #endregion

                            #region PRESTADOR
                            _nfse.LoteRps.ListaRps[i].InfRps.Prestador = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsPrestador();

                            _nfse.LoteRps.ListaRps[i].InfRps.Prestador.Cnpj = _Cedente.Cnpj;

                            _nfse.LoteRps.ListaRps[i].InfRps.Prestador.InscricaoMunicipal = _Cedente.InscMunicipal;

                            #endregion

                            #region TOMADOR

                            #region TOMADOR IDENTIFICACAO
                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsTomador();
                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.IdentificacaoTomador = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsTomadorIdentificacaoTomador();
                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.IdentificacaoTomador.CpfCnpj = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsTomadorIdentificacaoTomadorCpfCnpj();


                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cnpj = item.FirstOrDefault().SacadoCleinte._SacadoInfo.Documento;


                            #endregion

                            #region TOMADOR ENDEREÇO
                            ClienteEndereco Endereco = new ClienteEndereco();
                            try
                            {
                                Endereco = item.First().SacadoCleinte._MapaFaturamento._Contrato._InstalacaoEndereco.FirstOrDefault(f => f.TipoLogradouroId == 2);

                            }
                            catch
                            {
                                int ContratNrcId = (int)item.First().SacadoCleinte._MapaFaturamento.ContratoNrcId;
                                var nrcContrato = db.ContratoNrc.First(x => x.ContratoNrcId == ContratNrcId);
                                Endereco = nrcContrato._Contrato._InstalacaoEndereco.FirstOrDefault(f => f.TipoLogradouroId == 2);
                            }


                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsTomadorEndereco();


                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Bairro = Endereco._Bairro.Nome;

                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Cep = Endereco.Cep;

                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.CodigoMunicipio = Convert.ToUInt32(Endereco._Bairro._Cidade.CodIbge);

                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Complemento = Endereco.Complemento;

                            try
                            {
                                _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Endereco = Endereco.Logradouro + "-N:" + Endereco.Numero + "- Sl:" + Endereco.Sala + "- Andar:" + Endereco.Andar + "- Cep:" + Endereco.Cep;

                            }
                            catch
                            {

                                _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Endereco = Endereco.Logradouro;

                            }

                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Numero = Convert.ToInt32(Endereco.Numero);

                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Uf = Endereco._Bairro._Cidade._Estado.Estadosigla;

                            #endregion


                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.RazaoSocial = _Cliente.RazaoSocial;

                            _Mapeamento.LoteDebito = (_Mapeamento.LoteDebito + 1);

                            #endregion
                            db.MapeamentoFiscal.Update(_Mapeamento);
                            db.SaveChanges();

                            i++;

                        }

                        #endregion


                    }
                    catch (Exception erro)
                    {

                    }

                    GuardaBauNF(_nfse, TiposEnum.TipoImposto.DEBITO.ToString(), ListaLinhaCobranca);

                }


                #region ICMS



                #endregion
            }
            public void ConstroiICMS_ELE(List<Nota> lstBoleto, List<Utils.Remessa> ListaRemessa, string cenario, string plano,List<LinhaCobranca> ListaLinhaCobranca)
            {

                NFCenarioCod = cenario;
                NFPlano = plano;
                NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvio _nfse = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvio();

                #region NOTA

                foreach (var _nota in ListaRemessa.GroupBy(x => new { x.Cedente.CNPJ }))
                {

                    Empresa _Cedente = new Empresa();
                    string raz = _nota.First().Cedente.Cedente;
                    _Cedente = db.Empresa.First(x => x.RazaoSocial.Contains(raz));
                    MapeamentoFiscal _Mapeamento = new MapeamentoFiscal();
                    _Mapeamento = db.MapeamentoFiscal.First(x => x.EmpresaId == _Cedente.EmpresaId);

                    #region IDENTIFICACAO

                    _nfse = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvio();
                    _nfse.LoteRps = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRps();

                    #region LOTE
                    _nfse.LoteRps.Cnpj = _Cedente.Cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
                    _nfse.LoteRps.versao = 1.00M;
                    _nfse.LoteRps.Id = "lote";
                    _nfse.LoteRps.NumeroLote = _Mapeamento.LoteIcms.ToString();
                    _nfse.LoteRps.InscricaoMunicipal = _Cedente.InscMunicipal;
                    _nfse.LoteRps.QuantidadeRps = Convert.ToInt32(_nota.GroupBy(x => new { x.SacadoCleinte._SacadoInfo.Documento, x.SacadoCleinte._MapaFaturamento.Vencimento, x.SacadoCleinte._MapaFaturamento.MesParcelas }).Count()).ToString();
                    #endregion

                    #endregion
                    try
                    {

                        #region RPS

                        _nfse.LoteRps.ListaRps = Enumerable.Range(0, (_nota.GroupBy(x => new { x.SacadoCleinte._SacadoInfo.Documento, x.SacadoCleinte._MapaFaturamento.Vencimento, x.SacadoCleinte._MapaFaturamento.MesParcelas }).Count())).Select(g => new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRps()).ToArray();

                        int i = 0;
                        foreach (var item in _nota.GroupBy(x => new { x.SacadoCleinte._SacadoInfo.Documento, x.SacadoCleinte._MapaFaturamento.Vencimento, x.SacadoCleinte._MapaFaturamento.MesParcelas, x.SacadoCleinte._MapaFaturamento._Contrato._InstalacaoEndereco.First(d => d.TipoLogradouroId == 3).Cep }))
                        {


                            Cliente _Cliente = new Cliente();
                            string cn = item.First().SacadoCleinte._SacadoInfo.Documento;
                            _Cliente = db.cliente.First(x => x.CpfCnpj.Contains(cn));
                            #region INFRPS

                            _nfse.LoteRps.ListaRps[i].InfRps = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRps();
                            _nfse.LoteRps.ListaRps[i].InfRps.Id = "nfse" + (i + 1).ToString();


                            _nfse.LoteRps.ListaRps[i].InfRps.DataEmissao = DateTime.Now;

                            _nfse.LoteRps.ListaRps[i].InfRps.RegimeEspecialTributacao = Convert.ToByte(_Mapeamento.RegimeEspecialTributacao).ToString();

                            _nfse.LoteRps.ListaRps[i].InfRps.IncentivadorCultural = Convert.ToByte(_Mapeamento.IncentivadorCultural);

                            _nfse.LoteRps.ListaRps[i].InfRps.NaturezaOperacao = Convert.ToByte(_Mapeamento.NaturezaOperacao);

                            _nfse.LoteRps.ListaRps[i].InfRps.OptanteSimplesNacional = Convert.ToByte(_Mapeamento.OptanteSimplesNacional);

                            _nfse.LoteRps.ListaRps[i].InfRps.Status = Convert.ToByte(1);

                            #endregion

                            #region SERVIÇO
                            _nfse.LoteRps.ListaRps[i].InfRps.Servico = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsServico();

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.CodigoMunicipio = Convert.ToUInt32(RetornaEmpresa(true).CodigoMunicipio);

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.CodigoTributacaoMunicipio = "140100588";

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.ItemListaServico = Convert.ToDecimal(14.01);

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.Discriminacao = GeraDiscriminacao(lstBoleto, item.First().Cedente.CNPJ, item.First().SacadoCleinte._SacadoInfo.Documento);


                            #region SERVIÇO VALORES
                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.Valores = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsServicoValores();

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.Valores.ValorServicos = Convert.ToDecimal(item.Sum(x => x.SacadoCleinte.ListaBoleto.Sum(c => c.ValorDocumento)));

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.Valores.IssRetido = Convert.ToByte(2);

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.Valores.Aliquota = Convert.ToDecimal(0.05);


                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.Valores.ValorIss = Convert.ToByte(new Utils.Calculos().CalculoPorcentagem(0.05, (double)Convert.ToDecimal(item.Sum(x => x.SacadoCleinte.ListaBoleto.Sum(c => c.ValorDocumento)))));


                            #endregion


                            #endregion

                            #region INDENTIFICACAO
                            _nfse.LoteRps.ListaRps[i].InfRps.IdentificacaoRps = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsIdentificacaoRps();
                            _nfse.LoteRps.ListaRps[i].InfRps.IdentificacaoRps.Numero = Convert.ToInt32(_Mapeamento.NumeroIcms + 1).ToString();
                            _Mapeamento.NumeroIcms = Convert.ToInt32(_nfse.LoteRps.ListaRps[i].InfRps.IdentificacaoRps.Numero);
                            _nfse.LoteRps.ListaRps[i].InfRps.IdentificacaoRps.Serie = _Mapeamento.SerieIcms;

                            _nfse.LoteRps.ListaRps[i].InfRps.IdentificacaoRps.Tipo = Convert.ToByte(1);

                            #endregion

                            #region PRESTADOR
                            _nfse.LoteRps.ListaRps[i].InfRps.Prestador = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsPrestador();

                            _nfse.LoteRps.ListaRps[i].InfRps.Prestador.Cnpj = _Cedente.Cnpj;

                            _nfse.LoteRps.ListaRps[i].InfRps.Prestador.InscricaoMunicipal = _Cedente.InscMunicipal;

                            #endregion

                            #region TOMADOR

                            #region TOMADOR IDENTIFICACAO
                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsTomador();
                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.IdentificacaoTomador = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsTomadorIdentificacaoTomador();
                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.IdentificacaoTomador.CpfCnpj = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsTomadorIdentificacaoTomadorCpfCnpj();


                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cnpj = "99999999000191";


                            #endregion

                            #region TOMADOR ENDEREÇO
                            var Endereco = item.First().SacadoCleinte._MapaFaturamento._Contrato._InstalacaoEndereco.FirstOrDefault(f => f.TipoLogradouroId == 2);

                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsTomadorEndereco();


                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Bairro = Endereco._Bairro.Nome;

                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Cep = Endereco.Cep;

                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.CodigoMunicipio = Convert.ToUInt32(Endereco._Bairro._Cidade.CodIbge);

                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Complemento = Endereco.Complemento;

                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Endereco = Endereco.Logradouro;

                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Numero = Convert.ToInt32(Endereco.Numero);

                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Uf = Endereco._Bairro._Cidade._Estado.Estadosigla;

                            #endregion


                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.RazaoSocial = _Cliente.RazaoSocial;
                            _Mapeamento.LoteIcms = (_Mapeamento.LoteIcms + 1);

                            #endregion
                            db.MapeamentoFiscal.Add(_Mapeamento);
                            db.SaveChanges();

                            i++;

                        }

                        #endregion


                    }
                    catch (Exception erro)
                    {

                    }
                }

                #endregion

                #region ICMS


                GuardaBauNF(_nfse, TiposEnum.TipoImposto.ICMS_FOR.ToString(), ListaLinhaCobranca);

                #endregion
            }

            public void ConstroiICMS(List<Nota> lstBoleto, List<Utils.Remessa> ListaRemessa, string cenario, string plano, List<LinhaCobranca> ListaLinhaCobranca)
            {

                NFCenarioCod = cenario;
                NFPlano = plano;
                NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvio _nfse = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvio();

                #region NOTA

                foreach (var _nota in ListaRemessa.GroupBy(x => new { x.Cedente.CNPJ , x.SacadoCleinte._MapaFaturamento.Vencimento}))
                {

                    Empresa _Cedente = new Empresa();
                    string raz = _nota.First().Cedente.Cedente;
                    _Cedente = db.Empresa.First(x => x.RazaoSocial.Contains(raz));
                    MapeamentoFiscal _Mapeamento = new MapeamentoFiscal();
                    _Mapeamento = db.MapeamentoFiscal.First(x => x.EmpresaId == _Cedente.EmpresaId);

                    #region IDENTIFICACAO

                    _nfse = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvio();
                    _nfse.LoteRps = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRps();

                    #region LOTE
                    _nfse.LoteRps.Cnpj = _Cedente.Cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
                    _nfse.LoteRps.versao = 1.00M;
                    _nfse.LoteRps.Id = "lote";
                    _nfse.LoteRps.NumeroLote = _Mapeamento.LoteIcms.ToString();
                    _nfse.LoteRps.InscricaoMunicipal = _Cedente.InscMunicipal;
                    _nfse.LoteRps.QuantidadeRps = Convert.ToInt32(_nota.GroupBy(x => new { x.SacadoCleinte._SacadoInfo.Documento }).Count()).ToString();
                    #endregion

                    #endregion
                    try
                    {

                        #region RPS

                        _nfse.LoteRps.ListaRps = Enumerable.Range(0, (_nota.GroupBy(x => new { x.SacadoCleinte._SacadoInfo.Documento, x.SacadoCleinte._MapaFaturamento.Vencimento }).Count())).Select(g => new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRps()).ToArray();

                        int i = 0;
                        foreach (var item in _nota.GroupBy(x => new { x.SacadoCleinte._SacadoInfo.Documento }))
                        {


                            Cliente _Cliente = new Cliente();
                            string cn = item.First().SacadoCleinte._SacadoInfo.Documento;
                            _Cliente = db.cliente.First(x => x.CpfCnpj.Contains(cn));
                          
                            #region INFRPS

                            _nfse.LoteRps.ListaRps[i].InfRps = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRps();
                            _nfse.LoteRps.ListaRps[i].InfRps.Id = "nfse" + (i + 1).ToString();

                            ProdutoMapeamento ProdutoMapa = new ProdutoMapeamento();
                            try
                            {
                                int iddf = (int)item.First().SacadoCleinte._MapaFaturamento.ContratoId;
                                var ContratoVinculado = db.Contrato.First(x => x.ContratoId == iddf);
                                int produtoId = ContratoVinculado.ProdutoId;
                                var map = db.ProdutoMapeamento.First(x => x.ProdutoId == produtoId && x.CidadeId == 211);
                                ProdutoMapa = map;
                            }
                            catch
                            {
                                int iddf = (int)item.First().SacadoCleinte._MapaFaturamento.ContratoNrcId;
                                var ContratoVinculado = db.ContratoNrc.First(x => x.ContratoNrcId == iddf);
                                int produtoId = ContratoVinculado.ProdutoId;
                                var map = db.ProdutoMapeamento.First(x => x.ProdutoId == produtoId && x.CidadeId == 211);
                                ProdutoMapa = map;

                            }


                            _nfse.LoteRps.ListaRps[i].InfRps.DataEmissao = DateTime.Now;

                            _nfse.LoteRps.ListaRps[i].InfRps.RegimeEspecialTributacao = item.First().SacadoCleinte._MapaFaturamento.CodigoFiscal.ToString();

                            _nfse.LoteRps.ListaRps[i].InfRps.IncentivadorCultural = Convert.ToByte(_Mapeamento.IncentivadorCultural);

                            _nfse.LoteRps.ListaRps[i].InfRps.NaturezaOperacao = Convert.ToByte(_Mapeamento.NaturezaOperacao);

                            _nfse.LoteRps.ListaRps[i].InfRps.OptanteSimplesNacional = Convert.ToByte(_Mapeamento.OptanteSimplesNacional);

                            _nfse.LoteRps.ListaRps[i].InfRps.Status = Convert.ToByte((int)item.First().SacadoCleinte._MapaFaturamento.Vencimento);

                            #endregion

                            #region SERVIÇO
                            _nfse.LoteRps.ListaRps[i].InfRps.Servico = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsServico();

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.CodigoMunicipio = Convert.ToUInt32(RetornaEmpresa(true).CodigoMunicipio);

                            DateTime Vencimento = new DateTime();
                            DateTime Prestacao = new DateTime();
                            int Diavenci = item.First().SacadoCleinte._MapaFaturamento.Vencimento;
                            new Utils.retornos().RetornaPrestacao(item.First().SacadoCleinte._MapaFaturamento.MesParcelas, ref Prestacao, ref Vencimento);

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.CodigoTributacaoMunicipio = Diavenci + "/" + Vencimento.Month + "/" + Vencimento.Year;

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.ItemListaServico = Convert.ToDecimal(14.01);

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.Discriminacao = GeraDiscriminacao(lstBoleto, item.First().Cedente.CNPJ, item.First().SacadoCleinte._SacadoInfo.Documento, "ICMS");


                            #region SERVIÇO VALORES
                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.Valores = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsServicoValores();

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.Valores.ValorServicos = Convert.ToDecimal(Math.Round(item.Sum(x => x.SacadoCleinte.ListaBoleto.Sum(c => c.ValorDocumento)), 2));

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.Valores.IssRetido = Convert.ToByte(2);

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.Valores.Aliquota = Convert.ToDecimal(0.05);


                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.Valores.ValorIss = Convert.ToByte(new Utils.Calculos().CalculoPorcentagem(0.05, (double)Convert.ToDecimal(item.Sum(x => x.SacadoCleinte.ListaBoleto.Sum(c => c.ValorDocumento)))));


                            #endregion


                            #endregion

                            #region INDENTIFICACAO
                            _nfse.LoteRps.ListaRps[i].InfRps.IdentificacaoRps = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsIdentificacaoRps();
                            _nfse.LoteRps.ListaRps[i].InfRps.IdentificacaoRps.Numero = Convert.ToInt32(_Mapeamento.NumeroIcms + 1).ToString();
                            _Mapeamento.NumeroIcms = Convert.ToInt32(_nfse.LoteRps.ListaRps[i].InfRps.IdentificacaoRps.Numero);
                            _nfse.LoteRps.ListaRps[i].InfRps.IdentificacaoRps.Serie = _Mapeamento.SerieIcms;

                            _nfse.LoteRps.ListaRps[i].InfRps.IdentificacaoRps.Tipo = Convert.ToByte(1);

                            #endregion

                            #region PRESTADOR
                            _nfse.LoteRps.ListaRps[i].InfRps.Prestador = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsPrestador();

                            _nfse.LoteRps.ListaRps[i].InfRps.Prestador.Cnpj = _Cedente.Cnpj;

                            _nfse.LoteRps.ListaRps[i].InfRps.Prestador.InscricaoMunicipal = _Cedente.InscMunicipal;

                            #endregion

                            #region TOMADOR

                            #region TOMADOR IDENTIFICACAO
                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsTomador();
                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.IdentificacaoTomador = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsTomadorIdentificacaoTomador();
                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.IdentificacaoTomador.CpfCnpj = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsTomadorIdentificacaoTomadorCpfCnpj();


                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cnpj = item.First().SacadoCleinte._SacadoInfo.Documento;


                            #endregion

                            #region TOMADOR ENDEREÇO

                            ClienteEndereco Endereco = new ClienteEndereco();
                            try
                            {
                                Endereco = item.First().SacadoCleinte._MapaFaturamento._Contrato._InstalacaoEndereco.FirstOrDefault(f => f.TipoLogradouroId == 2);

                            }
                            catch
                            {
                                int ContratNrcId = (int)item.First().SacadoCleinte._MapaFaturamento.ContratoNrcId;
                                var nrcContrato = db.ContratoNrc.First(x => x.ContratoNrcId == ContratNrcId);
                                Endereco = nrcContrato._Contrato._InstalacaoEndereco.FirstOrDefault(f => f.TipoLogradouroId == 2);
                            }

                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsTomadorEndereco();


                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Bairro = Endereco._Bairro.Nome;

                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Cep = Endereco.Cep;

                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.CodigoMunicipio = Convert.ToUInt32(Endereco._Bairro._Cidade.CodIbge);

                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Complemento = Endereco.Complemento;
                            try
                            {
                                _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Endereco = Endereco.Logradouro + "-N:" + Endereco.Numero + "- Sl:" + Endereco.Sala + "- Andar:" + Endereco.Andar + "- Cep:" + Endereco.Cep;

                            }
                            catch
                            {

                                _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Endereco = Endereco.Logradouro;

                            }

                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Numero = Convert.ToInt32(Endereco.Numero);

                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Uf = Endereco._Bairro._Cidade._Estado.Estadosigla;

                            #endregion


                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.RazaoSocial = _Cliente.RazaoSocial;
                            _Mapeamento.LoteIcms = (_Mapeamento.LoteIcms + 1);

                            #endregion
                            db.MapeamentoFiscal.Update(_Mapeamento);
                            db.SaveChanges();

                            i++;

                        }

                        #endregion


                    }
                    catch (Exception erro)
                    {

                    }
                    GuardaBauNF(_nfse, TiposEnum.TipoImposto.ICMS_FOR.ToString(), ListaLinhaCobranca);

                }

                #endregion

                #region ICMS



                #endregion
            }
            public void DetalhesToObjeto(List<Nota> lstBoleto, List<Utils.Remessa> ListaRemessa, string cenario, string plano ,List<LinhaCobranca> ListaLinhaCobranca)
            {

                NFCenarioCod = cenario;
                NFPlano = plano;
                NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvio _nfse = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvio();
                foreach (var _nota in ListaRemessa.GroupBy(x => new { x.Cedente.CNPJ ,x.SacadoCleinte._MapaFaturamento.Vencimento}))
                {

                    Empresa _Cedente = new Empresa();
                    string raz = _nota.First().Cedente.Cedente;
                    _Cedente = db.Empresa.First(x => x.RazaoSocial.Contains(raz));
                    MapeamentoFiscal _Mapeamento = new MapeamentoFiscal();
                    _Mapeamento = db.MapeamentoFiscal.First(x => x.EmpresaId == _Cedente.EmpresaId);

                    #region IDENTIFICACAO

                    _nfse = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvio();
                    _nfse.LoteRps = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRps();

                    #region LOTE
                    _nfse.LoteRps.Cnpj = _Cedente.Cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
                    _nfse.LoteRps.versao = 1.00M;
                    _nfse.LoteRps.Id = "lote";
                    _nfse.LoteRps.NumeroLote = _Mapeamento.Lote.ToString();
                    _nfse.LoteRps.InscricaoMunicipal = _Cedente.InscMunicipal;
                    _nfse.LoteRps.QuantidadeRps = Convert.ToInt32(_nota.GroupBy(x => new { x.SacadoCleinte._SacadoInfo.Documento, x.SacadoCleinte._MapaFaturamento.Vencimento, x.SacadoCleinte._MapaFaturamento.MesParcelas }).Count()).ToString();
                    #endregion

                    #endregion
                    try
                    {

                        #region RPS

                        _nfse.LoteRps.ListaRps = Enumerable.Range(0, (_nota.GroupBy(x => new { x.SacadoCleinte._SacadoInfo.Documento, x.SacadoCleinte._MapaFaturamento.Vencimento, x.SacadoCleinte._MapaFaturamento.MesParcelas }).Count())).Select(g => new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRps()).ToArray();

                        int i = 0;
                        foreach (var item in _nota.GroupBy(x => new { x.SacadoCleinte._SacadoInfo.Documento, x.SacadoCleinte._MapaFaturamento.Vencimento, x.SacadoCleinte._MapaFaturamento.MesParcelas }))
                        {


                            Cliente _Cliente = new Cliente();
                            string cn = item.First().SacadoCleinte._SacadoInfo.Documento;
                            _Cliente = db.cliente.First(x => x.CpfCnpj.Contains(cn));
                            #region INFRPS

                            _nfse.LoteRps.ListaRps[i].InfRps = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRps();
                            _nfse.LoteRps.ListaRps[i].InfRps.Id = "nfse" + (i + 1).ToString();


                            _nfse.LoteRps.ListaRps[i].InfRps.DataEmissao = DateTime.Now;

                            _nfse.LoteRps.ListaRps[i].InfRps.RegimeEspecialTributacao = Convert.ToByte(_Mapeamento.RegimeEspecialTributacao).ToString();

                            _nfse.LoteRps.ListaRps[i].InfRps.IncentivadorCultural = Convert.ToByte(_Mapeamento.IncentivadorCultural);

                            _nfse.LoteRps.ListaRps[i].InfRps.NaturezaOperacao = Convert.ToByte(_Mapeamento.NaturezaOperacao);

                            _nfse.LoteRps.ListaRps[i].InfRps.OptanteSimplesNacional = Convert.ToByte(_Mapeamento.OptanteSimplesNacional);

                            _nfse.LoteRps.ListaRps[i].InfRps.Status = Convert.ToByte(1);

                            #endregion

                            #region SERVIÇO
                            _nfse.LoteRps.ListaRps[i].InfRps.Servico = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsServico();

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.CodigoMunicipio = Convert.ToUInt32(RetornaEmpresa(true).CodigoMunicipio);

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.CodigoTributacaoMunicipio = "140100588";

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.ItemListaServico = Convert.ToDecimal(14.01);

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.Discriminacao = GeraDiscriminacaoGroup(lstBoleto, item.First().Cedente.CNPJ, item.First().SacadoCleinte._SacadoInfo.Documento, "ISS");


                            #region SERVIÇO VALORES
                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.Valores = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsServicoValores();

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.Valores.ValorServicos = Convert.ToDecimal(item.Sum(x => x.SacadoCleinte.ListaBoleto.Sum(c => c.ValorDocumento)));

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.Valores.IssRetido = Convert.ToByte(2);

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.Valores.Aliquota = Convert.ToDecimal(0.05);


                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.Valores.ValorIss = Convert.ToByte(new Utils.Calculos().CalculoPorcentagem(0.05, (double)Convert.ToDecimal(item.Sum(x => x.SacadoCleinte.ListaBoleto.Sum(c => c.ValorDocumento)))));


                            #endregion


                            #endregion

                            #region INDENTIFICACAO
                            _nfse.LoteRps.ListaRps[i].InfRps.IdentificacaoRps = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsIdentificacaoRps();
                            _nfse.LoteRps.ListaRps[i].InfRps.IdentificacaoRps.Numero = Convert.ToInt32(_Mapeamento.Numero + 1).ToString();
                            _Mapeamento.Numero = Convert.ToInt32(_nfse.LoteRps.ListaRps[i].InfRps.IdentificacaoRps.Numero);
                            _nfse.LoteRps.ListaRps[i].InfRps.IdentificacaoRps.Serie = _Mapeamento.Serie;

                            _nfse.LoteRps.ListaRps[i].InfRps.IdentificacaoRps.Tipo = Convert.ToByte(1);

                            #endregion

                            #region PRESTADOR
                            _nfse.LoteRps.ListaRps[i].InfRps.Prestador = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsPrestador();

                            _nfse.LoteRps.ListaRps[i].InfRps.Prestador.Cnpj = _Cedente.Cnpj;

                            _nfse.LoteRps.ListaRps[i].InfRps.Prestador.InscricaoMunicipal = _Cedente.InscMunicipal;

                            #endregion

                            #region TOMADOR

                            #region TOMADOR IDENTIFICACAO
                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsTomador();
                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.IdentificacaoTomador = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsTomadorIdentificacaoTomador();
                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.IdentificacaoTomador.CpfCnpj = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsTomadorIdentificacaoTomadorCpfCnpj();


                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cnpj = "99999999000191";


                            #endregion

                            #region TOMADOR ENDEREÇO
                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRpsInfRpsTomadorEndereco();


                            //_nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Bairro = _Cliente._ClienteEndereco.First(x=>x.ClienteEnderecoId == item.First().SacadoCleinte._MapaFaturamento.LgrFaturamrentoId)._Bairro.Nome;

                            //_nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Cep = _Cliente._ClienteEndereco.First(x => x.ClienteEnderecoId == item.First().SacadoCleinte._MapaFaturamento.LgrFaturamrentoId).Cep;

                            //_nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.CodigoMunicipio = Convert.ToUInt32(_Cliente._ClienteEndereco.First(x => x.ClienteEnderecoId == item.First().SacadoCleinte._MapaFaturamento.LgrFaturamrentoId)._Bairro._Cidade.CodIbge);

                            //_nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Complemento = _Cliente._ClienteEndereco.First(x => x.ClienteEnderecoId == item.First().SacadoCleinte._MapaFaturamento.LgrFaturamrentoId).Complemento;

                            //_nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Endereco = _Cliente._ClienteEndereco.First(x => x.ClienteEnderecoId == item.First().SacadoCleinte._MapaFaturamento.LgrFaturamrentoId).Logradouro;

                            //_nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Numero = Convert.ToByte(_Cliente._ClienteEndereco.First(x => x.ClienteEnderecoId == item.First().SacadoCleinte._MapaFaturamento.LgrFaturamrentoId).Numero);

                            //_nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Uf = _Cliente._ClienteEndereco.First(x => x.ClienteEnderecoId == item.First().SacadoCleinte._MapaFaturamento.LgrFaturamrentoId)._Bairro._Cidade._Estado.Estadosigla;

                            #endregion


                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.RazaoSocial = _Cliente.RazaoSocial;
                            _Mapeamento.Lote = (_Mapeamento.Lote + 1);

                            #endregion
                            db.MapeamentoFiscal.Add(_Mapeamento);
                            db.SaveChanges();

                            i++;

                        }

                        #endregion


                    }
                    catch (Exception erro)
                    {

                    }
                    GuardaBauNF(_nfse, TiposEnum.TipoImposto.ISS.ToString(),ListaLinhaCobranca);
                }



            }

            /// <summary>
            /// NOTA FISCAL
            /// </summary>

            public void GuardaNotaFiscal(NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRps _nfse, int LoteId, string _Cenario, int NumeroLote, string TipoImposto,List<LinhaCobranca> ListaCobranca)
            {
                string cliente = _nfse.InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cnpj;
                var Cliente = db.cliente.First(x => x.CpfCnpj == cliente);

                NotaFiscal _nf = new NotaFiscal();
                _nf.NotaFiscalLoteId = LoteId;
                _nf.Aliquota = _nfse.InfRps.Servico.Valores.Aliquota;
                _nf.CenarioId = 1;
                _nf.ClienteId = Cliente.ClienteId;
                _nf.CodigoTributacaoMunicipio = _nfse.InfRps.Servico.CodigoTributacaoMunicipio.ToString();
                _nf.DataAtual = DateTime.Now;
                try
                {
                    _nf.DataVencimento = Convert.ToDateTime(_nfse.InfRps.Servico.CodigoTributacaoMunicipio);

                }
                catch
                {

                }
                _nf.DataEmissao = _nfse.InfRps.DataEmissao;
                _nf.Discriminacao = _nfse.InfRps.Servico.Discriminacao;
                _nf.TipoImposto = TipoImposto;
                _nf.ItemListaServico = _nfse.InfRps.Servico.ItemListaServico;
                _nf.NumeroLote = NumeroLote;
                _nf.NumeroRps = Convert.ToInt32(_nfse.InfRps.IdentificacaoRps.Numero);
                _nf.ValorAliquota = _nfse.InfRps.Servico.Valores.Aliquota;
                _nf.ValorIss = _nfse.InfRps.Servico.Valores.ValorIss;
                _nf.ValorServicos = _nfse.InfRps.Servico.Valores.ValorServicos;
                _nf.LinhaCobrancaId = ListaCobranca.First(x => x.ClienteId == Cliente.ClienteId).LinhaCobrancaId;
                _nf.PDF = Encoding.ASCII.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(_nfse));
                db.NotaFiscal.Add(_nf);
                db.SaveChanges();

            }

            public void GuardaNotaFiscal(NFSE.Net.Layouts.Carioca.EnviarLoteRpsEnvioLoteRpsRps _nfse, int LoteId, string _Cenario, int NumeroLote, string TipoImposto, List<LinhaCobranca> ListaLinhaCobranca)
            {
                string cliente = _nfse.InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cnpj;
                var Cliente = db.cliente.First(x => x.CpfCnpj == cliente);
              
                NotaFiscal _nf = new NotaFiscal();
                _nf.NotaFiscalLoteId = LoteId;
                _nf.Aliquota = Convert.ToDecimal(_nfse.InfRps.Servico.Valores.Aliquota);
                _nf.CenarioId = 1;
                _nf.ClienteId = Cliente.ClienteId;
                _nf.CodigoTributacaoMunicipio = _nfse.InfRps.Servico.CodigoTributacaoMunicipio.ToString();
                _nf.DataAtual = DateTime.Now;
                _nf.TipoImposto = TipoImposto;
                _nf.DataEmissao = _nfse.InfRps.DataEmissao;
                _nf.Discriminacao = _nfse.InfRps.Servico.Discriminacao;
                _nf.ItemListaServico = Convert.ToDecimal(_nfse.InfRps.Servico.ItemListaServico);
                _nf.NumeroLote = NumeroLote;
                _nf.NumeroRps = Convert.ToInt32(_nfse.InfRps.IdentificacaoRps.Numero);
                //_nf.ValorAliquota = Convert.ToInt32(_nfse.InfRps.Servico.Valores.Aliquota);
                _nf.ValorIss = _nfse.InfRps.Servico.Valores.ValorIss;
                _nf.ValorServicos = _nfse.InfRps.Servico.Valores.ValorServicos;
                _nf.LinhaCobrancaId = ListaLinhaCobranca.First(x=>x.ClienteId == Cliente.ClienteId).LinhaCobrancaId;
                _nf.PDF = Encoding.ASCII.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(_nfse));
                db.NotaFiscal.Add(_nf);
                db.SaveChanges();

            }

            public void GuardaNotaFiscal(NFSE.Net.Layouts.Paulista.PedidoEnvioLoteRPSRPS _nfse, int LoteId, string _Cenario, int NumeroLote, string TipoImposto)
            {
                string cliente = _nfse.RazaoSocialTomador;
                NotaFiscal _nf = new NotaFiscal();
                _nf.NotaFiscalLoteId = LoteId;
                _nf.TipoImposto = TipoImposto;
                _nf.Aliquota = _nfse.AliquotaServicos;
                _nf.CenarioId = 1;
                _nf.ClienteId = db.cliente.First(x => x.RazaoSocial.Contains(cliente)).ClienteId;
                _nf.CodigoTributacaoMunicipio = _nfse.CodigoServico;
                _nf.DataAtual = DateTime.Now;
                _nf.DataEmissao = _nfse.DataEmissao;
                _nf.Discriminacao = _nfse.Discriminacao;
                _nf.ItemListaServico = Convert.ToDecimal(_nfse.CodigoServico);
                _nf.NumeroLote = NumeroLote;
                _nf.NumeroRps = Convert.ToInt32(_nfse.ChaveRPS.NumeroRPS);
                _nf.ValorAliquota = _nfse.AliquotaServicos; ;
                _nf.ValorServicos = Convert.ToDecimal(_nfse.ValorServicos);
                _nf.PDF = Encoding.ASCII.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(_nfse));
                db.NotaFiscal.Add(_nf);
                db.SaveChanges();

            }

            public NFSE.Net.Layouts.Carioca.EnviarLoteRpsEnvio NotaCarioca(List<Nota> lstBoleto, List<Utils.Remessa> ListaRemessa, string cenario, string plano, List<LinhaCobranca> ListaLinhaCobranca)
            {
                NFCenarioCod = cenario;
                NFPlano = plano;
                NFSE.Net.Layouts.Carioca.EnviarLoteRpsEnvio _nfse = new NFSE.Net.Layouts.Carioca.EnviarLoteRpsEnvio();
                foreach (var _nota in ListaRemessa.GroupBy(x => new { x.Cedente.CNPJ }))
                {
                    Empresa _Cedente = new Empresa();
                    string raz = _nota.First().Cedente.Cedente;
                    _Cedente = db.Empresa.First(x => x.RazaoSocial.Contains(raz));
                    MapeamentoFiscal _Mapeamento = new MapeamentoFiscal();
                    _Mapeamento = db.MapeamentoFiscal.First(x => x.EmpresaId == _Cedente.EmpresaId);

                    #region IDENTIFICACAO

                    _nfse = new NFSE.Net.Layouts.Carioca.EnviarLoteRpsEnvio();
                    _nfse.LoteRps = new NFSE.Net.Layouts.Carioca.EnviarLoteRpsEnvioLoteRps();
                    #region LOTE
                    _nfse.LoteRps.Cnpj = _Cedente.Cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
                    _nfse.LoteRps.Id = "lote";
                    _nfse.LoteRps.NumeroLote = _Mapeamento.Lote.ToString();
                    _nfse.LoteRps.InscricaoMunicipal = _Cedente.InscMunicipal.Replace(".", "").Replace("/", "").Replace("-", "");
                    _nfse.LoteRps.QuantidadeRps = Convert.ToInt32(_nota.GroupBy(x => new { x.SacadoCleinte._SacadoInfo.Documento }).Count()).ToString();
                    #endregion

                    #endregion

                    try
                    {

                        #region RPS
                        _nfse.LoteRps.ListaRps = Enumerable.Range(0, (_nota.GroupBy(x => new { x.SacadoCleinte._SacadoInfo.Documento }).Count())).Select(g => new NFSE.Net.Layouts.Carioca.EnviarLoteRpsEnvioLoteRpsRps()).ToArray();

                        int i = 0;
                        foreach (var item in _nota.GroupBy(x => new { x.SacadoCleinte._SacadoInfo.Documento }))
                        {

                            Cliente _Cliente = new Cliente();
                            string cn = item.First().SacadoCleinte._SacadoInfo.Documento;
                            _Cliente = db.cliente.First(x => x.CpfCnpj.Contains(cn));
                          
                            #region INFRPS
                            ProdutoMapeamento ProdutoMapa = new ProdutoMapeamento();
                            try
                            {
                                int iddf = (int)item.First().SacadoCleinte._MapaFaturamento.ContratoId;
                                var ContratoVinculado = db.Contrato.First(x => x.ContratoId == iddf);
                                int produtoId = ContratoVinculado.ProdutoId;
                                var map = db.ProdutoMapeamento.First(x => x.ProdutoId == produtoId && x.CidadeId == 211);
                                ProdutoMapa = map;
                            }
                            catch
                            {
                                int iddf = (int)item.First().SacadoCleinte._MapaFaturamento.ContratoNrcId;
                                var ContratoVinculado = db.ContratoNrc.First(x => x.ContratoNrcId == iddf);
                                int produtoId = ContratoVinculado.ProdutoId;
                                var map = db.ProdutoMapeamento.First(x => x.ProdutoId == produtoId && x.CidadeId == 211);
                                ProdutoMapa = map;

                            }

                            _nfse.LoteRps.ListaRps[i].InfRps = new NFSE.Net.Layouts.Carioca.EnviarLoteRpsEnvioLoteRpsRpsInfRps();
                            _nfse.LoteRps.ListaRps[i].InfRps.Id = "nfse" + (i + 1).ToString();


                            _nfse.LoteRps.ListaRps[i].InfRps.DataEmissao = DateTime.Now;


                            _nfse.LoteRps.ListaRps[i].InfRps.IncentivadorCultural = Convert.ToByte(_Mapeamento.IncentivadorCultural).ToString();

                            _nfse.LoteRps.ListaRps[i].InfRps.NaturezaOperacao = Convert.ToByte(_Mapeamento.NaturezaOperacao).ToString();

                            _nfse.LoteRps.ListaRps[i].InfRps.OptanteSimplesNacional = Convert.ToByte(_Mapeamento.OptanteSimplesNacional).ToString();

                            _nfse.LoteRps.ListaRps[i].InfRps.Status = Convert.ToByte(1).ToString();

                            #endregion

                            #region SERVIÇO
                            _nfse.LoteRps.ListaRps[i].InfRps.Servico = new NFSE.Net.Layouts.Carioca.EnviarLoteRpsEnvioLoteRpsRpsInfRpsServico();

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.CodigoMunicipio = Convert.ToUInt32(RetornaEmpresa(true).CodigoMunicipio);

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.CodigoTributacaoMunicipio = ProdutoMapa.ItemListaServico.Replace(".", "");

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.ItemListaServico = ProdutoMapa.ItemListaServico.Substring(0, 5).Replace(".", "");

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.Discriminacao = GeraDiscriminacaoGroup(lstBoleto, item.First().Cedente.CNPJ, item.First().SacadoCleinte._SacadoInfo.Documento, "ISS");


                            #region SERVIÇO VALORES
                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.Valores = new NFSE.Net.Layouts.Carioca.EnviarLoteRpsEnvioLoteRpsRpsInfRpsServicoValores();

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.Valores.ValorServicos = Math.Round(Convert.ToDecimal(item.Sum(x => x.SacadoCleinte.ListaBoleto.Sum(c => c.ValorDocumento))), 2);

                            #region RETENCAO
                            try
                            {
                                if (item.Sum(x => x.SacadoCleinte._Retencao.Count(s => s._PlanoEmpresa.Imposto == "ISS")) > 0)
                                {
                                    _nfse.LoteRps.ListaRps[i].InfRps.Servico.Valores.ValorPis = (decimal)new Utils.Calculos().CalculoPorcentagem((double)item.Sum(x => x.SacadoCleinte._Retencao.Where(s => s._PlanoEmpresa.Imposto == "ISS").Sum(y => y.R_PIS)), (double)Math.Round(Convert.ToDecimal(item.Sum(x => x.SacadoCleinte.ListaBoleto.Sum(c => c.ValorDocumento))), 2));
                                    _nfse.LoteRps.ListaRps[i].InfRps.Servico.Valores.ValorCofins = (decimal)new Utils.Calculos().CalculoPorcentagem((double)item.Sum(x => x.SacadoCleinte._Retencao.Where(s => s._PlanoEmpresa.Imposto == "ISS").Sum(y => y.R_Cofins)), (double)Math.Round(Convert.ToDecimal(item.Sum(x => x.SacadoCleinte.ListaBoleto.Sum(c => c.ValorDocumento))), 2));
                                    _nfse.LoteRps.ListaRps[i].InfRps.Servico.Valores.ValorInss = (decimal)new Utils.Calculos().CalculoPorcentagem((double)item.Sum(x => x.SacadoCleinte._Retencao.Where(s => s._PlanoEmpresa.Imposto == "ISS").Sum(y => y.R_INSS)), (double)Math.Round(Convert.ToDecimal(item.Sum(x => x.SacadoCleinte.ListaBoleto.Sum(c => c.ValorDocumento))), 2));
                                    _nfse.LoteRps.ListaRps[i].InfRps.Servico.Valores.ValorIr = (decimal)new Utils.Calculos().CalculoPorcentagem((double)item.Sum(x => x.SacadoCleinte._Retencao.Where(s => s._PlanoEmpresa.Imposto == "ISS").Sum(y => y.R_IRPJ)), (double)Math.Round(Convert.ToDecimal(item.Sum(x => x.SacadoCleinte.ListaBoleto.Sum(c => c.ValorDocumento))), 2));
                                    _nfse.LoteRps.ListaRps[i].InfRps.Servico.Valores.ValorCsll = (decimal)new Utils.Calculos().CalculoPorcentagem((double)item.Sum(x => x.SacadoCleinte._Retencao.Where(s => s._PlanoEmpresa.Imposto == "ISS").Sum(y => y.R_CSLL)), (double)Math.Round(Convert.ToDecimal(item.Sum(x => x.SacadoCleinte.ListaBoleto.Sum(c => c.ValorDocumento))), 2));
                                }
                            }
                            catch
                            {

                             
                            }
                          
                            #endregion

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.Valores.IssRetido = "2";

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.Valores.Aliquota = ProdutoMapa.Aliquota == "5" ? "0.05" : ProdutoMapa.Aliquota;

                            _nfse.LoteRps.ListaRps[i].InfRps.Servico.Valores.ValorIss = Math.Round(Convert.ToDecimal(new Utils.Calculos().CalculoPorcentagem(Convert.ToDouble(ProdutoMapa.Aliquota), (double)Convert.ToDecimal(item.Sum(x => x.SacadoCleinte.ListaBoleto.Sum(c => c.ValorDocumento))))),2);



                            #endregion


                            #endregion

                            #region INDENTIFICACAO
                            _nfse.LoteRps.ListaRps[i].InfRps.IdentificacaoRps = new NFSE.Net.Layouts.Carioca.EnviarLoteRpsEnvioLoteRpsRpsInfRpsIdentificacaoRps();
                            _nfse.LoteRps.ListaRps[i].InfRps.IdentificacaoRps.Numero = Convert.ToInt32(_Mapeamento.Numero + 1).ToString();
                            _Mapeamento.Numero = Convert.ToInt32(_nfse.LoteRps.ListaRps[i].InfRps.IdentificacaoRps.Numero);
                            _nfse.LoteRps.ListaRps[i].InfRps.IdentificacaoRps.Serie = _Mapeamento.Serie;

                            _nfse.LoteRps.ListaRps[i].InfRps.IdentificacaoRps.Tipo = Convert.ToByte(1).ToString();

                            #endregion

                            #region PRESTADOR
                            _nfse.LoteRps.ListaRps[i].InfRps.Prestador = new NFSE.Net.Layouts.Carioca.EnviarLoteRpsEnvioLoteRpsRpsInfRpsPrestador();

                            _nfse.LoteRps.ListaRps[i].InfRps.Prestador.Cnpj = _Cedente.Cnpj;

                            _nfse.LoteRps.ListaRps[i].InfRps.Prestador.InscricaoMunicipal = _Cedente.InscMunicipal.Replace(".", "").Replace("/", "").Replace("-", "");

                            #endregion

                            #region TOMADOR

                            #region TOMADOR IDENTIFICACAO
                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador = new NFSE.Net.Layouts.Carioca.EnviarLoteRpsEnvioLoteRpsRpsInfRpsTomador();
                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.IdentificacaoTomador = new NFSE.Net.Layouts.Carioca.EnviarLoteRpsEnvioLoteRpsRpsInfRpsTomadorIdentificacaoTomador();
                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.IdentificacaoTomador.CpfCnpj = new NFSE.Net.Layouts.Carioca.EnviarLoteRpsEnvioLoteRpsRpsInfRpsTomadorIdentificacaoTomadorCpfCnpj();


                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cnpj = item.First().SacadoCleinte._SacadoInfo.Documento;

                            #endregion

                            #region TOMADOR ENDEREÇO

                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco = new NFSE.Net.Layouts.Carioca.EnviarLoteRpsEnvioLoteRpsRpsInfRpsTomadorEndereco();

                            string _Cnnpj = item.First().SacadoCleinte._SacadoInfo.Documento;
                            var Cliente = db.cliente.First(x => x.CpfCnpj == _Cnnpj);

                            ClienteEndereco Endereco = new ClienteEndereco();
                            try
                            {
                                Endereco = item.First().SacadoCleinte._MapaFaturamento._Contrato._InstalacaoEndereco.FirstOrDefault(f => f.TipoLogradouroId == 2);

                            }
                            catch
                            {
                                int ContratNrcId = (int)item.First().SacadoCleinte._MapaFaturamento.ContratoNrcId;
                                var nrcContrato = db.ContratoNrc.First(x => x.ContratoNrcId == ContratNrcId);
                                Endereco = nrcContrato._Contrato._InstalacaoEndereco.FirstOrDefault(f => f.TipoLogradouroId == 2);
                            }

                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Bairro = Endereco._Bairro.Nome;

                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Cep = Endereco.Cep;

                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.CodigoMunicipio = Endereco._Bairro._Cidade.CodIbge.ToString();

                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Complemento = Endereco.Complemento == null ? "N/A" : Endereco.Complemento;

                            try
                            {
                                _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Endereco = Endereco.Logradouro + "-N:" + Endereco.Numero + "- Sl:" + Endereco.Sala + "- Andar:" + Endereco.Andar + "- Cep:" + Endereco.Cep;

                            }
                            catch
                            {

                                _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Endereco = Endereco.Logradouro;

                            }
                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Numero = Endereco.Numero.ToString();

                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.Endereco.Uf = Endereco._Bairro._Cidade._Estado.Estadosigla;

                            #endregion


                            _nfse.LoteRps.ListaRps[i].InfRps.Tomador.RazaoSocial = _Cliente.RazaoSocial;
                            _Mapeamento.Lote = (_Mapeamento.Lote + 1);

                            #endregion
                            db.MapeamentoFiscal.Update(_Mapeamento);
                            db.SaveChanges();
                            i++;

                        }

                        #endregion


                    }
                    catch (Exception erro)
                    {

                    }
                    GuardaBauNFCarioca(_nfse, TiposEnum.TipoImposto.ISS.ToString(), ListaLinhaCobranca);
                }



                return _nfse;
            }

            public NFSE.Net.Layouts.Paulista.PedidoEnvioLoteRPS NotaPaulista(List<Nota> lstBoleto, List<Utils.Remessa> ListaRemessa, string cenario, string plano)
            {


                NFCenarioCod = cenario;
                NFPlano = plano;

                NFSE.Net.Layouts.Paulista.PedidoEnvioLoteRPS _nfse = new NFSE.Net.Layouts.Paulista.PedidoEnvioLoteRPS();
                foreach (var _nota in ListaRemessa.GroupBy(x => new { x.Cedente.CNPJ }))
                {
                    Empresa _Cedente = new Empresa();
                    string raz = _nota.First().Cedente.Cedente;
                    _Cedente = db.Empresa.First(x => x.RazaoSocial.Contains(raz));
                    MapeamentoFiscal _Mapeamento = new MapeamentoFiscal();
                    _Mapeamento = db.MapeamentoFiscal.First(x => x.EmpresaId == _Cedente.EmpresaId);

                    _nfse.Cabecalho = new NFSE.Net.Layouts.Paulista.PedidoEnvioLoteRPSCabecalho();

                    #region CABEÇALHO

                    _nfse.Cabecalho.QtdRPS = _nota.GroupBy(x => new { x.SacadoCleinte._SacadoInfo.Documento, x.SacadoCleinte._MapaFaturamento.Vencimento, x.SacadoCleinte._MapaFaturamento.MesParcelas }).Count().ToString();
                    _nfse.Cabecalho.transacao = false;
                    _nfse.Cabecalho.ValorTotalDeducoes = "0.00";
                    _nfse.Cabecalho.ValorTotalServicos = _nota.GroupBy(x => new { x.SacadoCleinte._SacadoInfo.Documento, x.SacadoCleinte._MapaFaturamento.Vencimento, x.SacadoCleinte._MapaFaturamento.MesParcelas }).Sum(b => b.Sum(n => n.SacadoCleinte.ListaBoleto.Sum(f => f.ValorDocumento))).ToString();
                    _nfse.Cabecalho.Versao = "1";
                    _nfse.Cabecalho.CPFCNPJRemetente = new NFSE.Net.Layouts.Paulista.PedidoEnvioLoteRPSCabecalhoCPFCNPJRemetente();
                    _nfse.Cabecalho.CPFCNPJRemetente.CNPJ = _Cedente.Cnpj;
                    _nfse.Cabecalho.dtFim = DateTime.Now;
                    _nfse.Cabecalho.dtInicio = DateTime.Now;

                    #endregion


                    #region  <RPS xmlns="">
                    int cont = 0;
                    _nfse.RPS = Enumerable.Range(0, (_nota.GroupBy(x => new { x.SacadoCleinte._SacadoInfo.Documento, x.SacadoCleinte._MapaFaturamento.Vencimento, x.SacadoCleinte._MapaFaturamento.MesParcelas }).Count())).Select(g => new NFSE.Net.Layouts.Paulista.PedidoEnvioLoteRPSRPS()).ToArray();

                    foreach (var item in _nota.GroupBy(x => new { x.SacadoCleinte._SacadoInfo.Documento, x.SacadoCleinte._MapaFaturamento.Vencimento, x.SacadoCleinte._MapaFaturamento.MesParcelas }))
                    {


                        Cliente _Cliente = new Cliente();
                        string cn = item.First().SacadoCleinte._SacadoInfo.Documento;
                        _Cliente = db.cliente.First(x => x.CpfCnpj.Contains(cn));
                        #region <ChaveRPS>

                        _nfse.RPS[cont].ChaveRPS = new NFSE.Net.Layouts.Paulista.PedidoEnvioLoteRPSRPSChaveRPS();
                        _nfse.RPS[cont].ChaveRPS.InscricaoPrestador = _Cedente.InscMunicipal;
                        _nfse.RPS[cont].ChaveRPS.SerieRPS = _Mapeamento.Serie;
                        _nfse.RPS[cont].ChaveRPS.NumeroRPS = Convert.ToInt32(_Mapeamento.Numero + 1).ToString();

                        #endregion

                        _nfse.RPS[cont].TipoRPS = "RPS";
                        _nfse.RPS[cont].DataEmissao = DateTime.Now;
                        _nfse.RPS[cont].StatusRPS = "N";
                        _nfse.RPS[cont].TributacaoRPS = "T";
                        _nfse.RPS[cont].ValorServicos = Convert.ToDecimal(item.Sum(x => x.SacadoCleinte.ListaBoleto.Sum(c => c.ValorDocumento))).ToString();
                        _nfse.RPS[cont].ValorDeducoes = "0.00";
                        _nfse.RPS[cont].CodigoServico = "02917";
                        _nfse.RPS[cont].AliquotaServicos = 0.05M;
                        _nfse.RPS[cont].ISSRetido = false;

                        _nfse.RPS[cont].CPFCNPJTomador = new NFSE.Net.Layouts.Paulista.PedidoEnvioLoteRPSRPSCPFCNPJTomador();
                        _nfse.RPS[cont].CPFCNPJTomador.CNPJSpecified = true;
                        _nfse.RPS[cont].CPFCNPJTomador.CNPJ = _Cliente.CpfCnpj.Replace(".", "").Replace("-", "").Replace("/", "");

                        _nfse.RPS[cont].RazaoSocialTomador = _Cliente.RazaoSocial;


                        _nfse.RPS[cont].EnderecoTomador = new NFSE.Net.Layouts.Paulista.PedidoEnvioLoteRPSRPSEnderecoTomador();
                        _nfse.RPS[cont].EnderecoTomador.TipoLogradouro = "R";
                        //_nfse.RPS[cont].EnderecoTomador.Logradouro = _Cliente._ClienteEndereco.First(x => x.ClienteEnderecoId == item.First().SacadoCleinte._MapaFaturamento.LgrFaturamrentoId).Logradouro;
                        //_nfse.RPS[cont].EnderecoTomador.NumeroEndereco = _Cliente._ClienteEndereco.First(x => x.ClienteEnderecoId == item.First().SacadoCleinte._MapaFaturamento.LgrFaturamrentoId).Numero.ToString();
                        //_nfse.RPS[cont].EnderecoTomador.ComplementoEndereco = _Cliente._ClienteEndereco.First(x => x.ClienteEnderecoId == item.First().SacadoCleinte._MapaFaturamento.LgrFaturamrentoId).Complemento;
                        //_nfse.RPS[cont].EnderecoTomador.Bairro = _Cliente._ClienteEndereco.First(x => x.ClienteEnderecoId == item.First().SacadoCleinte._MapaFaturamento.LgrFaturamrentoId)._Bairro.Nome;
                        //_nfse.RPS[cont].EnderecoTomador.Cidade = _Cliente._ClienteEndereco.First(x => x.ClienteEnderecoId == item.First().SacadoCleinte._MapaFaturamento.LgrFaturamrentoId)._Bairro._Cidade.CodIbge.ToString();
                        //_nfse.RPS[cont].EnderecoTomador.UF = _Cliente._ClienteEndereco.First(x => x.ClienteEnderecoId == item.First().SacadoCleinte._MapaFaturamento.LgrFaturamrentoId)._Bairro._Cidade._Estado.Estadosigla;
                        //_nfse.RPS[cont].EnderecoTomador.CEP = _Cliente._ClienteEndereco.First(x => x.ClienteEnderecoId == item.First().SacadoCleinte._MapaFaturamento.LgrFaturamrentoId).Cep.ToString();


                        _nfse.RPS[cont].Discriminacao = "Desenvolvimento de Skate atomico";
                        _nfse.RPS[cont].EmailTomador = "henriqueteste@teste.com";
                        _nfse.RPS[cont].Assinatura = RetornaAssinatura(_nfse.RPS[cont]);
                        cont++;
                    }
                    #endregion


                    GuardaBauNFPaulista(_nfse, _Mapeamento.Lote, TiposEnum.TipoImposto.ISS.ToString());
                }
                return _nfse;


            }

            public string RetornaAssinatura(NFSE.Net.Layouts.Paulista.PedidoEnvioLoteRPSRPS _rps)
            {

                string retorno = string.Empty;
                retorno = retorno + _rps.ChaveRPS.InscricaoPrestador.ToString().PadLeft(8, '0');
                retorno = retorno + _rps.ChaveRPS.SerieRPS.ToString().PadRight(5, ' ');
                retorno = retorno + _rps.ChaveRPS.NumeroRPS.ToString().PadLeft(12, '0');
                retorno = retorno + _rps.DataEmissao.ToString("yyyyMMdd");
                retorno = retorno + _rps.TributacaoRPS;
                retorno = retorno + _rps.StatusRPS;
                string IssRetido = _rps.ISSRetido == true ? "S" : "N";
                retorno = retorno + IssRetido;
                retorno = retorno + _rps.ValorServicos.ToString().Replace(".", "").Replace(",", "").PadLeft(15, '0');
                retorno = retorno + _rps.ValorDeducoes.ToString().Replace(".", "").Replace(",", "").PadLeft(15, '0');
                retorno = retorno + _rps.CodigoServico.ToString().PadLeft(5, '0');
                retorno = retorno + "2";
                retorno = retorno + _rps.CPFCNPJTomador.CNPJ.ToString();



                return retorno;
            }

            private void GuardaBauNFCarioca(NFSE.Net.Layouts.Carioca.EnviarLoteRpsEnvio envio, string TipoImposto,List<LinhaCobranca> ListaLinhaCobranca)
            {
             
                NotaFiscalLote _lote = new NotaFiscalLote();
                _lote.Cenario = "MVC" + DateTime.Now.Month.ToString();
                _lote.CorpoDocumento = Encoding.ASCII.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(envio));
                _lote.DataCriacao = DateTime.Now;
           
                try
                {
                    string Cno = @String.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToInt64(envio.LoteRps.Cnpj));
                    _lote.EmpresaId = db.Empresa.First(x => x.Cnpj == Cno).EmpresaId;
                }
                catch
                {
                    _lote.EmpresaId = db.Empresa.First(x => x.Cnpj == envio.LoteRps.Cnpj).EmpresaId;

                }

                _lote.NumeroLote = Convert.ToInt32(envio.LoteRps.NumeroLote);
                _lote.Plano = NFPlano;
                _lote.TipoImposto = TipoImposto;
                _lote.ValorNota = envio.LoteRps.ListaRps.Sum(x => x.InfRps.Servico.Valores.ValorServicos);
                db.NotaFiscalLote.Add(_lote);
                db.SaveChanges();
                foreach (var item in envio.LoteRps.ListaRps)
                {
                    GuardaNotaFiscal(item, _lote.NotaFiscalLoteId, NFCenarioCod, _lote.NumeroLote, TipoImposto, ListaLinhaCobranca);
                }
                
                Infra.Entidades.Bau bau = new Infra.Entidades.Bau();
                bau.Cenario = NFCenarioCod;
                bau.CNPJ = "CNPJ: " + @String.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToInt64(envio.LoteRps.Cnpj));
                try
                {
                    string Cno = @String.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToInt64(envio.LoteRps.Cnpj));
                    bau.Nome = db.Empresa.First(x => x.Cnpj == Cno).RazaoSocial;
                }
                catch
                {
                    bau.Nome = db.Empresa.First(x => x.Cnpj == envio.LoteRps.Cnpj).RazaoSocial;

                }

                bau.DataSolicitada = DateTime.Now;
                bau.TipoBau = "NF";
                bau.TipoArquivo = envio.LoteRps.ListaRps.Count() > 1 ? TiposEnum.TipoArquivoBau.Agrupado.ToString() : TiposEnum.TipoArquivoBau.Avulso.ToString();
                bau.Remessa = "";
                bau.CodigoBr = "";
                bau.Valor = envio.LoteRps.ListaRps.Sum(x => x.InfRps.Servico.Valores.ValorServicos);
                bau.SetorSolicitante = "TESTE";
                bau.Detalhes = envio.LoteRps.ListaRps.Count().ToString();
                bau.Usuario = "Teste";
                bau.Status = "Lote: " + envio.LoteRps.NumeroLote.ToString() + " Não Processado";
                bau.Plano = envio.LoteRps.ListaRps.Count() > 1 ? "Planos Agrupados" : NFPlano;
                bau.ObsDocumento = _lote.NotaFiscalLoteId.ToString();
                bau.Lote = envio.LoteRps.NumeroLote.ToString();
                db.Bau.Add(bau);
                db.SaveChanges();

            }

            private void GuardaBauNFPaulista(NFSE.Net.Layouts.Paulista.PedidoEnvioLoteRPS envio, int Lote, string TipoImposto)
            {

                NotaFiscalLote _lote = new NotaFiscalLote();
                _lote.Cenario = db.Cenario.First().CenarioCodigo;
                _lote.CorpoDocumento = Encoding.ASCII.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(envio));
                _lote.DataCriacao = DateTime.Now;
                _lote.EmpresaId = db.Empresa.First(x => x.Cnpj == envio.Cabecalho.CPFCNPJRemetente.CNPJ).EmpresaId;
                _lote.NumeroLote = Lote;
                _lote.TipoImposto = TipoImposto;
                _lote.Plano = NFPlano;
                _lote.ValorNota = Convert.ToDecimal(envio.Cabecalho.ValorTotalServicos);
                db.NotaFiscalLote.Add(_lote);
                db.SaveChanges();
                foreach (var item in envio.RPS)
                {

                    GuardaNotaFiscal(item, _lote.NotaFiscalLoteId, NFCenarioCod, _lote.NumeroLote, TipoImposto);

                }
                Infra.Entidades.Bau bau = new Infra.Entidades.Bau();
                bau.Cenario = NFCenarioCod;
                bau.CNPJ = "CNPJ: " + @String.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToInt64(envio.Cabecalho.CPFCNPJRemetente.CNPJ));
                bau.Nome = db.Empresa.First(x => x.Cnpj == envio.Cabecalho.CPFCNPJRemetente.CNPJ).RazaoSocial;
                bau.DataSolicitada = DateTime.Now;
                bau.TipoBau = "NF";
                bau.TipoArquivo = envio.RPS.Count() > 1 ? TiposEnum.TipoArquivoBau.Agrupado.ToString() : TiposEnum.TipoArquivoBau.Avulso.ToString();
                bau.Remessa = "";
                bau.CodigoBr = "";
                bau.Valor = Convert.ToDecimal(envio.Cabecalho.ValorTotalServicos);
                bau.SetorSolicitante = "TESTE";
                bau.Detalhes = envio.RPS.Count().ToString();
                bau.Usuario = "Teste";
                bau.Status = "Lote: " + Lote.ToString() + " Não Processado";
                bau.Plano = envio.RPS.Count() > 1 ? "Planos Agrupados" : NFPlano;
                bau.ObsDocumento = _lote.NotaFiscalLoteId.ToString();
                bau.Lote = Lote.ToString();
                db.Bau.Add(bau);
                db.SaveChanges();

            }

            private void GuardaBauNF(NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvio envio, string TipoImposto, List<LinhaCobranca> ListaLinhaCobranca)
            {
                string Observacao = (string)HttpContext.Current.Session["_Obs"];
                string Mes = (string)HttpContext.Current.Session["_Mes"];
                string Ano = (string)HttpContext.Current.Session["_Ano"];

                NotaFiscalLote _lote = new NotaFiscalLote();
                _lote.Cenario = "MVC" + DateTime.Now.ToString("MMyy");
                _lote.CorpoDocumento = Encoding.ASCII.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(envio));
                _lote.DataCriacao = DateTime.Now;
                try
                {
                    string cn = @String.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToInt64(envio.LoteRps.Cnpj));
                    _lote.EmpresaId = db.Empresa.First(x => x.Cnpj == cn).EmpresaId;
                }
                catch
                {

                    _lote.EmpresaId = db.Empresa.First(x => x.Cnpj == envio.LoteRps.Cnpj).EmpresaId;

                }

                _lote.NumeroLote = Convert.ToInt32(envio.LoteRps.NumeroLote);
                _lote.Plano = NFPlano;
                _lote.TipoImposto = TipoImposto;
                _lote.ValorNota = envio.LoteRps.ListaRps.Sum(x => x.InfRps.Servico.Valores.ValorServicos);
                db.NotaFiscalLote.Add(_lote);
                db.SaveChanges();

                foreach (var item in envio.LoteRps.ListaRps)
                {
                    GuardaNotaFiscal(item, _lote.NotaFiscalLoteId, NFCenarioCod, _lote.NumeroLote, TipoImposto, ListaLinhaCobranca);
                }

          
                if (TipoImposto == TiposEnum.TipoImposto.ICMS.ToString())
                {
                    List<Icms.Mestre> ListMestre = new List<Icms.Mestre>();
                    List<Icms.Item> ListItem = new List<Icms.Item>();
                    List<Icms.Dados> ListDados = new List<Icms.Dados>();

                    new ICMS().GeraICMS(envio, ref ListMestre, ref ListItem, ref ListDados);


                    Infra.Entidades.Bau bau = new Infra.Entidades.Bau();
                    bau.Cenario = NFCenarioCod;

                    bau.CNPJ = "CNPJ: " + @String.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToInt64(envio.LoteRps.Cnpj));

                    try
                    {
                        string cnpj = envio.LoteRps.Cnpj;
                        bau.Nome = db.Empresa.First(x => x.Cnpj.Contains(cnpj)).RazaoSocial;
                    }
                    catch
                    {
                        string CCnpj = @String.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToInt64(envio.LoteRps.Cnpj));
                        bau.Nome = db.Empresa.First(x => x.Cnpj.Contains(CCnpj)).RazaoSocial;
                    }

                    bau.DataSolicitada = DateTime.Now;
                    bau.TipoBau = "IC";
                    bau.TipoArquivo = envio.LoteRps.ListaRps.Count() > 1 ? TiposEnum.TipoArquivoBau.Agrupado.ToString() : TiposEnum.TipoArquivoBau.Avulso.ToString();
                    bau.Remessa = envio.LoteRps.ListaRps.First().InfRps.IdentificacaoRps.Serie;
                    bau.CodigoBr = "";
                    bau.Valor = envio.LoteRps.ListaRps.Sum(x => x.InfRps.Servico.Valores.ValorServicos);
                    bau.SetorSolicitante = "TESTE";
                    bau.Detalhes = (envio.LoteRps.NumeroLote + envio.LoteRps.Cnpj);
                    bau.Usuario = "Teste";
                    bau.Status = "Nota ICMS Lote: " + envio.LoteRps.NumeroLote;
                    bau.Plano = NFPlano;
                    bau.ObsDocumento = "N/A";
                    bau.Lote = "";
                    db.Bau.Add(bau);
                    db.SaveChanges();
                }
              

            }

            public void ConsultaNota(NFSE.Net.Layouts.Carioca.ConsultarLoteRpsEnvio envio)
            {
                var serializarw = new NFSE.Net.Layouts.Serializador();
                serializarw.SalvarXml<NFSE.Net.Layouts.Carioca.ConsultarLoteRpsEnvio>(envio, Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data/" + envio.Prestador.Cnpj + "/nfse/envio"), "009900999-ped-loterps.xml"));
            }

            public void ConsultaNota(NFSE.Net.Layouts.Betha.ConsultarLoteRpsEnvio envio)
            {
                var serializarw = new NFSE.Net.Layouts.Serializador();
                serializarw.SalvarXml<NFSE.Net.Layouts.Betha.ConsultarLoteRpsEnvio>(envio, Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data/" + envio.Prestador.Cnpj + "/nfse/envio"), "009900999-ped-loterps.xml"));
            }

            public void processaNF(NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvio envio, string NFFCenario, string NFFPlano, int Lote)
            {

                try
                {
                    System.Net.ServicePointManager.Expect100Continue = false;

                    NFSE.Net.Core.Empresa empresa = RetornaEmpresa(false);
                    var localSalvarArquivo = NFSE.Net.Core.ArquivosEnvio.GerarCaminhos(envio.LoteRps.Id, System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, HttpContext.Current.Server.MapPath("~/App_Data/" + envio.LoteRps.Cnpj + "/nfse/envio")));
                    var envioCompleto = new NFSE.Net.Envio.EnvioCompleto();
                    envioCompleto.SalvarLoteRps(envio, localSalvarArquivo);

                }
                catch (Exception erro)
                {

                    throw new System.ArgumentException("Parameter cannot be null", erro);
                }



            }

            public void processaNFCarioca(NFSE.Net.Layouts.Carioca.EnviarLoteRpsEnvio envio, string NFFCenario, string NFFPlano, int Lote)
            {

                try
                {
                    System.Net.ServicePointManager.Expect100Continue = false;

                    NFSE.Net.Core.Empresa empresa = RetornaEmpresa(false);
                    var localSalvarArquivo = NFSE.Net.Core.ArquivosEnvio.GerarCaminhos(envio.LoteRps.Id, System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, HttpContext.Current.Server.MapPath("~/App_Data/" + envio.LoteRps.Cnpj + "/nfse/envio")));
                    var envioCompleto = new NFSE.Net.Envio.EnvioCompleto();
                    envioCompleto.SalvarLoteRpsCarioca(envio, localSalvarArquivo);

                }
                catch (Exception erro)
                {

                    throw new System.ArgumentException("Parameter cannot be null", erro);
                }



            }

            public void processaNFPaulista(NFSE.Net.Layouts.Paulista.PedidoEnvioLoteRPS envio, string NFFCenario, string NFFPlano, int Lote)
            {

                try
                {
                    System.Net.ServicePointManager.Expect100Continue = false;

                    NFSE.Net.Core.Empresa empresa = RetornaEmpresa(false);
                    var localSalvarArquivo = NFSE.Net.Core.ArquivosEnvio.GerarCaminhos(Lote.ToString(), System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, HttpContext.Current.Server.MapPath("~/App_Data/" + envio.Cabecalho.CPFCNPJRemetente.CNPJ + "/nfse/envio")));
                    var envioCompleto = new NFSE.Net.Envio.EnvioCompleto();
                    envioCompleto.SalvarLoteRpsPaulista(envio, localSalvarArquivo);

                }
                catch (Exception erro)
                {

                    throw new System.ArgumentException("Parameter cannot be null", erro);
                }



            }
        }

    }
}
