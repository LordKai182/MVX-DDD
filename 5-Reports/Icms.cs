using GemBox.Document;
using Infra;
using Microsoft.Reporting.WinForms;
using NFSE.Net.Layouts.BHISS;
using RawPrint;
using System;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Windows.Forms;

namespace MVX.Print
{
    public partial class Icms : Form
    {
        public Icms()
        {
            InitializeComponent();
        }

        private void Icms_Load(object sender, EventArgs e)
        {
            GeraTeste();
        }

        public void GeraTeste()
        {
            //reportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/App_Data/Report/ISS-ICMS.rdlc");
            reportViewer1.LocalReport.EnableExternalImages = true;
            string tituloPrefeitura = string.Empty;

        
            reportViewer1.LocalReport.SetParameters(new ReportParameter[]
            {

                AdicionaParametro("NumeroNota","TESTE"),
                AdicionaParametro("NaturezaOperacao","Serviços de Telecomunicações"),
                AdicionaParametro("CodigoNatureza","TESTE"),
                AdicionaParametro("DataEmissao","TESTE"),
                AdicionaParametro("Usuario" ,"TESTE"),
                AdicionaParametro("Endereco","TESTE"),
                AdicionaParametro("Municipio","TESTE"),
                AdicionaParametro("Estado","TESTE"),
                AdicionaParametro("UF","TESTE"),
                AdicionaParametro("CNPJ_CPF","TESTE"),
                AdicionaParametro("IE","TESTE"),
                AdicionaParametro("IM","TESTE"),
                AdicionaParametro("Discriminacao","TESTE"),
                AdicionaParametro("ValorDiscriminacao","TESTE"),
                AdicionaParametro("ValorTotal","TESTE"),
                AdicionaParametro("ValorAliquota","TESTE"),
                AdicionaParametro("Aliquota","32%"),
                AdicionaParametro("Vencimento","TESTE"),
                AdicionaParametro("PeriodoApurado","TESTE")


            });



            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.RefreshReport();
           
            SavePDF(reportViewer1, "ICMSTESTE.pdf");
        }

        private delegate void ExportTo(object sender);

