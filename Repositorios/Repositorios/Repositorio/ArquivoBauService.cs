using Infra;
using Infra.Entidades;
using Infra.EntidadesNaoPersistidas;
using Repositorios.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Utils;

namespace Repositorios.Repositorio
{
    public class ArquivoBauService : IArquivoBauService
    {
        Class1 db;
        public void AbreConexao()
        {
            db = new Class1(true);
        }



        #region CENARIO ARQUIVOS

        public List<ArquivosLinhaCobranca> RetornoArquivos()
        {
            AbreConexao();

            List<ArquivosLinhaCobranca> Lista = new List<ArquivosLinhaCobranca>();

            foreach (var linha in db.LinhaCobranca)
            {
                var ListaBoleto = linha._Boleto;
                foreach (var boleto in ListaBoleto)
                {
                    ArquivosLinhaCobranca arquivo = new ArquivosLinhaCobranca { CenarioArquivoId = boleto.BoletoId, CenarioId = 1, cliente = boleto.SacadoNome, cnpjCpf = boleto.CnpJCpfSacado, dataCriacao = boleto.DataCriacao, Observacao = boleto.Observacao, NumeroDocumento = boleto.NumeroDocumento, Valor = boleto.ValorBoleto, Vencimento = boleto.DataVencimentoOriginal.Day, Prestacao = String.Format("{0}/{1}", linha.MesCompetencia, linha.AnoCompetencia)+" - "+ String.Format("{0}/{1}", boleto.DataVencimentoOriginal.Month, boleto.DataVencimentoOriginal.Year), tipoArquivo = "BL" };
                    Lista.Add(arquivo);
                }
                var ListaNotas = linha._NotaFiscal.Where(x => x.TipoImposto == "DEBITO" || x.TipoImposto == "ICMS_FOR" || x.TipoImposto == "ISS");
                foreach (var nota in ListaNotas)
                {
                    var infCliente = linha._Cliente;
                    ArquivosLinhaCobranca arquivo = new ArquivosLinhaCobranca { CenarioArquivoId = nota.NotaFiscalId, CenarioId = 1, cliente = infCliente.RazaoSocial, cnpjCpf = new Utils.Formatacao().FormataCnpjCpf(infCliente.CpfCnpj), dataCriacao = nota.DataEmissao, Observacao = nota.Observacao, NumeroDocumento = nota.NumeroRps.ToString(), Valor = nota.ValorServicos, Vencimento = nota.DataVencimento.Day, Prestacao = String.Format("{0}/{1}", linha.MesCompetencia, linha.AnoCompetencia) + " - " + String.Format("{0}/{1}", nota.DataVencimento.Month, nota.DataVencimento.Year), tipoArquivo = nota.TipoImposto };
                    Lista.Add(arquivo);
                }

            }

            return Lista;
        }


        #endregion



        public List<Bau> RetornaBau()
        {

            AbreConexao();
            List<Bau> lista = new List<Bau>();

            var Remessa = db.BoletoRemessa.ToList();
            var Lote = db.NotaFiscalLote.Where(x=>x.TipoImposto != "ICMS_FOR" && x.TipoImposto != "DEBITO").ToList();
         

            foreach (var item in Remessa)
            {
                lista.Add(new Bau { BauId = item.BoletoRemessaId, TipoArquivo = "Remessa", Cenario = "TESTE", CNPJ = item.Cnpj, Nome = item.Cedente, Valor = item.ValorRemessa, Usuario = "Usuario", Lote = item.Lote, DataSolicitada = item.DataCadastro, Detalhes = String.Format("Gerados {0} Boleto(s)", item.Boletos) });
            }

            foreach (var item in Lote)
            {
                lista.Add(new Bau { BauId = item.NotaFiscalLoteId, TipoArquivo = "NF", Cenario = "TESTE", CNPJ = item._Empresa.Cnpj, Nome = item._Empresa.RazaoSocial, Valor = item.ValorNota, Usuario = "Usuario", Lote = item.NumeroLote.ToString(), DataSolicitada = item.DataCriacao, Detalhes = String.Format("Gerados {0} RPS(s)", item._NotaFiscal.Count()) });
            }


            return lista;

        }


        public byte[] CriaRemessa(FormCollection formulario)
        {
            var db = new Class1(true);
            string[] formCollec = formulario["loteIds"].Split(',');
            List<BoletoRemessa> lstR = new List<BoletoRemessa>();
            for (int i = 1; i < formCollec.Count(); i++)
            {
                try
                {
                    int iDD = Convert.ToInt32(formCollec[i].ToString());
                    lstR.Add(db.BoletoRemessa.First(x => x.BoletoRemessaId == iDD));
                }
                catch
                {


                }

            }
            byte[] fileBytes = System.IO.File.ReadAllBytes(_utilPasta.ArquivoCriaDeStringOrgaRemessa(lstR));

            return fileBytes;
        }

        private Utils.ultArquivosPastas _utilPasta = new Utils.ultArquivosPastas();

