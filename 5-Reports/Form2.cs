using Microsoft.Reporting.WinForms;
using NFSE.Net.Layouts.BHISS;
using RawPrint;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVX.Print
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public void GeraNf()
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
            reportViewer1.LocalReport.ReportPath = @"C:/Boletos/NFSe.rdlc";
            reportViewer1.LocalReport.EnableExternalImages = true;
            //reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp, rp2 });
            //BoletoImpressaoBindingSource.Add(bo);
            //NFSeImpressaoBindingSource.Add(nf);
            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.RefreshReport();
            SavePDF(reportViewer1, @"C:/teste/NFSe.pdf");
        }
        public void GeraNf(ConsultarLoteRpsRespostaCompNfse InfRps)
        {

            #region NF
            Infra.EntidadesNaoPersistidas.NfseImpressao nf = new Infra.EntidadesNaoPersistidas.NfseImpressao();
            nf.Cabecalho = "PREFEITURA DA CIDADE DO RIO DE JANEIRO";
            nf.DataHoraEmissao = InfRps.Nfse.InfNfse.DataEmissao.ToShortDateString();
            nf.CodigoVerificacao = InfRps.Nfse.InfNfse.CodigoVerificacao.ToString();
            nf.DescriminacaoServico = InfRps.Nfse.InfNfse.Servico.Discriminacao;
            nf.NumeroNota = InfRps.Nfse.InfNfse.Numero.ToString();
            nf.ValorAliquota = InfRps.Nfse.InfNfse.Servico.Valores.Aliquota.ToString();
            nf.ValorBaseCalculo = InfRps.Nfse.InfNfse.Servico.Valores.BaseCalculo.ToString();
            nf.ValorCreditoIPTU = "";
            nf.ValorDaNota = InfRps.Nfse.InfNfse.Servico.Valores.ValorServicos.ToString();
            nf.ValorISS = InfRps.Nfse.InfNfse.Servico.Valores.ValorIss.ToString();
            nf.OutrasInformacoes = InfRps.Nfse.InfNfse.OutrasInformacoes;
            nf.ServicoPrestado = InfRps.Nfse.InfNfse.Servico.ItemListaServico.ToString() + InfRps.Nfse.InfNfse.Servico.Discriminacao + InfRps.Nfse.InfNfse.Servico.Valores.ValorServicos.ToString();
            nf.DescriminacaoServico = InfRps.Nfse.InfNfse.Servico.ItemListaServico.ToString() + InfRps.Nfse.InfNfse.Servico.Discriminacao + InfRps.Nfse.InfNfse.Servico.Valores.ValorServicos.ToString();

            // NFSeImpressaoBindingSource.Add(nf);
            #endregion

            string path = AppDomain.CurrentDomain.BaseDirectory;
            ReportViewer rpt = new ReportViewer();
            //ReportParameter rp = new ReportParameter("localDaLogo", "logo");
            //ReportParameter rp2 = new ReportParameter("localBanco", "caixa");
            //reportViewer1.RefreshReport();
            reportViewer1.LocalReport.ReportPath =   Path.Combine(path, @"Notas/NFSe.rdlc");
            reportViewer1.LocalReport.EnableExternalImages = true;
            //reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp, rp2 });
            //BoletoImpressaoBindingSource.Add(bo);
            NfseImpressaoBindingSource.Add(nf);
            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.RefreshReport();
            SavePDF(reportViewer1, @"C:/teste/NFSe.pdf");
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            GeraNf();
        }
        public void imprimir(string impressora)
        {
           
            string Filepath = @"C:/teste/NFSe.pdf";

            string Filename = @"C:/teste/NFSe.pdf";

            string PrinterName = impressora;

            IPrinter printer = new Printer();

            // Print the file
            printer.PrintRawFile(PrinterName, Filepath, Filename);

        }
        public void SavePDF(ReportViewer viewer, string savePath)
        {
            try
            {
                byte[] Bytes = viewer.LocalReport.Render(format: "PDF", deviceInfo: "");

                using (FileStream stream = new FileStream(savePath, FileMode.Create))
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
