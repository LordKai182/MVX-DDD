using Infra;
using Infra.Entidades;
using NFSE.Net.Layouts.BHISS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using TiposEnum;

namespace Utils
{
    public class ultArquivosPastas
    {

        public string defineExtensao(string ext)
        {
            if (Enum.IsDefined(typeof(TipoArquivo), ext))
            {
                switch ((TipoArquivo)Enum.Parse(typeof(TipoArquivo), ext))
                {
                    case TipoArquivo.Boleto:
                        return ".txt";
                        break;
                    case TipoArquivo.ICMS:
                        return ".pdf";
                        break;
                    case TipoArquivo.ISS:
                        return ".xml";
                        break;

                }

            }
            return "";
        }

        #region FUNCOES PARA ARQUIVOS
        public void ArquivoCriaDeString(List<CenarioArquivo> lst, string caminho)
        {
            List<string> lstRR = new List<string>();
            for (int i = 0; i < lst.Count; i++)
            {


                //StreamWriter wr = new StreamWriter(Path.Combine(caminho, lst[i].tipoArquivo + lst[i]._CenarioDados.CenarioCodigo + lst[i].plano+i.ToString() + defineExtensao(lst[i].tipoArquivo)));
                // wr.Write(ArquivoByteAreaParaString(lst[i].arquivo));
                //wr.Close();
                // lstRR.Add(Path.Combine(caminho, lst[i].tipoArquivo + lst[i]._CenarioDados.CenarioCodigo + lst[i].plano+ i.ToString() + defineExtensao(lst[i].tipoArquivo)));

            }
            ZIP.AddListCompress(lstRR, Path.Combine(caminho, "Arquivos.zip"));
            for (int i = 0; i < lstRR.Count; i++)
            {

                File.Delete(lstRR[i]);
            }

        }
        public void CriarDebito(int NotaId, string NovoCaminho)
        {
            
             string Arq = GerarDebito(NotaId);
          
             File.Copy(HttpContext.Current.Server.MapPath("~/App_Data/Debito/" + Arq + ".pdf"), NovoCaminho + Arq + ".pdf");

        }
        public void CriarICMS(int IcmsId, string NovoCaminho)
        {
            var db = new Class1(true);
            var icms = db.NotaFiscal.First(x => x.NotaFiscalId == IcmsId);
           string AArq = HttpContext.Current.Server.MapPath("~/App_Data/ICMSELE/ICMS" + icms.NumeroRps + ".doc");
            if (File.Exists(AArq))
            {
                File.Delete(AArq);
            }
            if (!File.Exists(AArq))
            {
                EnviarLoteRpsEnvioLoteRpsRps nota = new EnviarLoteRpsEnvioLoteRpsRps();
                nota = Newtonsoft.Json.JsonConvert.DeserializeObject<EnviarLoteRpsEnvioLoteRpsRps>(Encoding.ASCII.GetString(icms.PDF));
                string valor = String.Format("{0:C}", new Utils.Calculos().CalculoPorcentagem(32, (double)icms.ValorServicos));

                
                var _Cliente = db.cliente.First(x => x.ClienteId == icms.ClienteId);
                var Contrato = db.Contrato.First(x => x.ClienteId == _Cliente.ClienteId);
                string dias = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month).ToString();
                string Vvencimento = string.Empty;
                var LinhaCob = db.LinhaCobranca.First(x => x.LinhaCobrancaId == icms.LinhaCobrancaId);
                dias = DateTime.DaysInMonth(Convert.ToInt32(LinhaCob.AnoCompetencia), Convert.ToInt32(LinhaCob.MesCompetencia)).ToString();
                Vvencimento = String.Format("01/{0}/{1} - {0}/{1}/{2}",LinhaCob.MesCompetencia,LinhaCob.AnoCompetencia,dias,LinhaCob.MesCompetencia,LinhaCob.AnoCompetencia);
                new MVX.Print.Icms().Gera(nota, icms.NumeroRps.ToString(), icms.Observacao, valor, icms.DataVencimento.Day.ToString() + "/" + icms.DataVencimento.ToString("MM/yyyy"), Vvencimento);


            }

