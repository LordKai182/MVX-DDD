using Impactro.Cobranca;
using Microsoft.Reporting.WinForms;
using System;
using System.IO;
using System.Web;
using System.Windows.Forms;

namespace MVX.Print
{
    public partial class Boleto : Form
    {
        public Boleto()
        {
            InitializeComponent();
        }

        private void Boleto_Load(object sender, EventArgs e)
        {
            reportViewer1.RefreshReport();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bltFrm"></param>
        public void Gera(Infra.Entidades.Boleto bltFrm)
        {
            reportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/App_Data/Report/Boleto.rdlc");
            reportViewer1.LocalReport.EnableExternalImages = true;
            string tituloPrefeitura = string.Empty;
            if (File.Exists(HttpContext.Current.Server.MapPath("~/App_Data/BoletosPDF/CodigoDeBarras.jpg")))
            {
                File.Delete(HttpContext.Current.Server.MapPath("~/App_Data/BoletosPDF/CodigoDeBarras.jpg"));
            }
            CobUtil.BarCodeImage(bltFrm.Observacao).Save(HttpContext.Current.Server.MapPath("~/App_Data/BoletosPDF/CodigoDeBarras.jpg"));
            string teste = HttpContext.Current.Server.MapPath("~/App_Data/BoletosPDF/CodigoDeBarras.jpg");
            reportViewer1.LocalReport.SetParameters(new ReportParameter[]
            {
                AdicionaParametro("LogoAgencia","logobras"),
                AdicionaParametro("CaminhoCodigoBarras",bltFrm.Observacao),
                AdicionaParametro("AutenticacaoCodigo",bltFrm.AutenticacaoCodigo),
                AdicionaParametro("CedenteNome",bltFrm.Cedente),
                AdicionaParametro("SacadoNome",bltFrm.Sacado),
                AdicionaParametro("NumeroDocumento",bltFrm.NumeroDocumento),
                AdicionaParametro("EspecieMoeda",bltFrm.EspecieMoeda),
                AdicionaParametro("AgenciaCodCedente",bltFrm.Agencia),
                AdicionaParametro("NossoNumero",bltFrm.NossoNumero),
                AdicionaParametro("ValorDocumento",bltFrm.ValorBoleto.ToString()),
                AdicionaParametro("DataProcessamento",bltFrm.DataEmissao.ToShortDateString()),
                AdicionaParametro("localPagamento",bltFrm.localPagamento),
                AdicionaParametro("Agencia",bltFrm.CodigoBanco),
                AdicionaParametro("Aceite",bltFrm.Aceite),
                AdicionaParametro("Carteira",bltFrm.Carteira),
                AdicionaParametro("EspecieDocumento","DM"),
                AdicionaParametro("Vencimento",bltFrm.DataVencimentoOriginal.ToShortDateString())
               

            });

            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.RefreshReport();
            SavePDF(reportViewer1, bltFrm.NumeroDocumento + ".pdf");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bltFrm"></param>
        public void Gera(Infra.Entidades.Boleto bltFrm, string Mora)
        {
            reportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/App_Data/Report/Boleto.rdlc");
            reportViewer1.LocalReport.EnableExternalImages = true;
            string tituloPrefeitura = string.Empty;
            if (File.Exists(HttpContext.Current.Server.MapPath("~/App_Data/BoletosPDF/CodigoDeBarras.jpg")))
            {
                File.Delete(HttpContext.Current.Server.MapPath("~/App_Data/BoletosPDF/CodigoDeBarras.jpg"));
            }
            CobUtil.BarCodeImage(bltFrm.Observacao).Save(HttpContext.Current.Server.MapPath("~/App_Data/BoletosPDF/CodigoDeBarras.jpg"));
            string teste = HttpContext.Current.Server.MapPath("~/App_Data/BoletosPDF/CodigoDeBarras.jpg");
            reportViewer1.LocalReport.SetParameters(new ReportParameter[]
            {
                AdicionaParametro("LogoAgencia","logobras"),
                AdicionaParametro("CaminhoCodigoBarras",bltFrm.Observacao),
                AdicionaParametro("AutenticacaoCodigo",bltFrm.AutenticacaoCodigo),
                AdicionaParametro("CedenteNome",bltFrm.Cedente),
                AdicionaParametro("SacadoNome",bltFrm.Sacado),
                AdicionaParametro("NumeroDocumento",bltFrm.NumeroDocumento),
                AdicionaParametro("EspecieMoeda",bltFrm.EspecieMoeda),
                AdicionaParametro("AgenciaCodCedente",bltFrm.Agencia),
                AdicionaParametro("NossoNumero",bltFrm.NossoNumero),
                AdicionaParametro("ValorDocumento",bltFrm.ValorBoleto.ToString()),
                AdicionaParametro("DataProcessamento",bltFrm.DataEmissao.ToShortDateString()),
                AdicionaParametro("localPagamento",bltFrm.localPagamento),
                AdicionaParametro("Agencia",bltFrm.CodigoBanco),
                AdicionaParametro("Aceite",bltFrm.Aceite),
                AdicionaParametro("Carteira",bltFrm.Carteira),
                AdicionaParametro("EspecieDocumento","DM"),
                AdicionaParametro("Vencimento",bltFrm.DataVencimentoOriginal.ToShortDateString()),
                AdicionaParametro("Mora",Mora)

            });
           
            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.RefreshReport();
            SavePDF(reportViewer1, bltFrm.NumeroDocumento + ".pdf");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="NomeParametro"></param>
        /// <param name="Valor"></param>
        /// <returns></returns>
        public ReportParameter AdicionaParametro(string NomeParametro, string Valor)
        {
            ReportParameter rp = new ReportParameter(NomeParametro, Valor);

            return rp;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewer"></param>
        /// <param name="savePath"></param>
        public void SavePDF(ReportViewer viewer, string savePath)
        {
            try
            {
                byte[] Bytes = viewer.LocalReport.Render(format: "PDF", deviceInfo: "");

                using (FileStream stream = new FileStream(HttpContext.Current.Server.MapPath("~/App_Data/BoletosPDF/" + savePath), FileMode.Create))
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
