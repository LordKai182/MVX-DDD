using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 
using System.Drawing.Printing;
using PdfiumViewer;
using System.Threading;
using RawPrint;
using NFSE.Net.Layouts.BHISS;
using System.Web;
using Infra.Entidades;
using Impactro.Cobranca;
using Impactro.WindowsControls;
using Infra;

namespace MVX.Print
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        public void Gera()
        {
            reportViewer1.RefreshReport();
            reportViewer1.LocalReport.ReportPath = @"Notas/ISS.rdlc";
            reportViewer1.LocalReport.EnableExternalImages = true;
            ReportParameter rp = new ReportParameter("NumeroDaNota", "135184");

            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });
            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.RefreshReport();
            SavePDF(reportViewer1, @"C:/teste/NFSe.pdf");
        }
        public void Gera(ConsultarLoteRpsRespostaCompNfse InfRps, string Observacao)
        {
            reportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/App_Data/Report/ISS.rdlc");
            reportViewer1.LocalReport.EnableExternalImages = true;
            string tituloPrefeitura = string.Empty;

            var db = new Class1(true);
            string _cep = InfRps.Nfse.InfNfse.TomadorServico.Endereco.Cep.ToString();
            var _Endereco = db.ClienteEndereco.First(x => x.Cep == _cep);
            string _produtos = string.Empty;
            string _valorProdutos = string.Empty;
            string Discriminacao = string.Empty;
            string[] Prod = InfRps.Nfse.InfNfse.Servico.Discriminacao.Split(';');
            string prescnpj = InfRps.Nfse.InfNfse.PrestadorServico.IdentificacaoPrestador.Cnpj == "7228550000101" ? prescnpj = "07228550000101" : prescnpj = InfRps.Nfse.InfNfse.PrestadorServico.IdentificacaoPrestador.Cnpj; ;
            //prescnpj == "7228550000101" ? prescnpj = "07228550000101" : prescnpj = prescnpj;
            var Empresa = db.Empresa.First(x=>x.Cnpj == prescnpj);
            var _EmpresaEndereco = Empresa._EmpresaEndereco.First();
            try
            {
                foreach (var vv in Prod)
                {
                    string[] jj = vv.Split('|');
                    decimal valorr = Convert.ToDecimal(jj[1]);
                    _valorProdutos = _valorProdutos + String.Format("{0:C}", valorr) + Environment.NewLine;
                    _produtos = _produtos + jj[0];

                    Discriminacao = Discriminacao + jj[0] + " "  +String.Format("{0:C}", valorr) + Environment.NewLine;


                }

            }
            catch
            {


            }

            if (InfRps.Nfse.InfNfse.PrestadorServico.Endereco.Uf == "RJ")
            {
                tituloPrefeitura = "PREFEITURA MUNICIPAL DA CIDADE DO RIO DE JANEIRO";
            }
            if (InfRps.Nfse.InfNfse.PrestadorServico.Endereco.Uf == "MG")
            {
                tituloPrefeitura = "PREFEITURA MUNICIPAL DA CIDADE DE BELO HORIZONTE";
            }
            if (InfRps.Nfse.InfNfse.PrestadorServico.Endereco.Uf == "SP")
            {
                tituloPrefeitura = "PREFEITURA MUNICIPAL DA CIDADE DE SÃO PAULO";
            }
            reportViewer1.LocalReport.SetParameters(new ReportParameter[]
            {
                AdicionaParametro("NumeroDaNota",InfRps.Nfse.InfNfse.Numero.ToString()),
                AdicionaParametro("DataHoraEmissao",InfRps.Nfse.InfNfse.DataEmissao.ToString()),
                AdicionaParametro("CodigoDeVerificacao",InfRps.Nfse.InfNfse.CodigoVerificacao),
                AdicionaParametro("TiluloPrefeitura",tituloPrefeitura),
                AdicionaParametro("PrestCnpj", Convert.ToUInt64(InfRps.Nfse.InfNfse.PrestadorServico.IdentificacaoPrestador.Cnpj).ToString(@"00\.000\.000\/0000\-00")),
                AdicionaParametro("PrestIncMunicipal",Empresa.InscMunicipal),
                AdicionaParametro("PrestadorIE",Empresa.InscEstadual),
                AdicionaParametro("PrestRazao",InfRps.Nfse.InfNfse.PrestadorServico.RazaoSocial),
                AdicionaParametro("PrestFantasia",InfRps.Nfse.InfNfse.PrestadorServico.RazaoSocial),
                AdicionaParametro("PrestEnd",_EmpresaEndereco.Logradouro +" Nº: "+_EmpresaEndereco.Numero +" Compl: "+_EmpresaEndereco.Complemento+" Cep:"+ _EmpresaEndereco.Cep),
                AdicionaParametro("PrestTel",Empresa.Telefone),
                AdicionaParametro("PrestMunicipio",_Endereco._Bairro._Cidade.Nome),
                AdicionaParametro("PrestUf",InfRps.Nfse.InfNfse.PrestadorServico.Endereco.Uf),
                AdicionaParametro("PrestEmail",Empresa.Email),
                AdicionaParametro("TomadorCnpj", Convert.ToUInt64(InfRps.Nfse.InfNfse.TomadorServico.IdentificacaoTomador.CpfCnpj.Cnpj).ToString(@"00\.000\.000\/0000\-00")),
                AdicionaParametro("TomadorRazao",InfRps.Nfse.InfNfse.TomadorServico.RazaoSocial),
                AdicionaParametro("TomadorMunicipio",_Endereco._Bairro._Cidade.Nome),
                AdicionaParametro("TomadorUf",InfRps.Nfse.InfNfse.TomadorServico.Endereco.Uf),
                AdicionaParametro("TomadorEndereco",InfRps.Nfse.InfNfse.TomadorServico.Endereco.Endereco+" Cep "+InfRps.Nfse.InfNfse.TomadorServico.Endereco.Cep),
                AdicionaParametro("TomadroEmail",_Endereco._Cliente.Email == null ? "N/A" : _Endereco._Cliente.Email),
                AdicionaParametro("Discriminacao",Discriminacao),
                AdicionaParametro("ServicoPrestado","01.03.02 - Provimento de acesso à internet."),
                AdicionaParametro("ValorCofins",InfRps.Nfse.InfNfse.Servico.Valores.ValorCofins.ToString()),
                AdicionaParametro("TomadorIE",_Endereco._Cliente.InscEstadual == null || _Endereco._Cliente.InscEstadual == string.Empty? "Isento" : _Endereco._Cliente.InscEstadual),
                AdicionaParametro("TomadorIM",_Endereco._Cliente.InscMunicipal == null || _Endereco._Cliente.InscMunicipal == string.Empty? "Isento" : _Endereco._Cliente.InscMunicipal),
                AdicionaParametro("ValorCSLL",InfRps.Nfse.InfNfse.Servico.Valores.ValorCsll.ToString()),
                AdicionaParametro("ValorINSS",InfRps.Nfse.InfNfse.Servico.Valores.ValorInss.ToString()),
                AdicionaParametro("ValorIRPJ",InfRps.Nfse.InfNfse.Servico.Valores.ValorIr.ToString()),
                AdicionaParametro("ValorPIS",InfRps.Nfse.InfNfse.Servico.Valores.ValorPis.ToString()),
                AdicionaParametro("ValorOutras",InfRps.Nfse.InfNfse.Servico.Valores.OutrasRetencoes.ToString()),
                AdicionaParametro("ValorNota", String.Format("{0:C}",InfRps.Nfse.InfNfse.Servico.Valores.ValorServicos)),
                AdicionaParametro("Observacao",Observacao == null || Observacao == string.Empty? "N/A" : Observacao)

            });



            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.RefreshReport();
            SavePDF(reportViewer1, InfRps.Nfse.InfNfse.Numero.ToString() + ".pdf");
        }

        public void Gera(ConsultarLoteRpsResposta InfRps, string Observacao)
        {
            reportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/App_Data/Report/ISS.rdlc");
            reportViewer1.LocalReport.EnableExternalImages = true;
            string tituloPrefeitura = string.Empty;
            if (InfRps.ListaNfse.CompNfse.Nfse.InfNfse.PrestadorServico.Endereco.Uf == "RJ")
            {
                tituloPrefeitura = "PREFEITURA MUNICIPAL DA CIDADE DO RIO DE JANEIRO";
            }
            if (InfRps.ListaNfse.CompNfse.Nfse.InfNfse.PrestadorServico.Endereco.Uf == "MG")
            {
                tituloPrefeitura = "PREFEITURA MUNICIPAL DA CIDADE DE BELO HORIZONTE";
            }
            if (InfRps.ListaNfse.CompNfse.Nfse.InfNfse.PrestadorServico.Endereco.Uf == "SP")
            {
                tituloPrefeitura = "PREFEITURA MUNICIPAL DA CIDADE DE SÃO PAULO";
            }
            reportViewer1.LocalReport.SetParameters(new ReportParameter[]
            {
                AdicionaParametro("NumeroDaNota",InfRps.ListaNfse.CompNfse.Nfse.InfNfse.Numero.ToString()),
                AdicionaParametro("DataHoraEmissao",InfRps.ListaNfse.CompNfse.Nfse.InfNfse.DataEmissao.ToString()),
                AdicionaParametro("CodigoDeVerificacao",InfRps.ListaNfse.CompNfse.Nfse.InfNfse.CodigoVerificacao),

                AdicionaParametro("TiluloPrefeitura",tituloPrefeitura),
                AdicionaParametro("PrestCnpj", Convert.ToUInt64(InfRps.ListaNfse.CompNfse.Nfse.InfNfse.PrestadorServico.IdentificacaoPrestador.Cnpj).ToString(@"00\.000\.000\/0000\-00")),
                AdicionaParametro("PrestIncMunicipal",InfRps.ListaNfse.CompNfse.Nfse.InfNfse.PrestadorServico.IdentificacaoPrestador.InscricaoMunicipal.ToString()),
                AdicionaParametro("PrestRazao",InfRps.ListaNfse.CompNfse.Nfse.InfNfse.PrestadorServico.RazaoSocial),
                AdicionaParametro("PrestFantasia",InfRps.ListaNfse.CompNfse.Nfse.InfNfse.PrestadorServico.RazaoSocial),
                AdicionaParametro("PrestEnd",InfRps.ListaNfse.CompNfse.Nfse.InfNfse.PrestadorServico.Endereco.Endereco+" - "+InfRps.ListaNfse.CompNfse.Nfse.InfNfse.PrestadorServico.Endereco.Cep+" - "+InfRps.ListaNfse.CompNfse.Nfse.InfNfse.PrestadorServico.Endereco.Complemento),
                AdicionaParametro("PrestTel","---"),
                AdicionaParametro("PrestMunicipio",InfRps.ListaNfse.CompNfse.Nfse.InfNfse.PrestadorServico.Endereco.CodigoMunicipio.ToString()),
                AdicionaParametro("PrestUf",InfRps.ListaNfse.CompNfse.Nfse.InfNfse.PrestadorServico.Endereco.Uf),
                AdicionaParametro("PrestEmail","-----"),
                AdicionaParametro("TomadorCnpj", Convert.ToUInt64(InfRps.ListaNfse.CompNfse.Nfse.InfNfse.TomadorServico.IdentificacaoTomador.CpfCnpj.Cnpj).ToString(@"00\.000\.000\/0000\-00")),
                AdicionaParametro("TomadorRazao",InfRps.ListaNfse.CompNfse.Nfse.InfNfse.TomadorServico.RazaoSocial),
                AdicionaParametro("TomadorMunicipio",InfRps.ListaNfse.CompNfse.Nfse.InfNfse.TomadorServico.Endereco.CodigoMunicipio.ToString()),
                AdicionaParametro("TomadorUf",InfRps.ListaNfse.CompNfse.Nfse.InfNfse.TomadorServico.Endereco.Uf),
                AdicionaParametro("TomadorEndereco",InfRps.ListaNfse.CompNfse.Nfse.InfNfse.TomadorServico.Endereco.Endereco),
                AdicionaParametro("TomadroEmail","----"),
                AdicionaParametro("Discriminacao",InfRps.ListaNfse.CompNfse.Nfse.InfNfse.Servico.Discriminacao),
                AdicionaParametro("ServicoPrestado",InfRps.ListaNfse.CompNfse.Nfse.InfNfse.Servico.ItemListaServico.ToString()),
                AdicionaParametro("ValorCofins","0,00"),
                AdicionaParametro("ValorCSLL","0,00"),
                AdicionaParametro("ValorINSS","0,00"),
                AdicionaParametro("ValorIRPJ","0,00"),
                AdicionaParametro("ValorPIS","0,00"),
                AdicionaParametro("ValorOutras","0,00"),
                AdicionaParametro("ValorNota",InfRps.ListaNfse.CompNfse.Nfse.InfNfse.Servico.Valores.ValorServicos.ToString()),
                 AdicionaParametro("Observacao",Observacao == null || Observacao == string.Empty? "N/A" : Observacao)
            });



            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.RefreshReport();
            SavePDF(reportViewer1, InfRps.ListaNfse.CompNfse.Nfse.InfNfse.Numero.ToString() + ".pdf");
        }

        public void Gera(ConsultarLoteRpsRespostaCompNfse InfRps)
        {
            reportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/App_Data/Report/ISS.rdlc");
            reportViewer1.LocalReport.EnableExternalImages = true;
            string tituloPrefeitura = string.Empty;
            if (InfRps.Nfse.InfNfse.PrestadorServico.Endereco.Uf == "RJ")
            {
                tituloPrefeitura = "PREFEITURA MUNICIPAL DA CIDADE DO RIO DE JANEIRO";
            }
            if (InfRps.Nfse.InfNfse.PrestadorServico.Endereco.Uf == "MG")
            {
                tituloPrefeitura = "PREFEITURA MUNICIPAL DA CIDADE DE BELO HORIZONTE";
            }
            if (InfRps.Nfse.InfNfse.PrestadorServico.Endereco.Uf == "SP")
            {
                tituloPrefeitura = "PREFEITURA MUNICIPAL DA CIDADE DE SÃO PAULO";
            }
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] 
            {
                AdicionaParametro("NumeroDaNota",InfRps.Nfse.InfNfse.Numero.ToString()),
                AdicionaParametro("DataHoraEmissao",InfRps.Nfse.InfNfse.DataEmissao.ToString()),
                AdicionaParametro("CodigoDeVerificacao",InfRps.Nfse.InfNfse.CodigoVerificacao),
               
                AdicionaParametro("TiluloPrefeitura",tituloPrefeitura),
                AdicionaParametro("PrestCnpj", Convert.ToUInt64(InfRps.Nfse.InfNfse.PrestadorServico.IdentificacaoPrestador.Cnpj).ToString(@"00\.000\.000\/0000\-00")),
                AdicionaParametro("PrestIncMunicipal",InfRps.Nfse.InfNfse.PrestadorServico.IdentificacaoPrestador.InscricaoMunicipal),
                AdicionaParametro("PrestRazao",InfRps.Nfse.InfNfse.PrestadorServico.RazaoSocial),
                AdicionaParametro("PrestFantasia",InfRps.Nfse.InfNfse.PrestadorServico.RazaoSocial),
                AdicionaParametro("PrestEnd",InfRps.Nfse.InfNfse.PrestadorServico.Endereco.Endereco+" - "+InfRps.Nfse.InfNfse.PrestadorServico.Endereco.Cep+" - "+InfRps.Nfse.InfNfse.PrestadorServico.Endereco.Complemento),
                AdicionaParametro("PrestTel","---"),
                AdicionaParametro("PrestMunicipio",InfRps.Nfse.InfNfse.PrestadorServico.Endereco.CodigoMunicipio.ToString()),
                AdicionaParametro("PrestUf",InfRps.Nfse.InfNfse.PrestadorServico.Endereco.Uf),
                AdicionaParametro("PrestEmail","-----"),
                AdicionaParametro("TomadorCnpj", Convert.ToUInt64(InfRps.Nfse.InfNfse.TomadorServico.IdentificacaoTomador.CpfCnpj.Cnpj).ToString(@"00\.000\.000\/0000\-00")),
                AdicionaParametro("TomadorRazao",InfRps.Nfse.InfNfse.TomadorServico.RazaoSocial),
                AdicionaParametro("TomadorMunicipio",InfRps.Nfse.InfNfse.TomadorServico.Endereco.CodigoMunicipio.ToString()),
                AdicionaParametro("TomadorUf",InfRps.Nfse.InfNfse.TomadorServico.Endereco.Uf),
                AdicionaParametro("TomadorEndereco",InfRps.Nfse.InfNfse.TomadorServico.Endereco.Endereco),
                AdicionaParametro("TomadroEmail","----"),
                AdicionaParametro("Discriminacao",InfRps.Nfse.InfNfse.Servico.Discriminacao),
                AdicionaParametro("ServicoPrestado",InfRps.Nfse.InfNfse.Servico.ItemListaServico.ToString()),
                AdicionaParametro("ValorCofins",InfRps.Nfse.InfNfse.Servico.Valores.ValorCofins.ToString()),
                AdicionaParametro("ValorCSLL",InfRps.Nfse.InfNfse.Servico.Valores.ValorCsll.ToString()),
                AdicionaParametro("ValorINSS",InfRps.Nfse.InfNfse.Servico.Valores.ValorInss.ToString()),
                AdicionaParametro("ValorIRPJ",InfRps.Nfse.InfNfse.Servico.Valores.ValorIr.ToString()),
                AdicionaParametro("ValorPIS",InfRps.Nfse.InfNfse.Servico.Valores.ValorPis.ToString()),
                AdicionaParametro("ValorOutras",InfRps.Nfse.InfNfse.Servico.Valores.OutrasRetencoes.ToString()),
                AdicionaParametro("ValorNota",InfRps.Nfse.InfNfse.Servico.Valores.ValorServicos.ToString())

            });



            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.RefreshReport();
            SavePDF(reportViewer1, InfRps.Nfse.InfNfse.Numero.ToString()+".pdf");
        }
        public void GeraDebito()
        {
            //reportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/App_Data/Report/ISS.rdlc");
            reportViewer1.LocalReport.EnableExternalImages = true;

            string tituloPrefeitura = string.Empty;
            string VV = String.Format("{0:C}",2);
            reportViewer1.LocalReport.SetParameters(new ReportParameter[]
            {
                AdicionaParametro("NumeroDaNota","TESTE"),
                AdicionaParametro("DataHoraEmissao","TESTE"),
                AdicionaParametro("CodigoDeVerificacao","TESTE"),

                AdicionaParametro("TiluloPrefeitura","TESTE"),
                AdicionaParametro("PrestCnpj","TESTE"),
                AdicionaParametro("PrestIncMunicipal","TESTE"),
                AdicionaParametro("PrestRazao","TESTE"),
                AdicionaParametro("PrestFantasia","TESTE"),
                AdicionaParametro("PrestEnd","TESTE"),
                AdicionaParametro("PrestTel","---"),
                AdicionaParametro("PrestMunicipio","TESTE"),
                AdicionaParametro("PrestUf","TESTE"),
                AdicionaParametro("PrestEmail","-----"),
                AdicionaParametro("TomadorCnpj","TESTE"),
                AdicionaParametro("TomadorRazao","TESTE"),
                AdicionaParametro("TomadorMunicipio","TESTE"),
                AdicionaParametro("TomadorUf","TESTE"),
                AdicionaParametro("TomadorEndereco","TESTE"),
                AdicionaParametro("TomadroEmail","----"),
                AdicionaParametro("Discriminacao","TESTE"),
                AdicionaParametro("ServicoPrestado","TESTE"),
                AdicionaParametro("ValorCofins","TESTE"),
                AdicionaParametro("ValorCSLL","TESTE"),
                AdicionaParametro("ValorINSS","TESTE"),
                AdicionaParametro("ValorIRPJ","TESTE"),
                AdicionaParametro("ValorPIS","TESTE"),
                AdicionaParametro("ValorOutras","TESTE"),
                AdicionaParametro("ValorNota","TESTE"),
                 AdicionaParametro("Observacao","TESTE")

            });



            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.RefreshReport();
            //SavePDFDebito(reportViewer1, "TESTE" + ".pdf");
        }
        public void GeraDebito(EnviarLoteRpsEnvioLoteRpsRpsInfRps InfRps, Empresa empresa, EmpresaEndereco EnderecoPrest)
        {
            //reportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/App_Data/Report/ISS-DEBITO.rdlc");
            reportViewer1.LocalReport.EnableExternalImages = true;
            string tituloPrefeitura = string.Empty;
            string VV = String.Format("{0:C}", InfRps.Servico.Valores.ValorServicos);
            reportViewer1.LocalReport.SetParameters(new ReportParameter[]
            {
                AdicionaParametro("NumeroDaNota",InfRps.IdentificacaoRps.Numero.ToString().PadLeft(9,'0')),
                AdicionaParametro("DataHoraEmissao",InfRps.DataEmissao.ToString()),
                AdicionaParametro("PrestCnpj", empresa.Cnpj),
                AdicionaParametro("PrestIncMunicipal",InfRps.Prestador.InscricaoMunicipal),
                AdicionaParametro("PrestRazao",empresa.RazaoSocial),
                AdicionaParametro("PrestFantasia",empresa.NomeFantasia),
                AdicionaParametro("PrestEnd",EnderecoPrest.Logradouro+" - "+EnderecoPrest.Cep+" - "+EnderecoPrest.Complemento),
                AdicionaParametro("PrestTel","---"),
                AdicionaParametro("PrestMunicipio",EnderecoPrest.Municipio.ToString()),
                AdicionaParametro("PrestUf",EnderecoPrest._Bairro._Cidade._Estado.Estadosigla),
                AdicionaParametro("PrestEmail","-----"),
                AdicionaParametro("TomadorCnpj", Convert.ToUInt64(InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cnpj).ToString(@"00\.000\.000\/0000\-00")),
                AdicionaParametro("TomadorRazao",InfRps.Tomador.RazaoSocial),
                AdicionaParametro("TomadorMunicipio",InfRps.Tomador.Endereco.CodigoMunicipio.ToString()),
                AdicionaParametro("TomadorUf",InfRps.Tomador.Endereco.Uf),
                AdicionaParametro("TomadorEndereco",InfRps.Tomador.Endereco.Endereco),
                AdicionaParametro("TomadroEmail","----"),
                AdicionaParametro("Discriminacao",InfRps.Servico.Discriminacao.Split('|')[1]),
                AdicionaParametro("ServicoPrestado",InfRps.Servico.ItemListaServico.ToString()),
                AdicionaParametro("ValorNota",VV)

            });



            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.RefreshReport();
            SavePDFDebito(reportViewer1, empresa.Cnpj.Replace("-", "").Replace("/", "").Replace(".","") + InfRps.IdentificacaoRps.Serie+InfRps.IdentificacaoRps.Numero.ToString() + ".pdf");
        }
        public ReportParameter AdicionaParametro(string NomeParametro,string Valor)
        {
            ReportParameter rp = new ReportParameter(NomeParametro, Valor);

            return rp;
        }
        public void GeraNf(ConsultarLoteRpsRespostaCompNfse InfRps)
        {

            #region NF
            Infra.EntidadesNaoPersistidas.NfseImpressao nf = new Infra.EntidadesNaoPersistidas.NfseImpressao();
            nf.Cabecalho = "PREFEITURA DA CIDADE DO RIO DE JANEIRO";
            nf.DataHoraEmissao = "TESTE";
            nf.CodigoVerificacao = "TESTE";
            nf.DescriminacaoServico = "TESTE";
            nf.NumeroNota = "TESTE";
            nf.ValorAliquota = "TESTE";
            nf.ValorBaseCalculo = "TESTE";
            nf.ValorCreditoIPTU = "";
            nf.ValorDaNota = "TESTE";
            nf.ValorISS = "TESTE";
            nf.OutrasInformacoes = "TESTE";
            nf.ServicoPrestado = "TESTE";
            nf.DescriminacaoServico = "TESTE";

            // NFSeImpressaoBindingSource.Add(nf);
            #endregion

           
            ReportViewer rpt = new ReportViewer();
            
            //ReportParameter rp = new ReportParameter("localDaLogo", "logo");
            //ReportParameter rp2 = new ReportParameter("localBanco", "caixa");
            //reportViewer1.RefreshReport();
            rpt.LocalReport.ReportPath = @"Notas/NFSe.rdlc";
            rpt.LocalReport.EnableExternalImages = true;
            //reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp, rp2 });
            //BoletoImpressaoBindingSource.Add(bo);
           // NFSeImpressaoBindingSource.Add(nf);
            rpt.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            rpt.RefreshReport();
            SavePDF(rpt, @"C:/teste/NFSe.pdf");
        }
        public void GeraBoleto()
        {
            Infra.EntidadesNaoPersistidas.BoletoImpressao bo = new Infra.EntidadesNaoPersistidas.BoletoImpressao();
            
            #region CARREGABOLETO
            bo.cabecalhoEndereco = "BORGES DE MEDEIROS,633 7º ANDAR/708 " + Environment.NewLine + "RIO DE JANEIRO  - RJ   22250911";
            bo.CabecalhoSacado = "XP INVESTIMENTOS CORRETORA";
            bo.CabecalhoTelefone = "3553-1000";
            
            bo.Banco = "341-7";
            bo.NumeroBoleto = "34191.09008  24578.040305 40438.540003  1  72880000221423";
            bo.CorpoCedente = "MUNDIVOX DO BRASIL LTDA ";
            bo.CorpoVencimento = " 20/09/2017";
            bo.CorpoDataDocumento = "06/09/2017";
            bo.CorpoNumeroDoc = "25022286";
            bo.CorpoEspecieDoc = "DS";
            bo.CorpoAceite = "A";
            bo.CorpoDataDoProcessamento = "06/09/2017";
            bo.CorpoAgenciaCodigoCedente = " 0304 / 04385-4";
            bo.CorpoNossoNumero = "00245780";
            bo.CorpoCarteira = "109";
            bo.CorpoEspecie = "REAL";
            bo.CorpoValorDoDocumento = "2.014,23";
            #endregion

            ReportViewer rpt = new ReportViewer();
            ReportParameter rp = new ReportParameter("localDaLogo", "logo");
            ReportParameter rp2 = new ReportParameter("localBanco", "caixa");
            //reportViewer1.RefreshReport();
            reportViewer1.LocalReport.ReportPath = @"C:/Boletos/Report1.rdlc";
            reportViewer1.LocalReport.EnableExternalImages = true;
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp,rp2 });
            //BoletoImpressaoBindingSource.Add(bo);
            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.RefreshReport();
            SavePDF(reportViewer1, @"C:/teste/Boleto.pdf");
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //GeraBoleto();
            //GeraNf();
            //ConsultarLoteRpsRespostaCompNfse InfRps = new ConsultarLoteRpsRespostaCompNfse();
            //Gera();
            GeraDebito();
        }
        public void ImprimirBoleto(string impressora,string Caminho)
        {
           
            string Filepath = HttpContext.Current.Server.MapPath("~/App_Data/BoletosPDF/"+Caminho+".pdf");
           
            string Filename = HttpContext.Current.Server.MapPath("~/App_Data/BoletosPDF/" + Caminho + ".pdf");
          
            string PrinterName = impressora;

         

            IPrinter printer = new Printer();

            // Print the file
            printer.PrintRawFile(PrinterName, Filepath, Filename);

        }
        public void ImprimirDemostra(string impressora, string Caminho)
        {

            string Filepath = HttpContext.Current.Server.MapPath("~/App_Data/Demonstrativos/Demonstrativo" + Caminho + ".pdf");

            string Filename = HttpContext.Current.Server.MapPath("~/App_Data/Demonstrativos/Demonstrativo" + Caminho + ".pdf");

            string PrinterName = impressora;

            IPrinter printer = new Printer();

            // Print the file
            printer.PrintRawFile(PrinterName, Filepath, Filename);

        }
        public void ImprimirNFSe(string impressora, string Caminho)
        {

            string Filepath = HttpContext.Current.Server.MapPath("~/App_Data/NFSe/" + Caminho + ".pdf");

            string Filename = HttpContext.Current.Server.MapPath("~/App_Data/NFSe/" + Caminho + ".pdf");

            string PrinterName = impressora;

            IPrinter printer = new Printer();

            // Print the file
            printer.PrintRawFile(PrinterName, Filepath, Filename);

        }
        public void ImprimirDebito(string impressora, string Caminho)
        {

            string Filepath = HttpContext.Current.Server.MapPath("~/App_Data/Debito/" + Caminho + ".pdf");

            string Filename = HttpContext.Current.Server.MapPath("~/App_Data/Debito/" + Caminho + ".pdf");

            if(!File.Exists(Filepath))
            {

            }

            string PrinterName = impressora;

            IPrinter printer = new Printer();

            // Print the file
            printer.PrintRawFile(PrinterName, Filepath, Filename);

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
            catch(Exception Erro)
            {
                throw;
               
            }

        }
        public void SavePDF(ReportViewer viewer, string savePath)
        {
            try
            {
                byte[] Bytes = viewer.LocalReport.Render(format: "PDF", deviceInfo: "");

                using (FileStream stream = new FileStream(HttpContext.Current.Server.MapPath("~/App_Data/NFSe/"+savePath), FileMode.Create))
                {
                    stream.Write(Bytes, 0, Bytes.Length);
                }
            }
            catch (Exception erro)
            {

                throw;
            }
           
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            GeraDebito();
        }
    }
}