            File.Copy(HttpContext.Current.Server.MapPath("~/App_Data/ICMSELE/ICMS" + icms.NumeroRps.ToString() + ".doc"), NovoCaminho + icms.NumeroRps.ToString() + ".doc");
        }
        public void CriarISS(int NotaID, string NovoCaminho)
        {
            Class1 db = new Class1(true);
            var iss = db.NotaFiscal.First(x => x.NotaFiscalId == NotaID);
            if (!File.Exists(HttpContext.Current.Server.MapPath("~/App_Data/NFSe/" + iss.NumeroRps + ".pdf")))
            {

                try
                {
                    ConsultarLoteRpsRespostaCompNfse nota = new ConsultarLoteRpsRespostaCompNfse();
                    nota = Newtonsoft.Json.JsonConvert.DeserializeObject<ConsultarLoteRpsRespostaCompNfse>(Encoding.ASCII.GetString(iss.PDF));

                    new MVX.Print.Form1().Gera(nota, iss.Observacao);
                }
                catch (Exception erro)
                {

                    MVX.Print.ConsultarLoteRpsResposta nota = new MVX.Print.ConsultarLoteRpsResposta();
                    nota = Newtonsoft.Json.JsonConvert.DeserializeObject<MVX.Print.ConsultarLoteRpsResposta>(Encoding.ASCII.GetString(iss.PDF));
                    new MVX.Print.Form1().Gera(nota, iss.Observacao);

                }


            }

            File.Copy(HttpContext.Current.Server.MapPath("~/App_Data/NFSe/" + iss.NumeroRps + ".pdf"), NovoCaminho + iss.NumeroRps + ".pdf");


        }
        public void CriarBoleto(int BauId, string NovoCaminho)
        {
            Class1 db = new Class1(true);
            var Boleto = db.Boleto.First(x => x.BoletoId == BauId);
            string valor = String.Format("{0:C}", new Utils.Calculos().CalculoPorcentagem(0.033, (double)Boleto.ValorBoleto));
            new MVX.Print.Boleto().Gera(Boleto, valor);
            File.Copy(HttpContext.Current.Server.MapPath("~/App_Data/BoletosPDF/" + Boleto.NumeroDocumento + ".pdf"), NovoCaminho + Boleto.NumeroDocumento + ".pdf");

        }
        public string ArquivoCriaDeStringOrga(List<Tuple<string,string>> lst)
        {
            string novocaminho = string.Empty;
            novocaminho = HttpContext.Current.Server.MapPath("~/App_Data/Documentos/");
            if (Directory.Exists(novocaminho))
            {
                Directory.Delete(novocaminho, true);

            }
            Class1 db = new Class1(true);
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/App_Data/Documentos/"));
            foreach (var item in lst)
            {
                try
                {
                     if (item.Item2 == "BL")
                    {
                        CriarBoleto(Convert.ToInt32(item.Item1), novocaminho);
                    }
                    if (item.Item2 == "DEBITO")
                    {
                        CriarDebito(Convert.ToInt32(item.Item1),novocaminho);
                    }
                    if (item.Item2 == "ISS")
                    {
                        CriarISS(Convert.ToInt32(item.Item1), novocaminho);
                    }
                    if (item.Item2 == "ICMS_FOR")
                    {

                      CriarICMS(Convert.ToInt32(item.Item1), novocaminho);

                    }
                }
                catch
                {

                }

            }
            List<string> lstStr = new List<string>();
            string[] arquivos = Directory.GetFiles(novocaminho);
            foreach (var item in arquivos)
            {
                lstStr.Add(item);
            }
            ZIP.AddListCompress(lstStr, Path.Combine(novocaminho, "Arquivos.zip"));
            return Path.Combine(novocaminho, "Arquivos.zip");



        }

