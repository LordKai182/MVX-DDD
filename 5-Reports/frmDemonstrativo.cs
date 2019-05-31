using Infra.EntidadesNaoPersistidas;
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
    public partial  class frmDemonstrativo : Form
    {
        public frmDemonstrativo()
        {
            InitializeComponent();
        }

        private void frmDemonstrativo_Load(object sender, EventArgs e)
        {
            List<Domostrativo> lst = new List<Domostrativo>();
            lst.Add(new Domostrativo { Codigo = "111", DataAtivacao = DateTime.Now.ToShortDateString(), Disponibilidade = "ssss", DisponibilidadeHs = "111", NCircuito = "11", NomeCircuito = "Teste", PontaA = "teste", PontaB = "Teste", QtdCircuito = "1", Servico = "Teste", Titulo = "Teste", ValorBruto = "126", ValorLiquido = "200" });
            ReportDataSource reportDataSource = new ReportDataSource();

            var dataTable = new DataTable();
            dataTable.Columns.Add("QtdCircuito");
            dataTable.Columns.Add("Codigo");
            dataTable.Columns.Add("Disponibilidade");
            dataTable.Columns.Add("DisponibilidadeHs");
            dataTable.Columns.Add("NCircuito");
            dataTable.Columns.Add("NomeCircuito");
            dataTable.Columns.Add("PontaA");
            dataTable.Columns.Add("PontaB");
            dataTable.Columns.Add("Servico");
            dataTable.Columns.Add("Titulo");
            dataTable.Columns.Add("ValorLiquido");
            dataTable.Columns.Add("ValorBruto");

            foreach (var item in lst)
            {
                dataTable.Rows.Add(item.QtdCircuito, item.Codigo, item.Disponibilidade, item.DisponibilidadeHs, item.NCircuito, item.NomeCircuito, item.PontaA, item.PontaB, item.Servico, item.Titulo, item.ValorLiquido, item.ValorBruto);
            }
            //dataTable.Rows.Add("1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1");
           
            this.reportViewer1.LocalReport.DataSources.Clear();

            this.reportViewer1.RefreshReport();
            this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DtsDemostrativo", dataTable));


        }
        public string Gera(List<Domostrativo> lst)
        {
            reportViewer1.RefreshReport();
            reportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/App_Data/Report/DemonstrativoRelatorio.rdlc");
            reportViewer1.LocalReport.EnableExternalImages = true;
            ReportDataSource reportDataSource = new ReportDataSource();

            var dataTable = new DataTable();
            dataTable.Columns.Add("QtdCircuito");
            dataTable.Columns.Add("Codigo");
            dataTable.Columns.Add("Disponibilidade");
            dataTable.Columns.Add("DisponibilidadeHs");
            dataTable.Columns.Add("NCircuito");
            dataTable.Columns.Add("NomeCircuito");
            dataTable.Columns.Add("PontaA");
            dataTable.Columns.Add("PontaB");
            dataTable.Columns.Add("Servico");
            dataTable.Columns.Add("Titulo");
            dataTable.Columns.Add("ValorLiquido");
            dataTable.Columns.Add("ValorBruto");
            dataTable.Columns.Add("DataAtivacao");
            dataTable.Columns.Add("Cnpj");
            dataTable.Columns.Add("TotalBruto");
            dataTable.Columns.Add("TotalLiquido");
            foreach (var item in lst)
            {
                dataTable.Rows.Add(item.QtdCircuito, item.Codigo, item.Disponibilidade, item.DisponibilidadeHs, item.NCircuito, item.NomeCircuito, item.PontaA, item.PontaB, item.Servico, item.Titulo, item.ValorLiquido, item.ValorBruto,item.DataAtivacao,item.Cnpj,item.TotalBruto,item.TotalLiquido);
            }

            this.reportViewer1.LocalReport.DataSources.Clear();

            this.reportViewer1.RefreshReport();
            this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DtsDemostrativo", dataTable));
            this.reportViewer1.RefreshReport();
            return SavePDF(reportViewer1,"Demonstrativo"+lst.First().Cnpj.Replace("CNPJ: ","").Replace("CPF: ","").Replace(".","").Replace("/", "").Replace("-", "") + ".pdf");
        }
        public string SavePDF(ReportViewer viewer, string savePath)
        {
            try
            {
                string retorno = HttpContext.Current.Server.MapPath("~/App_Data/Demostrativos/" + savePath);

                byte[] Bytes = viewer.LocalReport.Render(format: "PDF", deviceInfo: "");

                using (FileStream stream = new FileStream(HttpContext.Current.Server.MapPath("~/App_Data/Demostrativos/" + savePath), FileMode.Create))
                {
                    stream.Write(Bytes, 0, Bytes.Length);
                }

                return retorno;
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