        private void ExportToFile()
        {
            try
            {
                var x = reportViewer1.LocalReport.ListRenderingExtensions();
                RenderingExtension render_ = null;

               
                render_ = x[5];
               
               
                if (render_ != null)
                {
                    var DialogResult = reportViewer1.ExportDialog(render_);
                    if (DialogResult == DialogResult.OK)
                        MessageBox.Show("Done!");
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        public void Gera(EnviarLoteRpsEnvioLoteRpsRps InfRps, string NumeroDocumento, string Observacao, string ValorAliquota, string Vencimento, string Periodo)
        {
            reportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/App_Data/Report/ISS-ICMS.rdlc");
            reportViewer1.LocalReport.EnableExternalImages = true;
            string tituloPrefeitura = string.Empty;

            var db = new Class1(true);
            string _cep = InfRps.InfRps.Tomador.Endereco.Cep;
            var _Endereco = db.ClienteEndereco.First(x => x.Cep == _cep);
            string _produtos = string.Empty;
            string _valorProdutos = string.Empty;

            string[] Prod = InfRps.InfRps.Servico.Discriminacao.Split(';');

            try
            {
                foreach (var vv in Prod)
                {
                    string[] jj = vv.Split('|');
                    decimal valorr = Convert.ToDecimal(jj[1]);
                    _valorProdutos = _valorProdutos + String.Format("{0:C}", valorr)  + Environment.NewLine;
                    _produtos = _produtos + jj[0] + Environment.NewLine;
                  
                }
            }
            catch
            { 


            }
          


            string cnppj = InfRps.InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cnpj.ToString();
            var Cliente = db.cliente.First(x => x.CpfCnpj == cnppj);
            string NumeroNota = InfRps.InfRps.IdentificacaoRps.Numero;
            string NaturezaOperacao = string.Empty;
            string CodigoNatureza = InfRps.InfRps.RegimeEspecialTributacao;
            string DataEmissao = InfRps.InfRps.DataEmissao.ToShortDateString();
            string Usuario = Cliente.RazaoSocial;
            string Endereco = InfRps.InfRps.Tomador.Endereco.Endereco;
            string Municipio = _Endereco._Bairro._Cidade.Nome;
            string Estado = _Endereco._Bairro._Cidade._Estado.Estadonome;
            string UF = InfRps.InfRps.Tomador.Endereco.Uf;
            string CNPJ_CPF = FormataCnpjCpf(InfRps.InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cnpj.ToString()).Replace("CNPJ: ", "");
            string IE = Cliente.InscEstadual;
            string ValorDiscriminacao = _produtos + Environment.NewLine + Environment.NewLine + Environment.NewLine + Observacao;
            string Discriminacao = String.Format("{0:C}", InfRps.InfRps.Servico.Valores.ValorServicos);
            string IM = Cliente.InscMunicipal;
            string ValorTotal = _valorProdutos;
            reportViewer1.LocalReport.SetParameters(new ReportParameter[]
            {
               
                AdicionaParametro("NumeroNota",NumeroNota),
                AdicionaParametro("NaturezaOperacao","Serviços de Telecomunicações"),
                AdicionaParametro("CodigoNatureza",CodigoNatureza),
                AdicionaParametro("DataEmissao",DataEmissao),
                AdicionaParametro("Usuario" ,Usuario),
                AdicionaParametro("Endereco",Endereco),
                AdicionaParametro("Municipio",Municipio),
                AdicionaParametro("Estado",Estado),
                AdicionaParametro("UF",UF),
                AdicionaParametro("CNPJ_CPF",CNPJ_CPF),
                AdicionaParametro("IE",IE == null ? "Isento" : IE),
                AdicionaParametro("IM",IM == null ? "Isento" : IM),
                AdicionaParametro("Discriminacao",ValorDiscriminacao),
                AdicionaParametro("ValorDiscriminacao",ValorTotal),
                AdicionaParametro("ValorTotal",Discriminacao),
                AdicionaParametro("ValorAliquota",ValorAliquota),
                AdicionaParametro("Aliquota","32%"),
                AdicionaParametro("Vencimento",Vencimento),
                AdicionaParametro("PeriodoApurado",Periodo)


            });



            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.RefreshReport();
            SavePDF(reportViewer1, "ICMS" + NumeroDocumento + ".doc");
        }

        public void Gera(EnviarLoteRpsEnvioLoteRpsRps InfRps, string NumeroDocumento, string Observacao)
        {
            reportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/App_Data/Report/ISS-ICMS.rdlc");
            reportViewer1.LocalReport.EnableExternalImages = true;
            string tituloPrefeitura = string.Empty;

            var db = new Class1(true);
            string cnppj = InfRps.InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cnpj.ToString();
            var Cliente = db.cliente.First(x => x.CpfCnpj == cnppj);
            string NumeroNota = InfRps.InfRps.IdentificacaoRps.Numero;
            string NaturezaOperacao = string.Empty;
            string CodigoNatureza = InfRps.InfRps.RegimeEspecialTributacao;
            string DataEmissao = InfRps.InfRps.DataEmissao.ToShortDateString();
            string Usuario = Cliente.RazaoSocial;
            string Endereco = InfRps.InfRps.Tomador.Endereco.Endereco;
            string Municipio = InfRps.InfRps.Tomador.Endereco.Bairro;
            string Estado = InfRps.InfRps.Tomador.Endereco.Uf;
            string UF = InfRps.InfRps.Tomador.Endereco.Uf;
            string CNPJ_CPF = FormataCnpjCpf(InfRps.InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cnpj.ToString()).Replace("CNPJ: ", "");
            string IE = Cliente.InscEstadual;
            string ValorDiscriminacao = InfRps.InfRps.Servico.Discriminacao + Environment.NewLine + Environment.NewLine + Environment.NewLine + Observacao;
            string Discriminacao = InfRps.InfRps.Servico.Valores.ValorServicos.ToString();
            string IM = Cliente.InscMunicipal;
            string ValorTotal = InfRps.InfRps.Servico.Valores.ValorServicos.ToString();
            reportViewer1.LocalReport.SetParameters(new ReportParameter[]
            {
                
                    
                AdicionaParametro("NumeroNota",NumeroNota),
                AdicionaParametro("NaturezaOperacao","Serviços de Telecomunicações"),
                AdicionaParametro("CodigoNatureza",CodigoNatureza),
                AdicionaParametro("DataEmissao",DataEmissao),
                AdicionaParametro("Usuario" ,Usuario),
                AdicionaParametro("Endereco",Endereco),
                AdicionaParametro("Municipio",Municipio),
                AdicionaParametro("Estado",Estado),
                AdicionaParametro("UF",UF),
                AdicionaParametro("CNPJ_CPF",CNPJ_CPF),
                AdicionaParametro("IE",IE == null ? "Isento" : IE),
                AdicionaParametro("IM",IM == null ? "Isento" : IM),
                AdicionaParametro("Discriminacao",ValorDiscriminacao),
                AdicionaParametro("ValorDiscriminacao",Discriminacao ),
                AdicionaParametro("ValorTotal",ValorTotal),
                


            });



            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.RefreshReport();
            SavePDF(reportViewer1, "ICMS" + NumeroDocumento + ".pdf");
        }

        public void Gera(EnviarLoteRpsEnvioLoteRpsRps InfRps, string NumeroDocumento)
        {
            reportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/App_Data/Report/ISS-ICMS.rdlc");
            reportViewer1.LocalReport.EnableExternalImages = true;
            string tituloPrefeitura = string.Empty;

            var db = new Class1(true);
            string cnppj = InfRps.InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cnpj.ToString();
            var Cliente = db.cliente.First(x=>x.CpfCnpj == cnppj);
            reportViewer1.LocalReport.SetParameters(new ReportParameter[]
            {
                AdicionaParametro("NumeroNota",InfRps.InfRps.IdentificacaoRps.Numero),
                AdicionaParametro("NaturezaOperacao","Serviços de Telecomunicações"),
                AdicionaParametro("CodigoNatureza",InfRps.InfRps.RegimeEspecialTributacao),
                AdicionaParametro("DataEmissao",InfRps.InfRps.DataEmissao.ToShortDateString()),
                AdicionaParametro("Usuario" ,Cliente.RazaoSocial),
                AdicionaParametro("Endereco",InfRps.InfRps.Tomador.Endereco.Endereco),
                AdicionaParametro("Municipio",InfRps.InfRps.Tomador.Endereco.Bairro),
                AdicionaParametro("Estado",InfRps.InfRps.Tomador.Endereco.Uf),
                AdicionaParametro("UF",InfRps.InfRps.Tomador.Endereco.Uf),
                AdicionaParametro("CNPJ_CPF",FormataCnpjCpf(InfRps.InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cnpj.ToString()).Replace("CNPJ: ","")),
                AdicionaParametro("IE",Cliente.InscEstadual),
                AdicionaParametro("IM",Cliente.InscMunicipal),
                AdicionaParametro("Discriminacao",InfRps.InfRps.Servico.Discriminacao),
                AdicionaParametro("ValorDiscriminacao", InfRps.InfRps.Servico.Valores.ValorServicos.ToString()),
                AdicionaParametro("ValorTotal", InfRps.InfRps.Servico.Valores.ValorServicos.ToString()),


            });



            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.RefreshReport();
            SavePDF(reportViewer1, "ICMS" + NumeroDocumento+ ".pdf");
        }

        public string FormataCnpjCpf(string CnpjCpf)
        {
            string retorno = string.Empty;
            if (CnpjCpf.Length == 11)
            {
                try
                {
                    return retorno = "CPF: " + @String.Format(@"{0:000\.000\.000\-00}", Convert.ToInt64(CnpjCpf));
                }
                catch
                {

                    return "Não Foi Possivel Formatar o Valor Passado para CPF. ";
                }

            }
            if (CnpjCpf.Length == 14)
            {
                try
                {
                    return retorno = "CNPJ: " + @String.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToInt64(CnpjCpf));
                }
                catch
                {

                    return "Não Foi Possivel Formatar o Valor Passado para CNPJ. ";
                }

            }
            else
            {
                return "Não Foi Possivel Identificar o Valor Passado Para Formatação. ";
            }


            return retorno;
        }

        public void ImprimirFormulario(string impressora, string Caminho)
        {

            string Filepath = HttpContext.Current.Server.MapPath("~/App_Data/ICMSELE/ICMS" + Caminho + ".doc");

            string Filename = HttpContext.Current.Server.MapPath("~/App_Data/ICMSELE/ICMS" + Caminho + ".doc");

            string PrinterName = impressora;

            //Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            //wordApp.Visible = false;


            //    Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add(Filename);
            //    wordApp.ActivePrinter = PrinterName;
            //    wordApp.ActiveDocument.PrintOut(); //this will also work: doc.PrintOut();

            //    doc.Close(SaveChanges: false);
            //    doc = null;


            //// <EDIT to include Jason's suggestion>
            //((Microsoft.Office.Interop.Word._Application)wordApp).Quit(SaveChanges: false);
            //// </EDIT>

            //// Original: wordApp.Quit(SaveChanges: false);
            //wordApp = null;


            Process process = new Process();
            process.StartInfo.FileName = Filename;
            process.StartInfo.Verb = "printto";
            process.StartInfo.Arguments = "\"" + PrinterName + "\"";
            process.Start();
            process.WaitForExit();
            process.Kill();

        }

        public void Gera(EnviarLoteRpsEnvioLoteRpsRps InfRps)
        {
            reportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/App_Data/Report/ISS-ICMS.rdlc");
            reportViewer1.LocalReport.EnableExternalImages = true;
            string tituloPrefeitura = string.Empty;

            reportViewer1.LocalReport.SetParameters(new ReportParameter[]
            {
                AdicionaParametro("NumeroNota",InfRps.InfRps.Id),
                AdicionaParametro("NaturezaOperacao","111"),
                AdicionaParametro("CodigoNatureza","ICMS"),
                AdicionaParametro("DataEmissao","17/10/1986"),
                AdicionaParametro("Usuario", Convert.ToUInt64(InfRps.InfRps.Prestador.Cnpj).ToString(@"00\.000\.000\/0000\-00")),
                AdicionaParametro("Endereco",InfRps.InfRps.Tomador.Endereco.Endereco +"- Bairro"+InfRps.InfRps.Tomador.Endereco.Bairro+" -Cep:"+InfRps.InfRps.Tomador.Endereco.Cep),
                AdicionaParametro("Municipio",InfRps.InfRps.Tomador.Endereco.Bairro),
                AdicionaParametro("Estado",InfRps.InfRps.Tomador.Endereco.Uf),
                AdicionaParametro("UF",InfRps.InfRps.Tomador.Endereco.Uf),
                AdicionaParametro("CNPJ_CPF","---"),
                AdicionaParametro("IE",InfRps.InfRps.Prestador.InscricaoMunicipal),
                AdicionaParametro("IM",InfRps.InfRps.Prestador.InscricaoMunicipal),
                AdicionaParametro("Discriminacao","-----"),
                AdicionaParametro("ValorDiscriminacao", String.Format("{0:C}", InfRps.InfRps.Servico.Valores.ValorServicos)),
                AdicionaParametro("ValorTotal", String.Format("{0:C}", InfRps.InfRps.Servico.Valores.ValorServicos))
               

            });



            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.RefreshReport();
            SavePDF(reportViewer1,"ICMS"+DateTime.Now.ToString("MMyy")+".pdf");
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
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = @"C:\";
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.DefaultExt = "doc";

                saveFileDialog.Filter = "Word Doc(*.docx)| *.docx | PDF(*.pdf) | *.pdf";
                saveFileDialog.CheckPathExists = true;

                Warning[] warnings;
                string[] streams;
                string mimeType;
                string encoding;
                string extension;

                byte[] bytes = reportViewer1.LocalReport.Render("Word", null, out mimeType, out encoding, out extension, out streams, out warnings);

                    var filename = saveFileDialog.FileName;
                    System.IO.FileStream file = new FileStream(HttpContext.Current.Server.MapPath("~/App_Data/ICMSELE/" + savePath), FileMode.Create);
                    file.Write(bytes, 0, bytes.Length);
                    file.Close();
               
            }
            catch (Exception erro)
            {

                throw;
            }

        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