        public bool CriaNfse(FormCollection formulario)
        {
            var db = new Class1(true);
            try
            {
                string[] formCollec = formulario["Loteids"].Split(',');

                List<NotaFiscalLote> Lista = new List<NotaFiscalLote>();
                for (int i = 1; i < formCollec.Length; i++)
                {
                    try
                    {
                        int Codigo = Convert.ToInt32(formCollec[i]);
                        NotaFiscalLote _nota = new NotaFiscalLote();
                        _nota = db.NotaFiscalLote.First(x => x.NotaFiscalLoteId == Codigo);
                        Lista.Add(_nota);
                    }
                    catch
                    {


                    }


                }
                foreach (var item in Lista)
                {

                    EmpresaEndereco _empresa = new EmpresaEndereco();
                    _empresa = db.EmpresaEndereco.First(c => c.EmpresaId == item.EmpresaId);

                    if (_empresa._Bairro._Cidade._Estado.Estadosigla == "MG")
                    {
                        NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvio envio = new NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvio();
                        envio = Newtonsoft.Json.JsonConvert.DeserializeObject<NFSE.Net.Layouts.BHISS.EnviarLoteRpsEnvio>(Encoding.ASCII.GetString(item.CorpoDocumento));
                        new CNAB400.NFSe().processaNF(envio, item.Cenario, item.Plano, item.NumeroLote);
                    }
                    if (_empresa._Bairro._Cidade._Estado.Estadosigla == "RJ")
                    {
                        NFSE.Net.Layouts.Carioca.EnviarLoteRpsEnvio envio = new NFSE.Net.Layouts.Carioca.EnviarLoteRpsEnvio();
                        envio = Newtonsoft.Json.JsonConvert.DeserializeObject<NFSE.Net.Layouts.Carioca.EnviarLoteRpsEnvio>(Encoding.ASCII.GetString(item.CorpoDocumento));
                        new CNAB400.NFSe().processaNFCarioca(envio, item.Cenario, item.Plano, item.NumeroLote);
                    }
                    if (_empresa._Bairro._Cidade._Estado.Estadosigla == "SP")
                    {
                        NFSE.Net.Layouts.Paulista.PedidoEnvioLoteRPS envio = new NFSE.Net.Layouts.Paulista.PedidoEnvioLoteRPS();
                        envio = Newtonsoft.Json.JsonConvert.DeserializeObject<NFSE.Net.Layouts.Paulista.PedidoEnvioLoteRPS>(Encoding.ASCII.GetString(item.CorpoDocumento));
                        new CNAB400.NFSe().processaNFPaulista(envio, item.Cenario, item.Plano, item.NumeroLote);
                    }


                }
                return true;
            }
            catch
            {

                return false;
            }
        }

        public void ConsultaNF(FormCollection formulario)
        {
            string[] formCollec = formulario["Loteids"].Split(',');
            List<NotaFiscalLote> Lista = new List<NotaFiscalLote>();
            var db = new Class1(true);

            for (int i = 0; i < formCollec.Length; i++)
            {
                try
                {
                    int Codigo = Convert.ToInt32(formCollec[i]);
                    
                    NotaFiscalLote _nota = new NotaFiscalLote();
                    _nota = db.NotaFiscalLote.First(x => x.NotaFiscalLoteId == Codigo);
                    Lista.Add(_nota);
                }
                catch
                {


                }


            }
            foreach (var item in Lista)
            {
                try
                {
                    var consultaSituacaoLote = new NFSE.Net.Layouts.Betha.ConsultarLoteRpsEnvio();
                    consultaSituacaoLote.Prestador = new NFSE.Net.Layouts.Betha.tcIdentificacaoPrestador();
                    consultaSituacaoLote.Prestador.Cnpj = db.Empresa.First(x => x.EmpresaId == item.EmpresaId).Cnpj;
                    consultaSituacaoLote.Prestador.InscricaoMunicipal = db.Empresa.First(x => x.EmpresaId == item.EmpresaId).InscMunicipal;
                    consultaSituacaoLote.Protocolo = item.Protocolo;

                    new CNAB400.NFSe().ConsultaNota(consultaSituacaoLote);
                }
                catch
                {

                    var consultaSituacaoLote = new NFSE.Net.Layouts.Carioca.ConsultarLoteRpsEnvio();
                    consultaSituacaoLote.Prestador = new NFSE.Net.Layouts.Carioca.ConsultarLoteRpsEnvioPrestador();
                    consultaSituacaoLote.Prestador.Cnpj = db.Empresa.First(x => x.EmpresaId == item.EmpresaId).Cnpj;
                    consultaSituacaoLote.Prestador.InscricaoMunicipal = db.Empresa.First(x => x.EmpresaId == item.EmpresaId).InscMunicipal;
                    consultaSituacaoLote.Protocolo = item.Protocolo;

                    new CNAB400.NFSe().ConsultaNota(consultaSituacaoLote);
                }

            }

        }

    }
}
