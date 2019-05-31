using Infra;
using Infra.Entidades;
using Infra.EntidadesNaoPersistidas;
using NFSE.Net.Layouts.BHISS;
using Repositorios.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Repositorios.Repositorio
{
    public class ArquivosServices : IArquivosServices
    {
        private Utils.ultArquivosPastas _utilPasta = new Utils.ultArquivosPastas();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formulario"></param>
        /// <returns></returns>
        public byte[] Download(FormCollection formulario)
        {
            var db = new Class1(true);
            string[] ListaArquivos = formulario["ArquivosIds"].Split(',');
            List<Tuple<string, string>> Arquivos = new List<Tuple<string, string>>();
            List<CenarioArquivo> lstR = new List<CenarioArquivo>();
            for (int i = 1; i < ListaArquivos.Count(); i++)
            {
                string[] separado = ListaArquivos[i].Split('|');
                Arquivos.Add(new Tuple<string, string>(separado[0], separado[1]));
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(_utilPasta.ArquivoCriaDeStringOrga(Arquivos));

            return fileBytes;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formulario"></param>
        public void Imprimir(FormCollection formulario)
        {
            string[] formCollec = formulario["ArquivosIds"].Split(',');
            string impressora = formulario["Impressora"] == null ? "" : formulario["Impressora"];

            Imprimir(formCollec, impressora);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formCollec"></param>
        /// <param name="impressora"></param>
        public void Imprimir(string[] formCollec, string impressora)
        {

            var db = new Class1(true);
         
            for (int i = 1; i < formCollec.Count(); i++)
            {
                int IDD = Convert.ToInt32(formCollec[i]);
                var Arq = db.CenarioArquivos.First(x => x.CenarioArquivoId == IDD);


                if (Arq.tipoArquivo == "BL")
                {

                    var Boleto = db.Boleto.First(x => x.NumeroDocumento == Arq.NumeroDocumento);

                    string valor = String.Format("{0:C}", new Utils.Calculos().CalculoPorcentagem(0.033, (double)Boleto.ValorBoleto));

                    new MVX.Print.Boleto().Gera(Boleto, valor);
                    
                    new MVX.Print.Form1().ImprimirBoleto(impressora, Arq.NumeroDocumento);
                }
                if (Arq.tipoArquivo == "Demonstrativo")
                {
                    new MVX.Print.Form1().ImprimirDemostra(impressora, Arq.RemessaLote);
                }
                if (Arq.tipoArquivo == TiposEnum.TipoArquivo.ISS.ToString())
                {
                    new MVX.Print.Form1().ImprimirNFSe(impressora, Arq.NumeroDocumento);
                }
                if (Arq.tipoArquivo == TiposEnum.TipoArquivo.ICMS_FORMULARIO.ToString())
                {
                    string AArq = HttpContext.Current.Server.MapPath("~/App_Data/ICMSELE/ICMS" + Arq.NumeroDocumento + ".doc");
                    if (!File.Exists(AArq))
                    {
                        EnviarLoteRpsEnvioLoteRpsRps nota = new EnviarLoteRpsEnvioLoteRpsRps();
                        nota = Newtonsoft.Json.JsonConvert.DeserializeObject<EnviarLoteRpsEnvioLoteRpsRps>(Encoding.ASCII.GetString(Arq.CorpoDocumento));
                        string valor = String.Format("{0:C}", new Utils.Calculos().CalculoPorcentagem(32, (double)Arq.Valor));
                        string dias = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month).ToString();
                        string cliente = Arq.cnpjCpf.Replace("CNPJ:", "").Replace("CPF:", "").Replace(" ", "").Replace("-", "").Replace(".", "").Replace("/", "");
                        var _Cliente = db.cliente.First(x => x.CpfCnpj == cliente);
                        var Contrato = db.Contrato.First(x => x.ClienteId == _Cliente.ClienteId);
                        string Vvencimento = string.Empty;
                        if (Contrato._MapaFaturamento.First().MesParcelas == 2)
                        {
                            dias = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month).ToString();
                            Vvencimento = "01" + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + " - " + dias + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                        }
                        if (Contrato._MapaFaturamento.First().MesParcelas == 3)
                        {
                            dias = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(1).Month).ToString();
                            Vvencimento = "01" + "/" + DateTime.Now.AddMonths(1).Month + "/" + DateTime.Now.Year + " - " + dias + "/" + DateTime.Now.AddMonths(1).Month + "/" + DateTime.Now.Year;

                        }
                        new MVX.Print.Icms().Gera(nota, Arq.NumeroDocumento, Arq.Observacao, valor, Arq.Vencimento.ToString() + "/" + DateTime.Now.AddMonths(1).Month + "/" + DateTime.Now.Year, Vvencimento);

                    }
                    new MVX.Print.Icms().ImprimirFormulario(impressora, Arq.NumeroDocumento);
                }
                if (Arq.tipoArquivo == TiposEnum.TipoArquivo.DEBITO.ToString())
                {
                    try
                    {
                        new MVX.Print.Form1().ImprimirDebito(impressora, Arq.NumeroDocumento);
                    }
                    catch
                    {

                        CriaArquivo(IDD);
                        new MVX.Print.Form1().ImprimirDebito(impressora, Arq.NumeroDocumento);
                    }


                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ArquivoId"></param>
        public void CriaArquivo(int ArquivoId)
        {
            Class1 db = new Class1(true);
            var arquivo = db.CenarioArquivos.First(x => x.CenarioArquivoId == ArquivoId);
            string valor = arquivo.NumeroDocumento.Substring(arquivo.NumeroDocumento.Length - 4, 4);
            int Vv = Convert.ToInt32(valor);
            var tre = db.NotaFiscal.First(x => x.NumeroRps == Vv);
            NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvio obj = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvio();
            obj = Newtonsoft.Json.JsonConvert.DeserializeObject<NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvio>(Encoding.ASCII.GetString(db.NotaFiscalLote.First(x => x.NumeroLote == tre.NumeroLote).CorpoDocumento));

            string cnpj_ = @String.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToInt64(obj.LoteRps.Cnpj));
            //EU TAVA AQUI
            Empresa emp = new Empresa();
            try
            {



                emp = db.Empresa.First(x => x.Cnpj.Contains(cnpj_));
            }
            catch
            {

                emp = db.Empresa.First(x => x.Cnpj.Contains(obj.LoteRps.ListaRps.First(f => f.InfRps.IdentificacaoRps.Numero == valor).InfRps.Tomador.RazaoSocial));
            }
            var empresaEndereco = db.EmpresaEndereco.First(x => x.EmpresaId == emp.EmpresaId);

            new MVX.Print.DebitoFrm().GeraDebito(obj.LoteRps.ListaRps.First(f => f.InfRps.IdentificacaoRps.Numero == valor).InfRps, emp, empresaEndereco);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<CenarioArquivo> RetornaListaArquivos()
        {
            var db = new Class1(true);

            return db.CenarioArquivos.ToList();
        }

        public List<ArquivosLinhaCobranca> RetornoArquivos()
        {
            var db = new Class1(true);

            List<ArquivosLinhaCobranca> Lista = new List<ArquivosLinhaCobranca>();

            foreach (var linha in db.LinhaCobranca)
            {
                var ListaBoleto = linha._Boleto;
                foreach (var boleto in ListaBoleto)
                {
                    ArquivosLinhaCobranca arquivo = new ArquivosLinhaCobranca{ CenarioArquivoId = boleto.BoletoId, CenarioId = 1, cliente = boleto.SacadoNome, cnpjCpf = boleto.CnpJCpfSacado, dataCriacao = boleto.DataCriacao, Observacao = boleto.Observacao, NumeroDocumento = boleto.NumeroDocumento, Valor = boleto.ValorBoleto, Vencimento = boleto.DataVencimentoOriginal.Day, Prestacao = String.Format("{0}/{1}", linha.MesCompetencia, linha.AnoCompetencia) };

                }
                var ListaNotas = linha._NotaFiscal.Where(x=>x.TipoImposto == "DEBITO" || x.TipoImposto == "ICMS_FOR" || x.TipoImposto == "ISS");
                foreach (var nota in ListaNotas)
                {
                    var infCliente = linha._Cliente;
                    ArquivosLinhaCobranca arquivo = new ArquivosLinhaCobranca { CenarioArquivoId = nota.NotaFiscalId, CenarioId = 1, cliente = infCliente.RazaoSocial, cnpjCpf = new Utils.Formatacao().FormataCnpjCpf(infCliente.CpfCnpj), dataCriacao = nota.DataEmissao, Observacao = nota.Observacao, NumeroDocumento = nota.NumeroRps.ToString(), Valor = nota.ValorServicos, Vencimento = nota.DataVencimento.Day, Prestacao = String.Format("{0}/{1}", linha.MesCompetencia, linha.AnoCompetencia) };

                }

            }

            return Lista;
        }



        public void Descricao(FormCollection formulario)
        {
            var db = new Class1(true);
            string[] formCollec = formulario["ArquivosIds"].Split(',');
            string Observacao = formulario["Observacao"];
            foreach (var item in formCollec)
            {
                try
                {
                    int arq = Convert.ToInt32(item);
                    var Arquivo = db.CenarioArquivos.First(x => x.CenarioArquivoId == arq);
                    Arquivo.Observacao = Observacao;
                    db.CenarioArquivos.Update(Arquivo);
                    db.SaveChanges();
            }
                catch
                {

                }
            }
           

        }
    }
}
