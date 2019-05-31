using Infra;
using Infra.Entidades;
using Microsoft.Reporting.WinForms;
using NFSE.Net.Layouts.BHISS;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace MVX.Print
{
    public partial class DebitoFrm : Form
    {
        public DebitoFrm()
        {
            InitializeComponent();
        }

        private void DebitoFrm_Load(object sender, EventArgs e)
        {

        }

        public string retornaDescricao(string Valor)
        {
            string retorno = string.Empty;
            string[] Cortado = Valor.Split('|');
            foreach (var item in Cortado)
            {
                retorno = retorno+ Environment.NewLine + item;
            }
            return retorno;
        }
        public void GeraDebito(ConsultarLoteRpsRespostaCompNfseNfseInfNfse InfRps, Empresa empresa, EmpresaEndereco EnderecoPrest, string Observacao, string Prestacao)
        {
            string _Discriminacao = string.Empty;
            string[] Prod = InfRps.Servico.Discriminacao.Split(';');
            string _produtos = string.Empty;
            string _valorProdutos = string.Empty;
            try
            {
                foreach (var vv in Prod)
                {
                    string[] jj = vv.Split('|');
                    decimal valorr = Convert.ToDecimal(jj[1]);
                    _valorProdutos = _valorProdutos + String.Format("{0:C}", valorr) + Environment.NewLine;
                    _produtos = _produtos + jj[0];
                    if (jj[0].Contains("MundiSpecial") || jj[0].Contains("MundiAccess"))
                    {
                        jj[0] = jj[0] + " - Acesso à internet ";
                    }
                    _Discriminacao = _Discriminacao + jj[0] + " " + String.Format("{0:C}", valorr) + Environment.NewLine;


                }

            }
            catch
            {


            }
            var db = new Class1(true);
            reportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/App_Data/Report/ISS-DEBITO.rdlc");
            reportViewer1.LocalReport.EnableExternalImages = true;
            int ClienteCep = Convert.ToInt32(InfRps.TomadorServico.Endereco.CodigoMunicipio);
            var Cidade = db.Cidades.First(x => x.CodIbge == ClienteCep);
            string tituloPrefeitura = string.Empty;
            string VV = String.Format("{0:C}", InfRps.Servico.Valores.ValorServicos);
            string _cep = InfRps.TomadorServico.Endereco.Cep.ToString();
            var _Endereco = db.ClienteEndereco.First(x => x.Cep == _cep);
            string _CCnpj = InfRps.TomadorServico.IdentificacaoTomador.CpfCnpj.Cnpj;
            var Cliente = db.cliente.First(x => x.CpfCnpj == _CCnpj);
            string NumeroDaNota = InfRps.IdentificacaoRps.Numero.ToString().PadLeft(9, '0');
            string DataHoraEmissao = InfRps.DataEmissao.ToString();
            string PrestCnpj = empresa.Cnpj;
            string PrestIncMunicipal = InfRps.PrestadorServico.IdentificacaoPrestador.InscricaoMunicipal;
            string PrestRazao = empresa.RazaoSocial;
            string PrestEnd = EnderecoPrest.Logradouro + " - " + EnderecoPrest.Cep + " - " + EnderecoPrest.Complemento;
            string PrestTel = empresa.Telefone;
            string PrestMunicipio = EnderecoPrest.Municipio.ToString();
            string PrestUf = EnderecoPrest._Bairro._Cidade._Estado.Estadosigla;
            string PrestEmail = empresa.Email;
            string TomadorCnpj = Convert.ToUInt64(InfRps.TomadorServico.IdentificacaoTomador.CpfCnpj.Cnpj).ToString(@"00\.000\.000\/0000\-00");
            string TomadorRazao = Cliente.RazaoSocial;
            string TomadorMunicipio = Cidade.Nome;
            string TomadorUf = InfRps.TomadorServico.Endereco.Uf;
            string TomadorEndereco = InfRps.TomadorServico.Endereco.Endereco;
            string TomadroEmail = Cliente.Email;
            string TomaIE = Cliente.InscEstadual == null ? "Isento" : Cliente.InscEstadual;
            string TomaIM = Cliente.InscMunicipal == null ? "Isento" : Cliente.InscMunicipal;
            string Discriminacao = _Discriminacao;
            string ServicoPrestado = InfRps.Servico.ItemListaServico.ToString();
            string PrestIE = empresa.InscEstadual == null ? "Isento" : empresa.InscEstadual;
            string ValorNota = VV;
            string TomaTel = Cliente.Telefone;
            string Rodape = Prestacao;


            reportViewer1.LocalReport.SetParameters(new ReportParameter[]
            {
                AdicionaParametro("NumeroDaNota",NumeroDaNota),
                AdicionaParametro("DataHoraEmissao",DataHoraEmissao),
                AdicionaParametro("PrestCnpj", PrestCnpj),
                AdicionaParametro("PrestIncMunicipal",PrestIncMunicipal),
                AdicionaParametro("PrestRazao",PrestRazao),
                AdicionaParametro("PrestEnd",PrestEnd),
                AdicionaParametro("PrestTel",PrestTel),
                AdicionaParametro("PrestMunicipio",PrestMunicipio),
                AdicionaParametro("PrestUf",PrestUf),
                AdicionaParametro("PrestEmail",PrestEmail),
                AdicionaParametro("TomadorCnpj", TomadorCnpj),
                AdicionaParametro("TomadorRazao",TomadorRazao),
                AdicionaParametro("TomadorMunicipio",TomadorMunicipio),
                AdicionaParametro("TomadorUf",TomadorUf),
                AdicionaParametro("TomadorEndereco",TomadorEndereco),
                AdicionaParametro("TomadroEmail",TomadroEmail),
                AdicionaParametro("Discriminacao",Discriminacao),
                AdicionaParametro("ServicoPrestado","Provimento de acesso à internet."),
                AdicionaParametro("ValorNota",VV),
                AdicionaParametro("Observacao",Observacao == string.Empty ? "   " : Observacao == "ND" ? "   " :Observacao),
                AdicionaParametro("TomaIE",TomaIE),
                AdicionaParametro("TomaIM",TomaIM),
                AdicionaParametro("PrestIE",PrestIE),
                AdicionaParametro("TomaTel",TomaTel),
                  AdicionaParametro("Rodape",Rodape)

            });



            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.RefreshReport();
            SavePDFDebito(reportViewer1, empresa.Cnpj.Replace("-", "").Replace("/", "").Replace(".", "") + InfRps.IdentificacaoRps.Serie + InfRps.IdentificacaoRps.Numero.ToString() + ".pdf");

        }

        public void GeraDebito(EnviarLoteRpsEnvioLoteRpsRpsInfRps InfRps, Empresa empresa, EmpresaEndereco EnderecoPrest, string Observacao, string Prestacao)
        {
            string _Discriminacao = string.Empty;
            string[] Prod = InfRps.Servico.Discriminacao.Split(';') ;
            string _produtos = string.Empty;
            string _valorProdutos = string.Empty;
            try
            {
                foreach (var vv in Prod)
                {
                    string[] jj = vv.Split('|');
                    decimal valorr = Convert.ToDecimal(jj[1]);
                    _valorProdutos = _valorProdutos + String.Format("{0:C}", valorr) + Environment.NewLine;
                    _produtos = _produtos + jj[0];
                    if (jj[0].Contains("MundiSpecial") || jj[0].Contains("MundiAccess"))
                    {
                        jj[0] = jj[0] + " - Acesso à internet ";
                    }
                    _Discriminacao = _Discriminacao + jj[0] + " " + String.Format("{0:C}", valorr) + Environment.NewLine;


                }

            }
            catch
            {


            }
            var db = new Class1(true);
            reportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/App_Data/Report/ISS-DEBITO.rdlc");
            reportViewer1.LocalReport.EnableExternalImages = true;
            int ClienteCep = Convert.ToInt32(InfRps.Tomador.Endereco.CodigoMunicipio);
            var Cidade = db.Cidades.First(x => x.CodIbge == ClienteCep);
            string tituloPrefeitura = string.Empty;
            string VV = String.Format("{0:C}", InfRps.Servico.Valores.ValorServicos);
            string _cep = InfRps.Tomador.Endereco.Cep.ToString();
            var _Endereco = db.ClienteEndereco.First(x => x.Cep == _cep);
            string _CCnpj = InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cnpj;
            var Cliente = db.cliente.First(x => x.CpfCnpj == _CCnpj); 
            string NumeroDaNota = InfRps.IdentificacaoRps.Numero.ToString().PadLeft(9, '0');
            string DataHoraEmissao = InfRps.DataEmissao.ToString();
            string PrestCnpj = empresa.Cnpj;
            string PrestIncMunicipal = InfRps.Prestador.InscricaoMunicipal;
            string PrestRazao = empresa.RazaoSocial;
            string PrestEnd = EnderecoPrest.Logradouro + " - " + EnderecoPrest.Cep + " - " + EnderecoPrest.Complemento;
            string PrestTel = empresa.Telefone;
            string PrestMunicipio = EnderecoPrest.Municipio.ToString();
            string PrestUf = EnderecoPrest._Bairro._Cidade._Estado.Estadosigla;
            string PrestEmail = empresa.Email;
            string TomadorCnpj = Convert.ToUInt64(InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cnpj).ToString(@"00\.000\.000\/0000\-00");
            string TomadorRazao =Cliente.RazaoSocial;
            string TomadorMunicipio = Cidade.Nome;
            string TomadorUf = InfRps.Tomador.Endereco.Uf;
            string TomadorEndereco = InfRps.Tomador.Endereco.Endereco;
            string TomadroEmail = Cliente.Email;
            string TomaIE = Cliente.InscEstadual == null?"Isento":Cliente.InscEstadual;
            string TomaIM = Cliente.InscMunicipal == null ? "Isento" : Cliente.InscMunicipal;
            string Discriminacao = _Discriminacao;
            string ServicoPrestado = InfRps.Servico.ItemListaServico.ToString();
            string PrestIE = empresa.InscEstadual == null ? "Isento" : empresa.InscEstadual;
            string ValorNota = VV;
            string TomaTel = Cliente.Telefone;
            string Rodape = Prestacao;


            reportViewer1.LocalReport.SetParameters(new ReportParameter[]
            {
                AdicionaParametro("NumeroDaNota",NumeroDaNota),
                AdicionaParametro("DataHoraEmissao",DataHoraEmissao),
                AdicionaParametro("PrestCnpj", PrestCnpj),
                AdicionaParametro("PrestIncMunicipal",PrestIncMunicipal),
                AdicionaParametro("PrestRazao",PrestRazao),
                AdicionaParametro("PrestEnd",PrestEnd),
                AdicionaParametro("PrestTel",PrestTel),
                AdicionaParametro("PrestMunicipio",PrestMunicipio),
                AdicionaParametro("PrestUf",PrestUf),
                AdicionaParametro("PrestEmail",PrestEmail),
                AdicionaParametro("TomadorCnpj", TomadorCnpj),
                AdicionaParametro("TomadorRazao",TomadorRazao),
                AdicionaParametro("TomadorMunicipio",TomadorMunicipio),
                AdicionaParametro("TomadorUf",TomadorUf),
                AdicionaParametro("TomadorEndereco",TomadorEndereco),
                AdicionaParametro("TomadroEmail",TomadroEmail),
                AdicionaParametro("Discriminacao",Discriminacao),
                AdicionaParametro("ServicoPrestado","Provimento de acesso à internet."),
                AdicionaParametro("ValorNota",VV),
                AdicionaParametro("Observacao",Observacao == string.Empty ? "   " : Observacao == "ND" ? "   " : Observacao == null ? "  ": "   "),
                AdicionaParametro("TomaIE",TomaIE),
                AdicionaParametro("TomaIM",TomaIM),
                AdicionaParametro("PrestIE",PrestIE),
                AdicionaParametro("TomaTel",TomaTel),
                  AdicionaParametro("Rodape",Rodape)

            });



            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.RefreshReport();
            SavePDFDebito(reportViewer1, empresa.Cnpj.Replace("-", "").Replace("/", "").Replace(".", "") + InfRps.IdentificacaoRps.Serie + InfRps.IdentificacaoRps.Numero.ToString() + ".pdf");

        }

        public void GeraDebito(EnviarLoteRpsEnvioLoteRpsRpsInfRps InfRps, Empresa empresa, EmpresaEndereco EnderecoPrest)
        {
            reportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/App_Data/Report/ISS-DEBITO.rdlc");
            reportViewer1.LocalReport.EnableExternalImages = true;
            string tituloPrefeitura = string.Empty;
            string VV = String.Format("{0:C}", InfRps.Servico.Valores.ValorServicos);
            string NumeroDaNota = InfRps.IdentificacaoRps.Numero.ToString().PadLeft(9, '0');
            string DataHoraEmissao = InfRps.DataEmissao.ToString();
            string PrestCnpj = empresa.Cnpj;
            string PrestIncMunicipal = InfRps.Prestador.InscricaoMunicipal;
            string PrestRazao = empresa.RazaoSocial;
            string PrestFantasia = empresa.NomeFantasia;
            string PrestEnd = EnderecoPrest.Logradouro + " - " + EnderecoPrest.Cep + " - " + EnderecoPrest.Complemento;
            string PrestTel = "---";
            string PrestMunicipio = EnderecoPrest.Municipio.ToString();
            string PrestUf = EnderecoPrest._Bairro._Cidade._Estado.Estadosigla;
            string PrestEmail = "-----";
            string TomadorCnpj = Convert.ToUInt64(InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cnpj).ToString(@"00\.000\.000\/0000\-00");
            string TomadorRazao = InfRps.Tomador.RazaoSocial;
            string TomadorMunicipio = InfRps.Tomador.Endereco.CodigoMunicipio.ToString();
            string TomadorUf = InfRps.Tomador.Endereco.Uf;
            string TomadorEndereco = InfRps.Tomador.Endereco.Endereco;
            string TomadroEmail = "----";
            string Discriminacao = retornaDescricao(InfRps.Servico.Discriminacao);
            string ServicoPrestado = InfRps.Servico.ItemListaServico.ToString();
            string ValorNota = VV;
            string Observacao = "ND";



            reportViewer1.LocalReport.SetParameters(new ReportParameter[]
            {
                AdicionaParametro("NumeroDaNota",NumeroDaNota),
                AdicionaParametro("DataHoraEmissao",DataHoraEmissao),
                AdicionaParametro("PrestCnpj", PrestCnpj),
                AdicionaParametro("PrestIncMunicipal",PrestIncMunicipal),
                AdicionaParametro("PrestRazao",PrestRazao),
                AdicionaParametro("PrestFantasia",PrestFantasia),
                AdicionaParametro("PrestEnd",PrestEnd),
                AdicionaParametro("PrestTel","---"),
                AdicionaParametro("PrestMunicipio",PrestMunicipio),
                AdicionaParametro("PrestUf",PrestUf),
                AdicionaParametro("PrestEmail",PrestEmail),
                AdicionaParametro("TomadorCnpj", TomadorCnpj),
                AdicionaParametro("TomadorRazao",TomadorRazao),
                AdicionaParametro("TomadorMunicipio",TomadorMunicipio),
                AdicionaParametro("TomadorUf",TomadorUf),
                AdicionaParametro("TomadorEndereco",TomadorEndereco),
                AdicionaParametro("TomadroEmail",TomadroEmail),
                AdicionaParametro("Discriminacao",Discriminacao),
                AdicionaParametro("ServicoPrestado",ServicoPrestado),
                AdicionaParametro("ValorNota",VV),
                 AdicionaParametro("Observacao","ND")
                
            });



            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.RefreshReport();
            SavePDFDebito(reportViewer1, empresa.Cnpj.Replace("-", "").Replace("/", "").Replace(".", "") + InfRps.IdentificacaoRps.Serie + InfRps.IdentificacaoRps.Numero.ToString() + ".pdf");
            
        }

        public ReportParameter AdicionaParametro(string NomeParametro, string Valor)
        {
            ReportParameter rp = new ReportParameter(NomeParametro, Valor);

            return rp;
        }

        public void SavePDFDebito(ReportViewer viewer, string savePath)
        {
            try
            {
                byte[] Bytes = viewer.LocalReport.Render(format: "PDF", deviceInfo: "");

                using (FileStream stream = new FileStream(HttpContext.Current.Server.MapPath("~/App_Data/Debito/" + savePath), FileMode.Create))
                {
                    stream.Write(Bytes, 0, Bytes.Length);
                }
            }
            catch(Exception erro)
            {


            }

        }
    }
}
