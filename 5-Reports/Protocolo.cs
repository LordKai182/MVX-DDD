using Infra;
using Microsoft.Reporting.WinForms;
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
    public partial class Protocolo : Form
    {
        public Protocolo()
        {
            InitializeComponent();
        }
        public Protocolo(string Cnpj, string Vencimento, string Endereco)
        {
            GeraProtocolo(Cnpj, Vencimento, Endereco);
        }
        private void Protocolo_Load(object sender, EventArgs e)
        {
            GeraProtocolo();
        }

        public void GeraProtocolo()
        {
            reportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/App_Data/Report/Protocolo.rdlc");
            reportViewer1.LocalReport.EnableExternalImages = true;

            reportViewer1.LocalReport.SetParameters(new ReportParameter[]
            {
                AdicionaParametro("Empresa","Lar dos vagabundos Alados do Skateboard"),
                AdicionaParametro("Codigo","1337"),
                AdicionaParametro("Vencimento","16/04/2018"),
                AdicionaParametro("Local","Casa dos irmãos aquela ali do lado."),
                AdicionaParametro("ISS"," Nº Nota 201800000000091 Nº Paginas: 1"),
                AdicionaParametro("Boleto","Nº Boleto: 2590 Nº Paginas: 1"),
                AdicionaParametro("icms",".."),
                AdicionaParametro("Debito",".."),


            });



            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.RefreshReport();
            SavePDFProtocoTeste(reportViewer1, "Protocolo" + ".pdf");
        }
        public void GeraProtocolo(string Cnpj, string Vencimento, string Endereco)
        {
            var db = new Class1(true);
            reportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/App_Data/Report/Protocolo.rdlc");
            reportViewer1.LocalReport.EnableExternalImages = true;
            var Empresa = db.cliente.First(x => x.CpfCnpj == Cnpj);
            reportViewer1.LocalReport.SetParameters(new ReportParameter[]
            {
                AdicionaParametro("Empresa",Empresa.RazaoSocial),
                AdicionaParametro("Codigo",Empresa.CodRelacionamento),
                AdicionaParametro("Vencimento",Vencimento),
                AdicionaParametro("Local",Endereco),
               
               

            });



            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.RefreshReport();
            SavePDFProtocoTeste(reportViewer1, "Protocolo"+Cnpj+ ".pdf");
        }

        public ReportParameter AdicionaParametro(string NomeParametro, string Valor)
        {
            ReportParameter rp = new ReportParameter(NomeParametro, Valor);

            return rp;
        }
        public void SavePDFProtoco(ReportViewer viewer, string savePath)
        {
            try
            {
                byte[] Bytes = viewer.LocalReport.Render(format: "PDF", deviceInfo: "");

                using (FileStream stream = new FileStream(HttpContext.Current.Server.MapPath("~/App_Data/Debito/" + savePath), FileMode.Create))
                {
                    stream.Write(Bytes, 0, Bytes.Length);
                }
            }
            catch
            {


            }

        }

        public void SavePDFProtocoTeste(ReportViewer viewer, string savePath)
        {
            try
            {
                byte[] Bytes = viewer.LocalReport.Render(format: "PDF", deviceInfo: "");

                using (FileStream stream = new FileStream(@"C:\teste\Protocolo", FileMode.Create))
                {
                    stream.Write(Bytes, 0, Bytes.Length);
                }
            }
            catch
            {


            }

        }


    }
}