        public string ArquivoCriaDeStringOrga(List<CenarioArquivo> lst)
        {
            string novocaminho = string.Empty;
            novocaminho = HttpContext.Current.Server.MapPath("~/App_Data/Documentos/");
            if (Directory.Exists(novocaminho))
            {
                Directory.Delete(novocaminho, true);

            }
            Class1 db = new Class1(true);
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/App_Data/Documentos/"));
            foreach (var item in lst)
            {
                try
                {


                    if (item.tipoArquivo == "Demonstrativo")
                    {

                        File.Copy(HttpContext.Current.Server.MapPath("~/App_Data/Demostrativos/Demonstrativo" + item.RemessaLote + ".pdf"), novocaminho + "Demonstrativo" + item.RemessaLote + ".pdf");
                    }
                    if (item.tipoArquivo == "BL")
                    {
                        var Boleto = db.Boleto.First(x => x.NumeroDocumento == item.NumeroDocumento);

                        string valor = String.Format("{0:C}", new Utils.Calculos().CalculoPorcentagem(0.033, (double)item.Valor));
                        new MVX.Print.Boleto().Gera(Boleto, valor);
                        File.Copy(HttpContext.Current.Server.MapPath("~/App_Data/BoletosPDF/" + item.NumeroDocumento + ".pdf"), novocaminho + item.NumeroDocumento + ".pdf");
                    }
                    if (item.tipoArquivo == "DEBITO")
                    {
                        if (!File.Exists(HttpContext.Current.Server.MapPath("~/App_Data/Debito/" + item.NumeroDocumento + ".pdf")))
                        {
                            CriaArquivo(item.CenarioArquivoId);
                        }


                        File.Copy(HttpContext.Current.Server.MapPath("~/App_Data/Debito/" + item.NumeroDocumento + ".pdf"), novocaminho + item.NumeroDocumento + ".pdf");
                    }
                    if (item.tipoArquivo == "ISS")
                    {
                        if (!File.Exists(HttpContext.Current.Server.MapPath("~/App_Data/NFSe/" + item.NumeroDocumento + ".pdf")))
                        {

                            try
                            {
                                ConsultarLoteRpsRespostaCompNfse nota = new ConsultarLoteRpsRespostaCompNfse();
                                nota = Newtonsoft.Json.JsonConvert.DeserializeObject<ConsultarLoteRpsRespostaCompNfse>(Encoding.ASCII.GetString(item.CorpoDocumento));

                                new MVX.Print.Form1().Gera(nota, item.Observacao);
                            }
                            catch (Exception erro)
                            {

                                MVX.Print.ConsultarLoteRpsResposta nota = new MVX.Print.ConsultarLoteRpsResposta();
                                nota = Newtonsoft.Json.JsonConvert.DeserializeObject<MVX.Print.ConsultarLoteRpsResposta>(Encoding.ASCII.GetString(item.CorpoDocumento));
                                new MVX.Print.Form1().Gera(nota, item.Observacao);

                            }


                        }

                        File.Copy(HttpContext.Current.Server.MapPath("~/App_Data/NFSe/" + item.NumeroDocumento + ".pdf"), novocaminho + item.NumeroDocumento + ".pdf");

                    }
                    if (item.tipoArquivo == "ICMS_FORMULARIO")
                    {
                        string AArq = HttpContext.Current.Server.MapPath("~/App_Data/ICMSELE/ICMS" + item.NumeroDocumento + ".doc");
                        if (File.Exists(AArq))
                        {
                            File.Delete(AArq);
                        }
                        if (!File.Exists(AArq))
                        {
                            EnviarLoteRpsEnvioLoteRpsRps nota = new EnviarLoteRpsEnvioLoteRpsRps();
                            nota = Newtonsoft.Json.JsonConvert.DeserializeObject<EnviarLoteRpsEnvioLoteRpsRps>(Encoding.ASCII.GetString(item.CorpoDocumento));
                            string valor = String.Format("{0:C}", new Utils.Calculos().CalculoPorcentagem(32, (double)item.Valor));

                            string cliente = item.cnpjCpf.Replace("CNPJ:", "").Replace("CPF:", "").Replace(" ", "").Replace("-", "").Replace(".", "").Replace("/", "");
                            var _Cliente = db.cliente.First(x => x.CpfCnpj == cliente);
                            var Contrato = db.Contrato.First(x => x.ClienteId == _Cliente.ClienteId);
                            string dias = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month).ToString();
                            string Vvencimento = string.Empty;
                            if (Contrato._MapaFaturamento.First().MesParcelas == 2)
                            {
                                dias = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(1).Month).ToString();
                                var Data = DateTime.Now.AddMonths(1);
                                Vvencimento = "01" + "/" + Data.ToString("MM/yyyy") + " - " + dias + "/" + Data.ToString("MM/yyyy");
                                new MVX.Print.Icms().Gera(nota, item.NumeroDocumento, item.Observacao, valor, item.Vencimento.ToString() + "/" + Data.ToString("MM/yyyy"), Vvencimento);

                            }
                            if (Contrato._MapaFaturamento.First().MesParcelas == 1)
                            {
                                dias = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month).ToString();
                                var Data = DateTime.Now;
                                Vvencimento = "01" + "/" + Data.ToString("MM/yyyy") + " - " + dias + "/" + Data.ToString("MM/yyyy");
                                new MVX.Print.Icms().Gera(nota, item.NumeroDocumento, item.Observacao, valor, item.Vencimento.ToString() + "/" + Data.ToString("MM/yyyy"), Vvencimento);

                            }

                        }

                        File.Copy(HttpContext.Current.Server.MapPath("~/App_Data/ICMSELE/ICMS" + item.NumeroDocumento + ".doc"), novocaminho + item.NumeroDocumento + ".doc");

                    }
                }
                catch
                {

                }

            }
            List<string> lstStr = new List<string>();
            string[] arquivos = Directory.GetFiles(novocaminho);
            foreach (var item in arquivos)
            {
                lstStr.Add(item);
            }
            ZIP.AddListCompress(lstStr, Path.Combine(novocaminho, "Arquivos.zip"));
            return Path.Combine(novocaminho, "Arquivos.zip");



        }

        public string GerarDebito(int ArquivoId)
        {
            Class1 db = new Class1(true);
            var debito = db.NotaFiscal.First(x => x.NotaFiscalId == ArquivoId);

            NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRps obj = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRps();
            obj = Newtonsoft.Json.JsonConvert.DeserializeObject<NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvioLoteRpsRps>(Encoding.ASCII.GetString(debito.PDF));

            string cnpj_ = obj.InfRps.Prestador.Cnpj;
            //EU TAVA AQUI
            Empresa emp = new Empresa();
            emp = db.Empresa.First(x => x.Cnpj.Contains(cnpj_));

            var empresaEndereco = db.EmpresaEndereco.First(x => x.EmpresaId == emp.EmpresaId);
            string dias = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month).ToString();
            string Vvencimento = string.Empty;
            string cliente = obj.InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cnpj.Replace("CNPJ:", "").Replace("CPF:", "").Replace(" ", "").Replace("-", "").Replace(".", "").Replace("/", "");
            var _Cliente = db.cliente.First(x => x.CpfCnpj == cliente);
            var Contrato = db.Contrato.First(x => x.ClienteId == _Cliente.ClienteId);
            string Pres = string.Empty;
            var LinhaCob = db.LinhaCobranca.First(x => x.LinhaCobrancaId == debito.LinhaCobrancaId);
            Pres = String.Format("Vencimento: {0}       Mês prestação: {1}", debito.DataVencimento.ToShortDateString(), LinhaCob.MesCompetencia+"/"+LinhaCob.AnoCompetencia);
            
            new MVX.Print.DebitoFrm().GeraDebito(obj.InfRps, emp, empresaEndereco, debito.Observacao, Pres);

            return obj.InfRps.Prestador.Cnpj+"DBT"+obj.InfRps.IdentificacaoRps.Numero.ToString();
        }

        public void CriaArquivo(int ArquivoId)
        {
             
            Class1 db = new Class1(true);
            var arquivo = db.CenarioArquivos.First(x => x.CenarioArquivoId == ArquivoId);
            string valor = arquivo.NumeroDocumento.Substring(17, (arquivo.NumeroDocumento.Length - 17));
            int Vv = Convert.ToInt32(valor);
            var tre = db.NotaFiscal.First(x => x.NumeroRps == Vv);
            NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvio obj = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvio();
            obj = Newtonsoft.Json.JsonConvert.DeserializeObject<NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvio>(Encoding.ASCII.GetString(db.NotaFiscalLote.First(x => x.NumeroLote == tre.NumeroLote).CorpoDocumento));

            string cnpj_ = obj.LoteRps.Cnpj;
            //EU TAVA AQUI
            Empresa emp = new Empresa();
            emp = db.Empresa.First(x => x.Cnpj.Contains(cnpj_));

            var empresaEndereco = db.EmpresaEndereco.First(x => x.EmpresaId == emp.EmpresaId);
            string dias = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month).ToString();
            string Vvencimento = string.Empty;
            string cliente = arquivo.cnpjCpf.Replace("CNPJ:", "").Replace("CPF:", "").Replace(" ", "").Replace("-", "").Replace(".", "").Replace("/", "");
            var _Cliente = db.cliente.First(x => x.CpfCnpj == cliente);
            var Contrato = db.Contrato.First(x => x.ClienteId == _Cliente.ClienteId);
            string Pres = string.Empty;
            if (Contrato._MapaFaturamento.First().MesParcelas == 2)
            {
                var DataVenciemtno = DateTime.Now.AddMonths(1);
                var DataPrestacao = DateTime.Now;
                Vvencimento = Contrato._MapaFaturamento.First().Vencimento.ToString()+ "/" + DataVenciemtno.ToString("MM/yyyy");
                Pres = String.Format("Vencimento: {0}       Mês prestação: {1}", Vvencimento, DataPrestacao.ToString("MM/yyyy"));
                Pres = Pres + Environment.NewLine + "Empresa: "+Contrato._ContratoRelacionamento.First().DadosBabelId;
            }
          
            if (Contrato._MapaFaturamento.First().MesParcelas == 1)
            {
                var DataVenciemtno = DateTime.Now;
                var DataPrestacao = DateTime.Now;
                Vvencimento = Contrato._MapaFaturamento.First().Vencimento.ToString() + "/" + DataVenciemtno.ToString("MM/yyyy");
                Pres = String.Format("Vencimento: {0}       Mês prestação: {1}", Vvencimento, DataPrestacao.ToString("MM/yyyy"));
                Pres = Pres + Environment.NewLine + "Empresa: " + Contrato._ContratoRelacionamento.First().DadosBabelId;
            }
            new MVX.Print.DebitoFrm().GeraDebito(obj.LoteRps.ListaRps.First(f => f.InfRps.IdentificacaoRps.Numero == valor).InfRps, emp, empresaEndereco,arquivo.Observacao, Pres);
        }

        public string ArquivoCriaDeStringICMS(List<string> lst)
        {
            string novocaminho = string.Empty;
            novocaminho = HttpContext.Current.Server.MapPath("~/App_Data/Documentos/");
            if (Directory.Exists(novocaminho))
            {
                Directory.Delete(novocaminho, true);

            }

            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/App_Data/Documentos/"));
            foreach (var item in lst)
            {

                File.Copy(HttpContext.Current.Server.MapPath("~/App_Data/ICMS_Arquivo/" + item + "M.txt"), novocaminho + item + "M.txt");
                File.Copy(HttpContext.Current.Server.MapPath("~/App_Data/ICMS_Arquivo/" + item + "I.txt"), novocaminho + item + "I.txt");
                File.Copy(HttpContext.Current.Server.MapPath("~/App_Data/ICMS_Arquivo/" + item + "D.txt"), novocaminho + item + "D.txt");


            }
            List<string> lstStr = new List<string>();
            string[] arquivos = Directory.GetFiles(novocaminho);
            foreach (var item in arquivos)
            {
                lstStr.Add(item);
            }
            ZIP.AddListCompress(lstStr, Path.Combine(novocaminho, "ICMS.zip"));
            return Path.Combine(novocaminho, "ICMS.zip");



        }
        public string ArquivoCriaDeStringOrgaRemessa(List<BoletoRemessa> lst)
        {
            string novocaminho = string.Empty;
            novocaminho = HttpContext.Current.Server.MapPath("~/App_Data/Documentos/");
            if (Directory.Exists(novocaminho))
            {
                Directory.Delete(novocaminho, true);

            }

            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/App_Data/Documentos/"));
            foreach (var item in lst)
            {

                File.Copy(HttpContext.Current.Server.MapPath("~/App_Data/Remessa/CNAB400" + item.Lote+".txt"), novocaminho + item.Lote + ".txt");



            }
            List<string> lstStr = new List<string>();
            string[] arquivos = Directory.GetFiles(novocaminho);
            foreach (var item in arquivos)
            {
                lstStr.Add(item);
            }
            ZIP.AddListCompress(lstStr, Path.Combine(novocaminho, "Remessa.zip"));
            return Path.Combine(novocaminho, "Remessa.zip");



        }

        public void ArquivoExisteAcoes(string caminhoArquivo, bool criaSeNaoHouver, bool deletaSeHouver, bool criaSeDeletar)
        {
            if (File.Exists(caminhoArquivo))
            {
                if (deletaSeHouver)
                    File.Delete(caminhoArquivo);
                if (criaSeDeletar)
                    File.Create(caminhoArquivo);
            }
            if (!File.Exists(caminhoArquivo))
            {
                if (criaSeNaoHouver)
                    File.Delete(caminhoArquivo);
            }
        }

        public bool VerificaSeExiste(string caminhoArquivo)
        {
            if (File.Exists(caminhoArquivo))
            {
                return true;
            }

            if (!File.Exists(caminhoArquivo))
            {
                return false;
            }

            return false;
        }

        public bool DeletaArquivoSeExiste(string caminhoArquivo)
        {
            if (File.Exists(caminhoArquivo))
            {
                File.Delete(caminhoArquivo);
                return true;
            }
            return false;
        }

        public long TamanhoDoArquivo(string caminhoArquivo)
        {

            if (File.Exists(caminhoArquivo))
            {
                FileInfo arq = new FileInfo(caminhoArquivo);
                return arq.Length;
            }


            return 0;
        }

        public string ArquivoByteAreaParaString(byte[] area)
        {
            string something = Encoding.ASCII.GetString(area);
            return something;
        }

        public byte[] ArquivoStringParaByteArea(string Arquivo)
        {
            byte[] toBytes = Encoding.ASCII.GetBytes(Arquivo);
            return toBytes;
        }

        public static string Base64Cidificado(string caminhoArquivo)
        {
            StringReader re = new StringReader(caminhoArquivo);
            string plainText = re.ReadToEnd();
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decodificado(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string ArquivoLer(string caminhoArquivo)
        {
            string plainText = string.Empty;
            if (File.Exists(caminhoArquivo))
            {
                StringReader re = new StringReader(caminhoArquivo);
                plainText = re.ReadToEnd();
            }
            return plainText;
        }

        public bool ArquivoCopiar(string caminhoArqoriginal, string caminhoCopia)
        {
            if (File.Exists(caminhoArqoriginal))
            {
                File.Copy(caminhoArqoriginal, caminhoCopia);
                return true;
            }
            return false;
        }
        #endregion

        #region FUNCOES PARA PASTAS 
        public void PastaVerificaExistencia(string caminhoPasta, bool criaSeNaoExiste, bool deletaSeExiste)
        {
            if (Directory.Exists(caminhoPasta) && criaSeNaoExiste)
            {
                Directory.CreateDirectory(caminhoPasta);
            }
            if (Directory.Exists(caminhoPasta) && deletaSeExiste)
            {
                Directory.Delete(caminhoPasta);
            }
        }

        public bool PastaVerificaExistencia(string caminhoPasta)
        {
            if (Directory.Exists(caminhoPasta))
            {
                return true;
            }
            return false;
        }

        public bool PastaCriaPasta(string caminhoPasta, bool deletaSeExistir)
        {
            if (Directory.Exists(caminhoPasta))
            {
                if (deletaSeExistir)
                {
                    Directory.Delete(caminhoPasta);
                }
                Directory.CreateDirectory(caminhoPasta);
                return true;
            }
            return false;
        }

        public void PastaCopiarPasta(string pastaDeOrigem, string pastadeDestino, bool copiarSubDiretorios)
        {
            var dir = new DirectoryInfo(pastaDeOrigem);
            var dirs = dir.GetDirectories();

            // If the source directory does not exist, throw an exception.
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Diretorio de Origem não Existe o unão pode ser encontrado "
                    + pastaDeOrigem);
            }

            // If the destination directory does not exist, create it.
            if (!Directory.Exists(pastadeDestino))
            {
                Directory.CreateDirectory(pastadeDestino);
            }


            // Get the file contents of the directory to copy.
            var files = dir.GetFiles();

            foreach (var file in files)
            {
                // Create the path to the new copy of the file.
                var temppath = Path.Combine(pastadeDestino, file.Name);

                // Copy the file.
                file.CopyTo(temppath, true);
            }

            // If copySubDirs is true, copy the subdirectories.
            if (!copiarSubDiretorios) return;

            foreach (var subdir in dirs)
            {
                // Create the subdirectory.
                var temppath = Path.Combine(pastadeDestino, subdir.Name);

                // Copy the subdirectories.
                PastaCopiarPasta(subdir.FullName, temppath, copiarSubDiretorios);

            }
        }

        public static bool PastaProcessXcopy(string SolutionDirectory, string TargetDirectory)
        {
            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            //Give the name as Xcopy
            startInfo.FileName = "xcopy";
            //make the window Hidden
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //Send the Source and destination as Arguments to the process
            startInfo.Arguments = "\"" + SolutionDirectory + "\"" + " " + "\"" + TargetDirectory + "\"" + @" /e /y /I /F";
            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
