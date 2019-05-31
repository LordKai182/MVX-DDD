using Microsoft.Reporting.WinForms;
using NFSE.Net.Layouts.BHISS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace MVX.Print
{
    public partial class ISS : Form
    {
        public ISS()
        {
            InitializeComponent();
        }

        private void ISS_Load(object sender, EventArgs e)
        {

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
            SavePDF(reportViewer1, InfRps.Nfse.InfNfse.Numero.ToString() + ".pdf");
        }

        public ReportParameter AdicionaParametro(string NomeParametro, string Valor)
        {
            ReportParameter rp = new ReportParameter(NomeParametro, Valor);

            return rp;
        }

        public void SavePDF(ReportViewer viewer, string savePath)
        {
            try
            {
                byte[] Bytes = viewer.LocalReport.Render(format: "PDF", deviceInfo: "");

                using (FileStream stream = new FileStream(HttpContext.Current.Server.MapPath("~/App_Data/NFSe/" + savePath), FileMode.Create))
                {
                    stream.Write(Bytes, 0, Bytes.Length);
                }
            }
            catch (Exception erro)
            {

                throw;
            }

        }

    }
}
