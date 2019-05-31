using Impactro.Cobranca;
using Impactro.Layout;
using Infra;
using Infra.Entidades;
using Infra.EntidadesNaoPersistidas;
using Microsoft.EntityFrameworkCore;
using Repositorios.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Infra.EntidadesNaoPersistidas.CenarioDetalhes;
using static Utils.CNAB400;

namespace Repositorios.Repositorio
{
    public class CenarioService : ICenarioService
    {
        Class1 db;
        public AcessoUsuario _Acesso;
        Utils.Calculos _Calculos = new Utils.Calculos();
        #region METODOS

        /// <summary>
        /// 
        /// </summary>
        void AbreConexao()
        {
            db = new Class1(true);
        }
        /// <summary>
        /// 
        /// </summary>
        void FechaConexao()
        {
            db.Database.CloseConnection();
        }
        
        #endregion

        #region API

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SumarioDados ApiSumarioDados()
        {
            SumarioDados sumario = new SumarioDados();

            try
            {
                int mes = Convert.ToInt32(DateTime.Now.Month - 1);
                DateTime Parameto = DateTime.Now.AddMonths(-1);
                db = new Class1(true);
                var LinhaCobranca = db.LinhaCobranca.Where(x => x.Tipo == "Dados");
                var Contratos = db.Contrato.Where(x =>
                x._Produto.Tipo == "Dados" && x.ContratoStatusId == 5 && x.Ativo == true && x._MapaFaturamento.FirstOrDefault().PlanoId != 174 && x._MapaFaturamento.FirstOrDefault(f => f.PlanoId != 174).Vencimento > 0 && x.DataInstalacao < Parameto
                ||
                x._Produto.Tipo == "Dados" && x.ContratoStatusId == 3 && x.Ativo == true && x._MapaFaturamento.FirstOrDefault().PlanoId != 174 && x._MapaFaturamento.FirstOrDefault(f => f.PlanoId != 174).Vencimento > 0 && x.DataAlteracao.Month == mes
                ||
                x.ContratoStatusId == 9 && x.Ativo == true && x._MapaFaturamento.FirstOrDefault().PlanoId != 174 && x._MapaFaturamento.FirstOrDefault(f => f.PlanoId != 174).Vencimento > 0);
                sumario.Codigo = LinhaCobranca.First().Tipo;
                sumario.QtdFaturado = LinhaCobranca.Count();
                sumario.Totalfaturado = LinhaCobranca.Sum(x => x.ValorFaturado);
                sumario.TotalSemfatura = (Contratos.Sum(x => x.Valor) - LinhaCobranca.Sum(x => x.ValorFaturado));
                sumario.QtdSemFatura = (Contratos.Count() - LinhaCobranca.Count());
            }
            catch
            {

                sumario.Codigo = "Dados";
                sumario.QtdFaturado = 0;
                sumario.Totalfaturado = 0;
                sumario.TotalSemfatura = 0;
                sumario.QtdSemFatura = 0;
            }

           

            return sumario;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SumarioDados ApiSumarioVoz()
        {
            db = new Class1(true);
            DateTime Parameto = DateTime.Now.AddMonths(-1);
            IEnumerable<SumarioDados> corss =
                from t in
                db.Contrato.Where(x => x.ParceiroMvx == false && x.Ativo == true && x.ContratoStatusId == 5 && x._MapaFaturamento.FirstOrDefault().PlanoId != 174 && x._MapaFaturamento.FirstOrDefault(d => d.PlanoId != 174).Vencimento != 0 && x.DataInstalacao < Parameto && x._Produto.Tipo == "Voz")
                from m in db.MapaFaturamento.Where(x => x.PlanoId != 174)
                where m.ContratoId == t.ContratoId
                select new SumarioDados
                {
                    Codigo = t._Produto.Tipo,
                    Totalfaturado = m._ContratoMovimentoFiscal.Where(d => d.DataMovimentacao.Month == DateTime.Now.Month).Sum(x => x.ValorContrato),
                    QtdFaturado = m._ContratoMovimentoFiscal.Where(d => d.DataMovimentacao.Month == DateTime.Now.Month).Count(),
                    TotalSemfatura = m._ContratoMovimentoFiscal.Count(d => d.DataMovimentacao.Month != DateTime.Now.Month && d.DataMovimentacao.Year != DateTime.Now.Year) == 0 ? m._Contrato.Valor : 0,
                    QtdSemFatura = m._ContratoMovimentoFiscal.Count(d => d.DataMovimentacao.Month != DateTime.Now.Month && d.DataMovimentacao.Year != DateTime.Now.Year) == 0 ? 1 : 0,

                };
            List<CenarioDetalhes.SumarioDados> Lista = new List<SumarioDados>();
            Lista = corss.ToList();
            Lista[0].QtdEmAberto = Lista.Sum(x => x.QtdEmAberto);
            Lista[0].QtdFaturado = Lista.Sum(x => x.QtdFaturado);
            Lista[0].QtdSemFatura = Lista.Sum(x => x.QtdSemFatura);
            Lista[0].SaldoEmAberto = Lista.Sum(x => x.SaldoEmAberto);
            Lista[0].Totalfaturado = Lista.Sum(x => x.Totalfaturado);
            Lista[0].TotalSemfatura = Lista.Sum(x => x.TotalSemfatura);

            return Lista[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formulario"></param>
        /// <returns></returns>
        public string Faturar(string _Cenario, string[] _Contratos)
        {
            string[] ListaContratos = _Contratos;
            
            string Cenario = _Cenario;
            List<LinhaCobranca> LinhaCobranca = new List<LinhaCobranca>();
            HttpContext.Current.Session.Add("_Obs", "ND");
            HttpContext.Current.Session.Add("_Ano","");
            HttpContext.Current.Session.Add("_Mes","");
            try
            {
              if (Cenario == "Dados" || Cenario == "Grupo")
              {
                  var Contratos = RetornaContratos(ListaContratos);
                  var retorno = GerarBoletoRemessa(Contratos);
                  LinhaCobranca = GravaLinhaCobranca(Utils.Classes.ListaRemessa, "Dados");
                  ProcessaNF(retorno, "TESTE", LinhaCobranca);
                  GeraRemessaBoleto(Contratos, LinhaCobranca);
              }
              if (Cenario == "Vinculado")
              {
                  var Contratos = RetornaContratosVinculado(ListaContratos);
                  var retorno = GerarBoletoRemessa(Contratos);
                  LinhaCobranca = GravaLinhaCobranca(Utils.Classes.ListaRemessa, "NRC");
                  ProcessaNF(retorno, "TESTE", LinhaCobranca);
                  GeraRemessaBoleto(Contratos, LinhaCobranca);
              }
                
             
                GC.Collect();
                GC.WaitForPendingFinalizers();
                return String.Format("Contratos Faturados com sucesso.");
            }
            catch (Exception erro)
            {
                foreach (var item in LinhaCobranca)
                {
                    db.LinhaCobranca.Remove(item);
                    db.SaveChanges();
                }
                return erro.Message;
            }


        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContratoId"></param>
        /// <param name="Desconto"></param>
        public void ApiAplicarDesconto(int ContratoId, decimal Desconto)
        {
            AbreConexao();
            var Contrato = db.Contrato.FirstOrDefault(x => x.ContratoId == ContratoId);
            var MapaDeFaturamentoAtual = Contrato._MapaFaturamento.FirstOrDefault(x => x.PlanoId != 174);
            MapaDeFaturamentoAtual.Desconto = (double)Desconto;
            db.MapaFaturamento.Add(MapaDeFaturamentoAtual);
            db.SaveChanges();
        }
      
        #endregion

        #region EVENTOS

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formulario"></param>
        public void AplicarDesconto(FormCollection formulario)
        {
            AbreConexao();
            string[] ContratoIds = formulario["ContratosIds"].Split(',');
            double desconto = Convert.ToDouble(formulario["Desconto"]);
            foreach (var item in ContratoIds)
            {
                try
                {
                    int IdContrato = Convert.ToInt32(item);
                    var Contrato = db.Contrato.FirstOrDefault(x => x.ContratoId == IdContrato);
                    var MapaDeFaturamentoAtual = Contrato._MapaFaturamento.FirstOrDefault(x => x.PlanoId != 174);
                    MapaDeFaturamentoAtual.Desconto = desconto;
                    db.MapaFaturamento.Add(MapaDeFaturamentoAtual);
                    db.SaveChanges();

                }
                catch
                {


                }
            }
        }
     
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formulario"></param>
        public void AplicarMensalidade(FormCollection formulario)
        {
            AbreConexao();
            string[] ContratoIds = formulario["ContratosIds"].Split(',');
            double desconto = Convert.ToDouble(formulario["Desconto"]);
            foreach (var item in ContratoIds)
            {
                try
                {
                    int IdContrato = Convert.ToInt32(item);
                    var Contrato = db.Contrato.FirstOrDefault(x => x.ContratoId == IdContrato);
                    var MapaDeFaturamentoAtual = Contrato._MapaFaturamento.FirstOrDefault(x => x.PlanoId != 174);
                    Contrato.Valor = (decimal)desconto;
                    MapaDeFaturamentoAtual.Desconto = desconto;
                    db.MapaFaturamento.Add(MapaDeFaturamentoAtual);
                    db.SaveChanges();

                }
                catch
                {


                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formulario"></param>
        /// <returns></returns>
        public string ProcessarEvento(FormCollection formulario)
        {
            string[] ListaContratos = formulario["ContratosIds"].Split(',');
            string Evento = formulario["EventoId"];
            string Cenario = formulario["Cenario"];
            int Faturados = 0;
            int NaoFaturados = 0;
            List<LinhaCobranca> LinhaCobranca = new List<LinhaCobranca>();
            HttpContext.Current.Session.Add("_Obs", "ND");
            HttpContext.Current.Session.Add("_Ano", "");
            HttpContext.Current.Session.Add("_Mes", "");
            try
            {

                if (Evento == "13")
                {
                    string Vencimento = formulario["VencimentoId"];
                    Alterarvencimento(ListaContratos, Convert.ToInt32(Vencimento));
                    return String.Format(" Vencimento Alterado para: {0}",Vencimento);
                }
                if (Evento == "9")
                {
                    AplicarDesconto(formulario);
                }
                if (Evento == "11")
                {

                }
                if (Evento == "8")
                {
                    if (Cenario == "Dados" || Cenario == "Grupo")
                    {
                        var Contratos = RetornaContratos(ListaContratos);
                        var retorno = GerarBoletoRemessa(Contratos);
                        Faturados = retorno.Count(x => x.Vinculado == false);
                        NaoFaturados = (Contratos.Count() - Faturados);
                        LinhaCobranca = GravaLinhaCobranca(Utils.Classes.ListaRemessa, "Dados");
                        ProcessaNF(retorno, "TESTE", LinhaCobranca);
                        GeraRemessaBoleto(retorno, LinhaCobranca);
                    }
                    if (Cenario == "Vinculado")
                    {
                        var Contratos = RetornaContratosVinculado(ListaContratos);
                        var retorno = GerarBoletoRemessa(Contratos);
                        Faturados = retorno.Count(x => x.Vinculado == false);
                        NaoFaturados = (Contratos.Count() - Faturados);
                        LinhaCobranca = GravaLinhaCobranca(Utils.Classes.ListaRemessa, "NRC");
                        ProcessaNF(retorno, "TESTE", LinhaCobranca);
                        GeraRemessaBoleto(retorno, LinhaCobranca);
                    }
                }
                if (Evento == "10")
                {
                    return Demostrativo(formulario);
                }

                if (Evento == "2")
                {

                    if (Cenario == "Dados" || Cenario == "Grupo")
                    {
                        var Contratos = RetornaContratos(ListaContratos);
                        RefaturarContratos(Contratos);
                        var retorno = GerarBoletoRemessa(Contratos);
                        LinhaCobranca = GravaLinhaCobranca(Utils.Classes.ListaRemessa, "Dados");
                        ProcessaNF(retorno, "TESTE", LinhaCobranca);
                        GeraRemessaBoleto(Contratos, LinhaCobranca);
                    }
                    if (Cenario == "Vinculado")
                    {
                        var Contratos = RetornaContratosVinculado(ListaContratos);

                        var retorno = GerarBoletoRemessa(Contratos);
                        LinhaCobranca = GravaLinhaCobranca(Utils.Classes.ListaRemessa, "NRC");
                        ProcessaNF(retorno, "TESTE", LinhaCobranca);
                        GeraRemessaBoleto(Contratos, LinhaCobranca);
                    }
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                return String.Format(" Contratos Faturados: {0} Contratos não Faturados {1}", Faturados, NaoFaturados);
            }
            catch (Exception erro)
            {
                foreach (var item in LinhaCobranca)
                {
                    db.LinhaCobranca.Remove(item);
                    db.SaveChanges();
                }
                return erro.Message;
            }


        }
      
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContratoId"></param>
        /// <returns></returns>
        public MapaFaturamento RetornaMapa(int ContratoId)
        {
            var db = new Class1(true);

            return db.MapaFaturamento.First(x => x.ContratoId == ContratoId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TipoCenario"></param>
        /// <returns></returns>
        public List<Contrato> ContratoS(string TipoCenario)
        {
            int mes = Convert.ToInt32(DateTime.Now.Month - 1);
            DateTime Parameto = DateTime.Now.AddMonths(-1);
            Utils.Calculos _Calculos = new Utils.Calculos();
            db = new Class1(true);
            var contratos =
           db.Contrato.Where(x =>
           x._Produto.Tipo == TipoCenario && x.ContratoStatusId == 5 && x.Ativo == true && x._MapaFaturamento.FirstOrDefault().PlanoId != 174 && x._MapaFaturamento.FirstOrDefault(f => f.PlanoId != 174).Vencimento > 0 && x.DataInstalacao < Parameto
           ||
           x._Produto.Tipo == TipoCenario && x.ContratoStatusId == 3 && x.Ativo == true && x._MapaFaturamento.FirstOrDefault().PlanoId != 174 && x._MapaFaturamento.FirstOrDefault(f => f.PlanoId != 174).Vencimento > 0 && x.DataAlteracao.Month == mes
           ||
           x.ContratoStatusId == 9 && x.Ativo == true && x._MapaFaturamento.FirstOrDefault().PlanoId != 174 && x._MapaFaturamento.FirstOrDefault(f => f.PlanoId != 174).Vencimento > 0
           );
         

            return contratos.ToList();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TipoCenario"></param>
        /// <returns></returns>
        public List<CenarioDetalhes.Detalhes> Detalhes(string TipoCenario)
        {
            AbreConexao();
           
            Utils.Calculos _Calculos = new Utils.Calculos();
            db = new Class1(true
                );
            IEnumerable<Detalhes> corss =
            from t in
            db.Contrato.Where(x =>
            x.ContratoStatusId == 5 && x.DataInstalacao.Month <= DateTime.Now.AddMonths(-1).Month
            ||
             x.ContratoStatusId == 3 && x.Ativo == true && x.DataAlteracao.Month == DateTime.Now.AddMonths(-1).Month
            ||
            x.ContratoStatusId == 8 && x.Ativo == true 
         )
            from m in db.MapaFaturamento.Where(x=>x.ContratoId == t.ContratoId && x.PlanoId != 174 && x.Vencimento >0).OrderByDescending(i => i.MapaFaturamentoId).Take(1)

            select new Detalhes
            {
                ContratoId = t.ContratoId,
                StatusId = t.ContratoStatusId,
                Codigo = t.Codigo,
                //Cidade = e._Bairro._Cidade.Nome,
                //UF = e._Bairro._Cidade._Estado.Estadosigla,
                CnpjCpf = t._Cliente.CpfCnpj,
                DataInstalacao = t.DataInstalacao,
                NomeRazao = t._Cliente.RazaoSocial,
                Produto = t._Produto.Nome,
                ValorMensalidade = t.Valor,
                PossuiRc = t._ContratoNrc.Count(x => x._Produto.Faturamento == "RC" && x.ProdutoId != 6) == 0 ? false : true,
                ValorTotalRc = t._ContratoNrc.Where(x => x._Produto.Faturamento == "RC" && x.ProdutoId != 6).Sum(x => x.Valor),
                DataStatus = t.DataAlteracao,
                TipoCliente = m._TipoClienteFaturamento == null ? "Não Definido" : m._TipoClienteFaturamento.Descricao,
                Vencimento = m.Vencimento,
                TipoClienteId = m._TipoClienteFaturamento == null ? 0 : m.TipoClienteFaturamentoId,
                StatusCenario = m._ContratoMovimentoFiscal.Count() == 0 ? 1 : 2,
                StatusContrato = m._Contrato.DataCriacao.Month == DateTime.Now.Month ? 3 : 0,
                PlanoId = m.PlanoId,
                Plano = m._Plano.PlanoNome,
                RenovacaoAutomatica = m.RenovacaoAutomatica == true ? "SIM" : "NÃO",
                Desconto = m.Desconto,
                Historico = t._ContratoHistorico.Count(x => x.Campo == "Valor") > 0 ? true : false,
                CodigoMundiData = t._ContratoRelacionamento.FirstOrDefault().DadosBabelId.ToString()+"-"+ t._ContratoRelacionamento.FirstOrDefault().CodPar.ToString(),
                CodPar = t._ContratoRelacionamento.FirstOrDefault().CodPar.ToString(),
                VencimentoCorrente = m.MesParcelas,
                TipoPagamento = m.Tipo == 1 ? "Débito Automatico" : m.Tipo == 2 ? "Depósito" : "Boleto",
                VencidoVencimento = m.MesParcelas == 1 ? "Vencimento" : m.MesParcelas == 2 ? "Vencido" : "Não Definido",
                Competencia = new Utils.retornos().RetornaPrestacao(m.MesParcelas).Month+"/" + new Utils.retornos().RetornaPrestacao(m.MesParcelas).Year+" - "+ new Utils.retornos().RetornaVencimento(m.MesParcelas).Month + "/" + new Utils.retornos().RetornaVencimento(m.MesParcelas).Year,
            };

            List<CenarioDetalhes.Detalhes> Lista = new List<Detalhes>();
            Lista = corss.ToList();

            foreach (var item in Lista)
            {
               
                if (item.StatusId == 5)
                {
                    if (Convert.ToDateTime(item.DataInstalacao).Month == Convert.ToInt32(DateTime.Now.Month - 1) && Convert.ToDateTime(item.DataInstalacao).Year == DateTime.Now.Year)
                    {
                        item.Prorrata = true;
                        item.TipoDoFaturamento = "Pro-rata";
                    }
                    item.TipoDoFaturamento = "Normal";
                }
                if (item.StatusId == 3)
                {

                    item.Prorrata = true;
                    item.TipoDoFaturamento = "Cancelado";

                }
                if (item.StatusId == 9)
                {

                    if (item.Historico)
                    {
                        item.TipoDoFaturamento = "Upgrade (Executado)";
                    }
                    if (!item.Historico)
                    {
                        item.TipoDoFaturamento = "Upgrade (não executado)";
                    }

                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            return Lista.OrderBy(x => x.Vencimento).ToList();
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TipoCenario"></param>
        /// <returns></returns>
        public List<CenarioDetalhes.Detalhes> DetalhesVinculado(string TipoCenario)
        {
            AbreConexao();
            DateTime Parameto = DateTime.Now.AddMonths(-1);
            Utils.Calculos _Calculos = new Utils.Calculos();

            IEnumerable<Detalhes> corss =
                from t in
                db.ContratoNrc.Where(x => x.ContratoId != null && x.Ativo == true && x._Produto.Faturamento == "NRC" && x.Faturado == true && x.DataInstalacao < Parameto && x._MapaFaturamento.Count(c => c.PlanoId != 174) > 0).AsNoTracking()
                from m in 
                db.MapaFaturamento.Where(x=>x.PlanoId != 174)
                where m.ContratoId == t.ContratoId
                select new Detalhes
                {
                    ContratoId = t.ContratoNrcId,
                    Codigo = t._Contrato.Codigo,
                    StatusId = t._Contrato.ContratoStatusId,
                    //Cidade = t._Contrato._InstalacaoEndereco.FirstOrDefault(x => x.TipoLogradouroId == 1)._Bairro._Cidade.Nome,
                    //UF = t._Contrato._InstalacaoEndereco.FirstOrDefault(x => x.TipoLogradouroId == 1)._Bairro._Cidade._Estado.Estadosigla,
                    CnpjCpf = t._Contrato._Cliente.CpfCnpj,
                    DataInstalacao = t.DataInstalacao,
                    Desconto = m.Desconto,
                    NomeRazao = t._Contrato._Cliente.RazaoSocial,
                    Produto = t._Produto.Nome,
                    ValorMensalidade = t.Valor,
                    StatusCenario = t.Faturado == true ? 2 : 1,
                    Plano = m._Plano.PlanoNome,
                    TipoCliente = m._TipoClienteFaturamento == null ? "Não Definido" : m._TipoClienteFaturamento.Descricao,
                    Vencimento = m.Vencimento,
                    TipoClienteId = m.TipoClienteFaturamentoId,
                    StatusContrato = t._Contrato.DataCriacao.Month == DateTime.Now.Month ? 3 : 0,
                    PlanoId = m.PlanoId,
                    RenovacaoAutomatica = m.RenovacaoAutomatica == true ? "SIM" : "NÃO",
                    Historico = t._Contrato._ContratoHistorico.Count(x => x.Campo == "Valor") > 0 ? true : false,
                    CodigoMundiData = t._Contrato._ContratoRelacionamento.FirstOrDefault().DadosBabelId.ToString(),
                    CodPar = t._Contrato._ContratoRelacionamento.FirstOrDefault().CodPar.ToString(),
                    VencimentoCorrente = m.MesParcelas,
                    TipoPagamento = m.Tipo == 1 ? "Débito Automatico" : m.Tipo == 2 ? "Depósito" : "Boleto",
                    VencidoVencimento = m.MesParcelas == 1 ? "Vencimento" : m.MesParcelas == 2 ? "Vencido" : "Não Definido",
                    Competencia = new Utils.retornos().RetornaPrestacao(m.MesParcelas).Month + "/" + new Utils.retornos().RetornaPrestacao(m.MesParcelas).Year + " - " + new Utils.retornos().RetornaVencimento(m.MesParcelas).Month + "/" + new Utils.retornos().RetornaVencimento(m.MesParcelas).Year,

                };
            List<CenarioDetalhes.Detalhes> Lista = new List<Detalhes>();
            Lista = corss.ToList();
            foreach (var item in Lista)
            {
                if (item.StatusId == 5)
                {
                    if (Convert.ToDateTime(item.DataInstalacao).Month == Convert.ToInt32(DateTime.Now.Month - 1) && Convert.ToDateTime(item.DataInstalacao).Year == DateTime.Now.Year)
                    {
                        item.Prorrata = true;
                        item.TipoDoFaturamento = "Pro-rata";
                    }
                    item.TipoDoFaturamento = "Normal";
                }
                if (item.StatusId == 3)
                {

                    item.Prorrata = true;
                    item.TipoDoFaturamento = "Cancelado";

                }
                if (item.StatusId == 9)
                {

                    if (item.Historico)
                    {
                        item.TipoDoFaturamento = "Upgrade (Executado)";
                    }
                    if (!item.Historico)
                    {
                        item.TipoDoFaturamento = "Upgrade (não executado)";
                    }

                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();

            return Lista.OrderBy(x => x.Vencimento).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Contrato> ListaContratosDados()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ContratoNrc> ListaContratosVinculados()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listaContratos"></param>
        /// <param name="Vencimento"></param>
        public void Alterarvencimento(string[] listaContratos, int Vencimento)
        {
            AbreConexao();
            foreach (var item in listaContratos)
            {
                try
                {
                    int Contratoid = Convert.ToInt32(item);
                    var Contrato = db.Contrato.First(x => x.ContratoId == Contratoid);
                    var Mapa = Contrato._MapaFaturamento.First();
                    Mapa.Vencimento = Vencimento;
                    db.MapaFaturamento.Update(Mapa);
                    db.SaveChanges();
                }
                catch
                {

                   
                }
            }



        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formulario"></param>
        public string Demostrativo(FormCollection formulario)
        {
            AbreConexao();
            string[] ContratoIds = formulario["ContratosIds"].Split(',');
            List<Domostrativo> lst = new List<Domostrativo>();

            try
            {
                foreach (var item in ContratoIds)
                {
                    try
                    {
                        int IdContrato = Convert.ToInt32(item);
                        var Contrato = db.Contrato.FirstOrDefault(x => x.ContratoId == IdContrato);
                        var pontaA = Contrato._InstalacaoEndereco.FirstOrDefault(x => x.TipoLogradouroId == 1);
                        var pontaB = Contrato._InstalacaoEndereco.Count(x => x.TipoLogradouroId == 4) > 0 ? Contrato._InstalacaoEndereco.FirstOrDefault(x => x.TipoLogradouroId == 4) : null;
                        lst.Add(new Domostrativo
                        {
                            Cnpj = new Utils.Formatacao().FormataCnpjCpf(Contrato._Cliente.CpfCnpj),
                            Codigo = Contrato._ContratoRelacionamento.FirstOrDefault().DadosBabelId.ToString(),
                            DataAtivacao = Contrato.DataInstalacao.ToShortDateString(),
                            Disponibilidade = "100.00%",
                            DisponibilidadeHs = "720.00",
                            NCircuito = "11",
                            NomeCircuito = Contrato.Alias,
                            PontaA = pontaA.Logradouro + " nº " + pontaA.Numero + " - " + pontaA.Complemento + " Andar " + pontaA.Andar + " " + pontaA._Bairro.Nome + " - " + pontaA._Bairro._Cidade._Estado.Estadosigla,
                            PontaB = pontaB == null ? "N/A" : pontaB.Logradouro + " nº " + pontaB.Numero + " - " + pontaB.Complemento + " Andar " + pontaB.Andar + " " + pontaB._Bairro.Nome + " - " + pontaB._Bairro._Cidade._Estado.Estadosigla,
                            QtdCircuito = "1",
                            //Servico = Contrato._Produto.Classe + " " + Contrato._ListaPropriedade(IdContrato).First(x => x.PropriedadeNome == "VELOCIDADE").ValorDigitado + "M",
                            Titulo = Contrato._Cliente.RazaoSocial,
                            ValorBruto = String.Format("{0:c}", Contrato.Valor),
                            ValorLiquido = String.Format("{0:c}", new Utils.Calculos().CalculoConfazSemContrato((int)Contrato._MapaFaturamento.First().TipoClienteFaturamentoId, Contrato.Valor, pontaA._Bairro._Cidade._Estado.Estadosigla, Contrato._MapaFaturamento.First().PlanoId)),
                            CTotBruto = Contrato.Valor,
                            CTotLiquido = new Utils.Calculos().CalculoConfazSemContrato((int)Contrato._MapaFaturamento.First().TipoClienteFaturamentoId, Contrato.Valor, pontaA._Bairro._Cidade._Estado.Estadosigla, Contrato._MapaFaturamento.First().PlanoId)



                        }
                        );


                    }
                    catch
                    {


                    }
                }
                foreach (var item in lst)
                {
                    item.TotalBruto = String.Format("{0:c}", lst.Sum(x => x.CTotBruto));
                    item.TotalLiquido = String.Format("{0:c}", lst.Sum(x => x.CTotLiquido));
                }
                string NomeArquivo = lst.First().Cnpj.Replace("CNPJ: ", "").Replace("CPF: ", "").Replace(".", "").Replace("/", "").Replace("-", "");
                db.CenarioArquivos.Add(
                    new CenarioArquivo
                    {
                        CenarioId = 1,
                        cliente = lst.First().Titulo,
                        cnpjCpf = lst.First().Cnpj,
                        responsavel = "TESTE",
                        plano = "TESTE",
                        dataCriacao = DateTime.Now,
                        NumeroDocumento = lst.First().Cnpj,
                        Valor = lst.Sum(x => x.CTotLiquido),
                        tipoArquivo = "Demonstrativo",
                        RemessaLote = NomeArquivo

                    }
                    );
                db.SaveChanges();
                new MVX.Print.frmDemonstrativo().Gera(lst);
                return "Arquivo Gerado e movido para ('Arquivos')";
            }
            catch (Exception erro)
            {

                throw new Exception("Houve Erro ao Gerar Demonstrativo.");
            }




        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ListaContrato"></param>
        public void GeraRemessaBoleto(List<ContratoNrc> ListaContrato, List<LinhaCobranca> ListaLinhaCobranca)
        {
            #region GERA REMESSA BOLETO

            Utils.Agrupado.ListaRemessaAgrupado = new List<Utils.Remessa>();

            int Counts = 1;

            foreach (var Contratos in Utils.Classes.ListaRemessa.GroupBy(x => new { x.Cedente.CNPJ, x.SacadoCleinte._SacadoInfo.Documento, x.SacadoCleinte._MapaFaturamento.Vencimento }))
            {

                SacadoInfo ced = new SacadoInfo();
                Utils.Remessa _cdd = new Utils.Remessa();
                _cdd.Cedente = new CedenteInfo();
                ced = Contratos.First().SacadoCleinte._SacadoInfo;
                _cdd.Cedente = new CedenteInfo();
                _cdd.Cedente = Contratos.First().Cedente;
                List<BoletoInfo> lst = new List<BoletoInfo>();
                BoletoInfo BoletoAgrupado = new BoletoInfo();
                BoletoAgrupado = new Utils.FaturamentoDeCenariosBoletos().RetornaBoletoRegra(db.BancoVencimento.FirstOrDefault(x => x._Banco.EmpresaId == db.Empresa.FirstOrDefault(f => f.RazaoSocial.Contains(_cdd.Cedente.Cedente)).EmpresaId)._Banco.NossoNumero, Contratos.Sum(c => c.SacadoCleinte.ListaBoleto.Sum(f => f.ValorDocumento)).ToString(), "2018/02/10", Counts, (decimal)Contratos.Sum(c => c.SacadoCleinte.ListaBoleto.Sum(f => f.ValorDocumento)), 0, DateTime.Now);
                lst.Add(BoletoAgrupado);
                _cdd.SacadoCleinte = new Utils.Sacado();
                _cdd.SacadoCleinte.ListaBoleto = new List<BoletoInfo>();
                _cdd.SacadoCleinte._SacadoInfo = Contratos.First().SacadoCleinte._SacadoInfo;
                _cdd.SacadoCleinte.ListaBoleto = lst;
                Utils.Agrupado.ListaRemessaAgrupado.Add(new Utils.Remessa { Cedente = _cdd.Cedente, SacadoCleinte = _cdd.SacadoCleinte });

                Counts++;
            }


            string Plano = "Planos Agrupados";

            foreach (var item in Utils.Agrupado.ListaRemessaAgrupado.GroupBy(c => c.Cedente.CNPJ))
            {
                int EmpId = 0;
                List<SacadoInfo> lstSacado = new List<SacadoInfo>();
                foreach (var saca in item)
                {
                    lstSacado.Add(saca.SacadoCleinte._SacadoInfo);
                }
                List<BoletoInfo> lstBoleto = new List<BoletoInfo>();
                foreach (var Bol in item)
                {
                    int Ccontagem = 1;

                    foreach (var _boleto in Bol.SacadoCleinte.ListaBoleto)
                    {
                        EmpId = db.Empresa.First(x => x.RazaoSocial.Contains(Bol.Cedente.Cedente)).EmpresaId;
                        BoletoInfo bole = new BoletoInfo();

                        bole = new Utils.FaturamentoDeCenariosBoletos().RetornaBoletoRegra(db.BancoVencimento.First(x => x._Banco.EmpresaId == EmpId)._Banco.NossoNumero, _boleto.ValorDocumento.ToString(), "1218/01/01", Ccontagem, (decimal)_boleto.ValorDocumento, 0, DateTime.Now);
                        lstBoleto.Add(bole);
                        int atu = Convert.ToInt32(db.BancoVencimento.First(x => x._Banco.EmpresaId == EmpId)._Banco.NossoNumero) + 1;
                        Banco bb = db.BancoVencimento.First(x => x._Banco.EmpresaId == EmpId)._Banco;
                        bb.NossoNumero = atu.ToString();
                        db.Banco.Update(bb);
                        db.SaveChanges();
                        Ccontagem++;
                    }

                }


                GeraRemessaLista(item.First().Cedente, lstSacado, lstBoleto, db.BancoVencimento.First(x => x._Banco.EmpresaId == EmpId)._Banco.NossoNumero, ListaContrato,ListaLinhaCobranca);

            }

            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ListaContratos"></param>
        public void RefaturarContratos(List<Contrato> ListaContratos)
        {

            foreach (var item in ListaContratos)
            {
                var Movimento = item._MapaFaturamento.First()._ContratoMovimentoFiscal.First(x => x.DataMovimentacao.Month == DateTime.Now.Month && x.DataMovimentacao.Year == DateTime.Now.Year);

                //Movimento.Refaturado = true;
                db.ContratoMovimentoFiscal.Update(Movimento);
                db.SaveChanges();
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Lista"></param>
        /// <returns></returns>
        public List<ContratoNrc> RetornaContratosVinculado(string[] Lista)
        {
            AbreConexao();
            List<ContratoNrc> ListaContratos = new List<ContratoNrc>();

            foreach (var item in Lista)
            {
                try
                {
                    int id = Convert.ToInt32(item);
                    var Contrato = db.ContratoNrc.First(x => x.ContratoNrcId == id);
                    if (ProrrataVinculado(Contrato.ContratoNrcId))
                    {
                        Contrato.Valor = ValorProrrataVinculado(Contrato.ContratoNrcId);
                    }
                    if (ProrrataCalceladoVinculado(Contrato.ContratoNrcId))
                    {
                        Contrato.Valor = ValorProrrataCalceladoVinculado(Contrato.ContratoNrcId);
                    }
                    if (!ProrrataVinculado(Contrato.ContratoNrcId))
                    {
                        Utils.Calculos _Calculos = new Utils.Calculos();
                        Contrato.Valor = _Calculos.CalculoConfazSemContrato((int)Contrato._Contrato._MapaFaturamento.First(x => x.PlanoId != 174).TipoClienteFaturamentoId, (Contrato.Valor - (decimal)Contrato._MapaFaturamento.First().Desconto), Contrato._Contrato._InstalacaoEndereco.First(x => x.TipoLogradouroId == 1)._Bairro._Cidade._Estado.Estadosigla, Contrato._MapaFaturamento.First().PlanoId);
                    }
                    ListaContratos.Add(Contrato);
                }
                catch(Exception erro)
                {


                }

            }

            return ListaContratos;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Lista"></param>
        /// <returns></returns>
        public List<Contrato> RetornaContratos(string[] Lista)
        {
            AbreConexao();
          
            List<Contrato> ListaContratos = new List<Contrato>();
            foreach (var item in Lista)
            {
                try
                {
                    int codigo = Convert.ToInt32(item);
                    var Contrato = db.Contrato.First(x => x.ContratoId == codigo);

                 
                        bool resposta = Prorrata(Contrato);

                        if (resposta)
                        {
                        Contrato.Valor = ValorProrrata(Contrato);
                        }

                        if (ProrrataCalcelado(Contrato))
                        {
                        Contrato.Valor = ValorProrrataCalcelado(Contrato);
                        }
                        if (!resposta)
                        {
                        Contrato.Valor = (Contrato.Valor - (decimal)Contrato._MapaFaturamento.First().Desconto);
                        }
                    

                    ListaContratos.Add(Contrato);
                }
                catch
                {


                }
            }
           

            

            return ListaContratos;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_lstContratos"></param>
        /// <returns></returns>
        public List<Utils.Nota> GerarBoletoRemessa(List<ContratoNrc> _lstContratos)
        {
            List<Utils.Nota> ListaNotas = new List<Utils.Nota>();
            try
            {

                int NossoNumeroCount = 1;
                Utils.Classes.ListaRemessa = new List<Utils.Remessa>();
                foreach (var rc in _lstContratos)
                {
                    foreach (PlanoEmpresa empresas in rc._MapaFaturamento.FirstOrDefault(d => d.PlanoId != 174)._Plano._PlanoEmpresa)
                    {
                        if (ProrrataVinculado(rc.ContratoNrcId))
                        {
                            rc.Valor = Math.Round(ValorProrrataVinculado(rc.ContratoNrcId), 2);
                        }
                        Utils.Remessa _Cedente = new Utils.Remessa();
                        var _EmpresaBR = db.Empresa.First(x => x.EmpresaId == empresas.EmpresaId);
                        int _vencimento = rc._MapaFaturamento.Last(d => d.PlanoId != 174).Vencimento;
                        var _BancoVencimento = db.BancoVencimento.First(x => x._Banco.EmpresaId == empresas.EmpresaId && x.Vecimento == _vencimento);

                        #region Cria o Cedente


                        _Cedente.Cedente = new CedenteInfo();
                        _Cedente.Cedente.Cedente = _EmpresaBR.RazaoSocial;
                        _Cedente.Cedente.CNPJ = _EmpresaBR.Cnpj;
                        _Cedente.Cedente.Layout = LayoutTipo.Auto;
                        _Cedente.Cedente.Banco = _BancoVencimento._Banco.BancoCodigo;
                        _Cedente.Cedente.Agencia = _BancoVencimento._Banco.Agencia;
                        _Cedente.Cedente.Conta = _BancoVencimento._Banco.Conta;
                        _Cedente.Cedente.Carteira = _BancoVencimento._Banco.Carteira;
                        _Cedente.Cedente.CedenteCOD = _BancoVencimento._Banco.CedenteCodigo;

                        #endregion
                        #region Variaveis de Comandos



                        var empre = db.Empresa.First(c => c.Cnpj == _Cedente.Cedente.CNPJ);
                        #endregion

                        double valorContato = new Utils.Calculos().CalculoPorcentagem(empresas.Receita, (double)(rc.Valor - (decimal)rc._MapaFaturamento.Last(x => x.PlanoId != 174).Desconto));
                        double valorPercentual = (double)new Utils.Calculos().CalculoConfazSemContrato((int)rc._Contrato._MapaFaturamento.FirstOrDefault(x => x.PlanoId != 174).TipoClienteFaturamentoId, (decimal)valorContato, rc._Contrato._InstalacaoEndereco.FirstOrDefault(x => x.TipoLogradouroId == 1)._Bairro._Cidade._Estado.Estadosigla, rc._Contrato._MapaFaturamento.FirstOrDefault(x => x.PlanoId != 174).PlanoId);
                        //double valorPercentual = new Utils.Calculos().CalculoPorcentagemComConfazBoleto(empresas.Receita, (double)rc.Valor, empresas.Imposto, (int)item._MapaFaturamento.FirstOrDefault(x => x.PlanoId != 174).TipoClienteFaturamentoId, item._InstalacaoEndereco.FirstOrDefault(x => x.TipoLogradouroId == 1)._Bairro._Cidade._Estado.Estadosigla);
                        BoletoInfo BoletoRegra = new BoletoInfo();

                        BoletoRegra = new Utils.FaturamentoDeCenariosBoletos().RetornaBoletoRegra(db.BancoVencimento.First(x => x._Banco.EmpresaId == empresas.EmpresaId)._Banco.NossoNumero, valorPercentual.ToString(), "2018/02/10", NossoNumeroCount, (decimal)valorPercentual, 0, DateTime.Now);

                        SacadoInfo ced = new SacadoInfo();

                        ced = new Utils.FaturamentoDeCenariosBoletos().RetornaSacado(rc._Contrato._Cliente);


                        _Cedente.SacadoCleinte = new Utils.Sacado();

                        _Cedente.SacadoCleinte._SacadoInfo = ced;
                        _Cedente.SacadoCleinte.ListaBoleto = new List<BoletoInfo>();
                        _Cedente.SacadoCleinte.ListaBoleto.Add(BoletoRegra);
                        _Cedente.SacadoCleinte._MapaFaturamento = new MapaFaturamento();
                        _Cedente.SacadoCleinte._MapaFaturamento = db.MapaFaturamento.First(x => x.ContratoNrcId == rc.ContratoNrcId && x.PlanoId != 174);


                        _Cedente.SacadoCleinte._PlanoEmpresa = db.PlanoEmpresa.First(x => x.PlanoId == _Cedente.SacadoCleinte._MapaFaturamento.PlanoId && x.EmpresaId == empre.EmpresaId && x.Imposto == empresas.Imposto);

                        Utils.Classes.ListaRemessa.Add(new Utils.Remessa { Cedente = _Cedente.Cedente, SacadoCleinte = _Cedente.SacadoCleinte });

                        ListaNotas.Add(new Utils.Nota { Contrato = rc._Produto.Nome, Cedente = _Cedente.Cedente.CNPJ, Sacado = _Cedente.SacadoCleinte._SacadoInfo.Documento, valorDocumento = (decimal)valorPercentual, Imposto = empresas.Imposto });
                    }

                }

                return ListaNotas;
            }
            catch (Exception erro)
            {


            }
            return ListaNotas;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_lstContratos"></param>
        /// <returns></returns>
        public List<Utils.Nota> GerarBoletoRemessa(List<Contrato> _lstContratos)
        {
            List<Utils.Nota> ListaNotas = new List<Utils.Nota>();
            try
            {
                int NossoNumeroCount = 1;
                Utils.Classes.ListaRemessa = new List<Utils.Remessa>();

                #region GERA BOLETO REMESSA DADOS

                foreach (var item in _lstContratos)
                {
                    Utils.Nota nn = new Utils.Nota();
                    try
                    {
                        foreach (PlanoEmpresa empresas in item._MapaFaturamento.FirstOrDefault(d => d.PlanoId != 174)._Plano._PlanoEmpresa)
                        {

                            Utils.Remessa _Cedente = new Utils.Remessa();
                            var _EmpresaBR = empresas._Empresa;
                            int _vencimento = item._MapaFaturamento.FirstOrDefault().Vencimento;
                            var _BancoVencimento = _EmpresaBR._Banco.First(x => x._BancoVencimento.Select(f => f.Vecimento).Contains(_vencimento))._BancoVencimento.First(x=>x.Vecimento == _vencimento);
                            double Rretencao = 0;

                            foreach (var ret in empresas._MapaFaturamentoComplemento.Where(x=>x.ContratoId == item.ContratoId))
                            {
                                Rretencao = (ret.R_Cofins + ret.R_CSLL + ret.R_INSS + ret.R_IRPJ + ret.R_Outros + ret.R_PIS + ret.R_Boleto);
                            }
                            #region Cria o Cedente


                            _Cedente.Cedente = new CedenteInfo();
                            _Cedente.Cedente.Cedente = _EmpresaBR.RazaoSocial;
                            _Cedente.Cedente.CNPJ = _EmpresaBR.Cnpj;
                            _Cedente.Cedente.Layout = LayoutTipo.Auto;
                            _Cedente.Cedente.Banco = _BancoVencimento._Banco.BancoCodigo;
                            _Cedente.Cedente.Agencia = _BancoVencimento._Banco.Agencia;
                            _Cedente.Cedente.Conta = _BancoVencimento._Banco.Conta;
                            _Cedente.Cedente.Carteira = _BancoVencimento._Banco.Carteira;
                            _Cedente.Cedente.CedenteCOD = _BancoVencimento._Banco.CedenteCodigo;

                            #endregion

                           
                        
                            #endregion

                            double valorPercentual = new Utils.Calculos().CalculoPorcentagemComConfazBoleto(empresas.Receita, (double)item.Valor, empresas.Imposto, (int)item._MapaFaturamento.FirstOrDefault(x => x.PlanoId != 174).TipoClienteFaturamentoId, item._InstalacaoEndereco.FirstOrDefault(x => x.TipoLogradouroId == 1)._Bairro._Cidade._Estado.Estadosigla);
                            valorPercentual = (valorPercentual - (new Utils.Calculos().CalculoPorcentagem(valorPercentual, Rretencao)));
                            BoletoInfo BoletoRegra = new BoletoInfo();

                            BoletoRegra = new Utils.FaturamentoDeCenariosBoletos().RetornaBoletoRegra(_BancoVencimento._Banco.NossoNumero, valorPercentual.ToString(), "2018/02/10", NossoNumeroCount, (decimal)valorPercentual, 0, DateTime.Now);

                            SacadoInfo ced = new SacadoInfo();

                            ced = new Utils.FaturamentoDeCenariosBoletos().RetornaSacado(item._Cliente);


                            _Cedente.SacadoCleinte = new Utils.Sacado();

                            _Cedente.SacadoCleinte._SacadoInfo = ced;
                            _Cedente.SacadoCleinte.ListaBoleto = new List<BoletoInfo>();
                            _Cedente.SacadoCleinte.ListaBoleto.Add(BoletoRegra);
                            _Cedente.SacadoCleinte._MapaFaturamento = new MapaFaturamento();
                            _Cedente.SacadoCleinte._MapaFaturamento = item._MapaFaturamento.First();
                            //_Cedente.SacadoCleinte._Retencao = item._MapaFaturamentoComplemento.ToList();
                           

                            _Cedente.SacadoCleinte._PlanoEmpresa = empresas;
                            nn = new Utils.Nota {Codigo = item.Codigo, ContratoId = item.ContratoId, Contrato = item._Produto.Nome, Cedente = _Cedente.Cedente.CNPJ, Sacado = _Cedente.SacadoCleinte._SacadoInfo.Documento, valorDocumento = (decimal)valorPercentual, Imposto = empresas.Imposto,Retencao = Rretencao };
                            ListaNotas.Add(nn);

                            Utils.Classes.ListaRemessa.Add(new Utils.Remessa { Cedente = _Cedente.Cedente, SacadoCleinte = _Cedente.SacadoCleinte });
                            string propriedade = string.Empty;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                    try
                    {
                        if (item._ContratoNrc.Count(x => x.ProdutoId != 6 && x.Faturado == false || x.ProdutoId == 7) > 0)
                        {
                            var ContratosRc = item._ContratoNrc.Where(x => x.ProdutoId != 6);

                            foreach (var rc in ContratosRc)
                            {
                                foreach (PlanoEmpresa empresas in rc._MapaFaturamento.FirstOrDefault(d => d.PlanoId != 174)._Plano._PlanoEmpresa)
                                {

                                    Utils.Remessa _Cedente = new Utils.Remessa();
                                    var _EmpresaBR = empresas._Empresa;
                                    int _vencimento = item._MapaFaturamento.First().Vencimento;
                                    var _BancoVencimento = _EmpresaBR._Banco.First(x => x._BancoVencimento.Select(f => f.Vecimento).Contains(_vencimento))._BancoVencimento.First(x => x.Vecimento == _vencimento);
                                    double Rretencao = 0;

                                    foreach (var ret in empresas._MapaFaturamentoComplemento.Where(x=>x.ContratoNrcId == rc.ContratoNrcId))
                                    {
                                        Rretencao = (ret.R_Cofins + ret.R_CSLL + ret.R_INSS + ret.R_IRPJ + ret.R_Outros + ret.R_PIS + ret.R_Boleto);
                                    }
                                    #region Cria o Cedente


                                    _Cedente.Cedente = new CedenteInfo();
                                    _Cedente.Cedente.Cedente = _EmpresaBR.RazaoSocial;
                                    _Cedente.Cedente.CNPJ = _EmpresaBR.Cnpj;
                                    _Cedente.Cedente.Layout = LayoutTipo.Auto;
                                    _Cedente.Cedente.Banco = _BancoVencimento._Banco.BancoCodigo;
                                    _Cedente.Cedente.Agencia = _BancoVencimento._Banco.Agencia;
                                    _Cedente.Cedente.Conta = _BancoVencimento._Banco.Conta;
                                    _Cedente.Cedente.Carteira = _BancoVencimento._Banco.Carteira;
                                    _Cedente.Cedente.CedenteCOD = _BancoVencimento._Banco.CedenteCodigo;

                                    #endregion

                                    #region Variaveis de Comandos



                                    var empre = _EmpresaBR;
                                    #endregion

                                    double valorContato = new Utils.Calculos().CalculoPorcentagem(empresas.Receita, (double)(rc.Valor - (decimal)rc._MapaFaturamento.Last(x => x.PlanoId != 174).Desconto));
                                    double valorPercentual = (double)valorContato;
                                    valorPercentual =  ((double)rc.Valor - (new Utils.Calculos().CalculoPorcentagem(valorPercentual, Rretencao)));
                                    BoletoInfo BoletoRegra = new BoletoInfo();

                                    BoletoRegra = new Utils.FaturamentoDeCenariosBoletos().RetornaBoletoRegra(_BancoVencimento._Banco.NossoNumero, valorPercentual.ToString(), "2018/02/10", NossoNumeroCount, (decimal)valorPercentual, 0, DateTime.Now);

                                    SacadoInfo ced = new SacadoInfo();

                                    ced = new Utils.FaturamentoDeCenariosBoletos().RetornaSacado(item._Cliente);


                                    _Cedente.SacadoCleinte = new Utils.Sacado();

                                    _Cedente.SacadoCleinte._SacadoInfo = ced;
                                    _Cedente.SacadoCleinte.ListaBoleto = new List<BoletoInfo>();
                                    _Cedente.SacadoCleinte.ListaBoleto.Add(BoletoRegra);
                                    _Cedente.SacadoCleinte._MapaFaturamento = new MapaFaturamento();
                                    _Cedente.SacadoCleinte._MapaFaturamento = item._MapaFaturamento.First();


                                    _Cedente.SacadoCleinte._PlanoEmpresa = empresas;


                                    ListaNotas.Add(new Utils.Nota {Codigo = rc._Contrato.Codigo, ContratoId = rc.ContratoNrcId, Contrato = rc._Produto.Nome, Cedente = _Cedente.Cedente.CNPJ, Sacado = _Cedente.SacadoCleinte._SacadoInfo.Documento, valorDocumento = (decimal)valorPercentual, Imposto = empresas.Imposto, Vinculado = true });
                                    Utils.Classes.ListaRemessa.Add(new Utils.Remessa { Cedente = _Cedente.Cedente, SacadoCleinte = _Cedente.SacadoCleinte });

                                }

                            }
                        }

                    }
                    catch
                    {

                        ListaNotas.Remove(nn);
                    }


                }

                
                

                return ListaNotas;
            }
            catch
            {

                throw new Exception(" Houve um erro ao reunir informações para faturarmento.");
            }

           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ListaPronta"></param>
        /// <param name="cenarioCOD"></param>
        public void ProcessaNF(List<Utils.Nota> ListaPronta, string cenarioCOD,List<LinhaCobranca> ListaLinhaCobranca)
        {

            foreach (var item in Utils.Classes.ListaRemessa.GroupBy(x => x.Cedente.CNPJ))
            {
                Empresa _empresa = new Empresa();
                string _cnpj = item.First().Cedente.CNPJ;
                string Plano = "TESTE";
                _empresa = db.Empresa.First(x => x.Cnpj == _cnpj);
                switch (db.EmpresaEndereco.First(x => x.EmpresaId == _empresa.EmpresaId)._Bairro._Cidade._Estado.Estadosigla)
                {
                    case "MG":
                        #region VOLTA PARA MG
                     
                        if (Utils.Classes.ListaRemessa.Where(x => x.Cedente.CNPJ == _cnpj && x.SacadoCleinte._PlanoEmpresa.Imposto == "ISS").ToList().Count() > 0)
                        {
                            new NFSe().DetalhesToObjeto(ListaPronta, Utils.Classes.ListaRemessa.Where(x => x.Cedente.CNPJ == _cnpj && x.SacadoCleinte._PlanoEmpresa.Imposto == "ISS").ToList(), cenarioCOD, Plano,ListaLinhaCobranca);
                           
                        }
                        if (Utils.Classes.ListaRemessa.Where(x => x.Cedente.CNPJ == _cnpj && x.SacadoCleinte._PlanoEmpresa.Imposto == "ICMS_ELE").ToList().Count() > 0)
                        {
                            new NFSe().ConstroiICMS(ListaPronta, Utils.Classes.ListaRemessa.Where(x => x.Cedente.CNPJ == _cnpj && x.SacadoCleinte._PlanoEmpresa.Imposto == "ICMS_ELE").ToList(), cenarioCOD, Plano, ListaLinhaCobranca);

                        }
                        if (Utils.Classes.ListaRemessa.Where(x => x.Cedente.CNPJ == _cnpj && x.SacadoCleinte._PlanoEmpresa.Imposto == "DEBITO").ToList().Count() > 0)
                        {
                            new NFSe().ConstroiDebito(ListaPronta, Utils.Classes.ListaRemessa.Where(x => x.Cedente.CNPJ == _cnpj && x.SacadoCleinte._PlanoEmpresa.Imposto == "DEBITO").ToList(), cenarioCOD, Plano, ListaLinhaCobranca);

                        }

                        #endregion
                        break;
                    case "RJ":
                        #region VOLTA PARA RJ

                        if (Utils.Classes.ListaRemessa.Where(x => x.Cedente.CNPJ == _cnpj && x.SacadoCleinte._PlanoEmpresa.Imposto == "ISS").ToList().Count() > 0)
                        {
                            new NFSe().NotaCarioca(ListaPronta, Utils.Classes.ListaRemessa.Where(x => x.Cedente.CNPJ == _cnpj && x.SacadoCleinte._PlanoEmpresa.Imposto == "ISS").ToList(), cenarioCOD, Plano, ListaLinhaCobranca);

                        }
                        if (Utils.Classes.ListaRemessa.Where(x => x.Cedente.CNPJ == _cnpj && x.SacadoCleinte._PlanoEmpresa.Imposto == "ICMS_ELE").ToList().Count() > 0)
                        {
                            new NFSe().ConstroiICMS(ListaPronta, Utils.Classes.ListaRemessa.Where(x => x.Cedente.CNPJ == _cnpj && x.SacadoCleinte._PlanoEmpresa.Imposto == "ICMS_ELE").ToList(), cenarioCOD, Plano, ListaLinhaCobranca);

                        }
                        if (Utils.Classes.ListaRemessa.Where(x => x.Cedente.CNPJ == _cnpj && x.SacadoCleinte._PlanoEmpresa.Imposto == "ICMS_FOR").ToList().Count() > 0)
                        {
                            new NFSe().ConstroiICMS(ListaPronta, Utils.Classes.ListaRemessa.Where(x => x.Cedente.CNPJ == _cnpj && x.SacadoCleinte._PlanoEmpresa.Imposto == "ICMS_FOR").ToList(), cenarioCOD, Plano, ListaLinhaCobranca);

                        }
                        if (Utils.Classes.ListaRemessa.Where(x => x.Cedente.CNPJ == _cnpj && x.SacadoCleinte._PlanoEmpresa.Imposto == "DEBITO").ToList().Count() > 0)
                        {
                            new NFSe().ConstroiDebito(ListaPronta, Utils.Classes.ListaRemessa.Where(x => x.Cedente.CNPJ == _cnpj && x.SacadoCleinte._PlanoEmpresa.Imposto == "DEBITO").ToList(), cenarioCOD, Plano, ListaLinhaCobranca);

                        }

                        #endregion
                        break;
                    case "SP":
                        #region VOLTA PARA SP

                        if (Utils.Classes.ListaRemessa.Where(x => x.Cedente.CNPJ == _cnpj && x.SacadoCleinte._PlanoEmpresa.Imposto == "ISS").ToList().Count() > 0)
                        {
                            new NFSe().NotaPaulista(ListaPronta, Utils.Classes.ListaRemessa.Where(x => x.Cedente.CNPJ == _cnpj && x.SacadoCleinte._PlanoEmpresa.Imposto == "ISS").ToList(), cenarioCOD, Plano);

                        }
                        if (Utils.Classes.ListaRemessa.Where(x => x.Cedente.CNPJ == _cnpj && x.SacadoCleinte._PlanoEmpresa.Imposto == "ICMS_ELE").ToList().Count() > 0)
                        {
                            new NFSe().ConstroiICMS(ListaPronta, Utils.Classes.ListaRemessa.Where(x => x.Cedente.CNPJ == _cnpj && x.SacadoCleinte._PlanoEmpresa.Imposto == "ICMS_ELE").ToList(), cenarioCOD, Plano, ListaLinhaCobranca);

                        }
                        if (Utils.Classes.ListaRemessa.Where(x => x.Cedente.CNPJ == _cnpj && x.SacadoCleinte._PlanoEmpresa.Imposto == "DEBITO").ToList().Count() > 0)
                        {
                            new NFSe().ConstroiDebito(ListaPronta, Utils.Classes.ListaRemessa.Where(x => x.Cedente.CNPJ == _cnpj && x.SacadoCleinte._PlanoEmpresa.Imposto == "DEBITO").ToList(), cenarioCOD, Plano, ListaLinhaCobranca);

                        }

                        #endregion
                        break;
                }
            }

        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ListaContrato"></param>

        public void GeraRemessaBoleto(List<Contrato> ListaContrato, List<LinhaCobranca> ListaLinhaCobranca)
        {
            #region GERA REMESSA BOLETO

            Utils.Agrupado.ListaRemessaAgrupado = new List<Utils.Remessa>();

            int Counts = 1;

            foreach (var Contratos in Utils.Classes.ListaRemessa.GroupBy(x => new { x.Cedente.CNPJ, x.SacadoCleinte._SacadoInfo.Documento, x.SacadoCleinte._MapaFaturamento.Vencimento }))
            {

                SacadoInfo ced = new SacadoInfo();
                Utils.Remessa _cdd = new Utils.Remessa();
                _cdd.Cedente = new CedenteInfo();
                ced = Contratos.First().SacadoCleinte._SacadoInfo;
                _cdd.Cedente = new CedenteInfo();
                _cdd.Cedente = Contratos.First().Cedente;
                List<BoletoInfo> lst = new List<BoletoInfo>();
                BoletoInfo BoletoAgrupado = new BoletoInfo();
                BoletoAgrupado = new Utils.FaturamentoDeCenariosBoletos().RetornaBoletoRegra(db.BancoVencimento.FirstOrDefault(x => x._Banco.EmpresaId == db.Empresa.FirstOrDefault(f => f.RazaoSocial.Contains(_cdd.Cedente.Cedente)).EmpresaId)._Banco.NossoNumero, Contratos.Sum(c => c.SacadoCleinte.ListaBoleto.Sum(f => f.ValorDocumento)).ToString(), "2018/02/10", Counts, (decimal)Contratos.Sum(c => c.SacadoCleinte.ListaBoleto.Sum(f => f.ValorDocumento)), 0, DateTime.Now);
                lst.Add(BoletoAgrupado);
                _cdd.SacadoCleinte = new Utils.Sacado();
                _cdd.SacadoCleinte.ListaBoleto = new List<BoletoInfo>();
                _cdd.SacadoCleinte._SacadoInfo = Contratos.First().SacadoCleinte._SacadoInfo;
                _cdd.SacadoCleinte.ListaBoleto = lst;
                Utils.Agrupado.ListaRemessaAgrupado.Add(new Utils.Remessa { Cedente = _cdd.Cedente, SacadoCleinte = _cdd.SacadoCleinte });

                Counts++;
            }


            string Plano = "Planos Agrupados";

            foreach (var item in Utils.Agrupado.ListaRemessaAgrupado.GroupBy(c => c.Cedente.CNPJ))
            {
                int EmpId = 0;
                List<SacadoInfo> lstSacado = new List<SacadoInfo>();
                foreach (var saca in item)
                {
                    lstSacado.Add(saca.SacadoCleinte._SacadoInfo);
                }
                List<BoletoInfo> lstBoleto = new List<BoletoInfo>();
                foreach (var Bol in item)
                {
                    int Ccontagem = 1;

                    foreach (var _boleto in Bol.SacadoCleinte.ListaBoleto)
                    {
                        EmpId = db.Empresa.First(x => x.RazaoSocial.Contains(Bol.Cedente.Cedente)).EmpresaId;
                        BoletoInfo bole = new BoletoInfo();

                        bole = new Utils.FaturamentoDeCenariosBoletos().RetornaBoletoRegra(db.BancoVencimento.First(x => x._Banco.EmpresaId == EmpId)._Banco.NossoNumero, _boleto.ValorDocumento.ToString(), "1218/01/01", Ccontagem, (decimal)_boleto.ValorDocumento, 0, DateTime.Now);
                        lstBoleto.Add(bole);
                        int atu = Convert.ToInt32(db.BancoVencimento.First(x => x._Banco.EmpresaId == EmpId)._Banco.NossoNumero) + 1;
                        Banco bb = db.BancoVencimento.First(x => x._Banco.EmpresaId == EmpId)._Banco;
                        bb.NossoNumero = atu.ToString();
                        db.Banco.Update(bb);
                        db.SaveChanges();
                        Ccontagem++;
                    }

                }


                GeraRemessaLista(item.First().Cedente, lstSacado, lstBoleto, db.BancoVencimento.First(x => x._Banco.EmpresaId == EmpId)._Banco.NossoNumero, ListaContrato,ListaLinhaCobranca);

            }

            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ListaContrato"></param>

        public void GeraRemessaBoleto(List<Utils.Nota> ListaContrato, List<LinhaCobranca> ListaLinhaCobranca)
        {
            #region GERA REMESSA BOLETO

            Utils.Agrupado.ListaRemessaAgrupado = new List<Utils.Remessa>();

            int Counts = 1;

            foreach (var Contratos in Utils.Classes.ListaRemessa.GroupBy(x => new { x.Cedente.CNPJ, x.SacadoCleinte._MapaFaturamento.Vencimento }))
            {
                foreach (var cliente in Contratos.GroupBy(x=> new { x.SacadoCleinte._SacadoInfo.Documento}))
                {
                    SacadoInfo ced = new SacadoInfo();
                    Utils.Remessa _cdd = new Utils.Remessa();
                    _cdd.Cedente = new CedenteInfo();
                    ced = cliente.First().SacadoCleinte._SacadoInfo;
                    _cdd.Cedente = new CedenteInfo();
                    _cdd.Cedente = cliente.First().Cedente;
                    List<BoletoInfo> lst = new List<BoletoInfo>();
                    BoletoInfo BoletoAgrupado = new BoletoInfo();

                    DateTime Vencimento = new DateTime();
                    DateTime Prestacao = new DateTime();
                    int Diavenci = cliente.First().SacadoCleinte._MapaFaturamento.Vencimento;
                    new Utils.retornos().RetornaPrestacao(cliente.First().SacadoCleinte._MapaFaturamento.MesParcelas, ref Prestacao, ref Vencimento);


                    BoletoAgrupado = new Utils.FaturamentoDeCenariosBoletos().RetornaBoletoRegra(db.BancoVencimento.FirstOrDefault(x => x._Banco.EmpresaId == db.Empresa.FirstOrDefault(f => f.RazaoSocial.Contains(_cdd.Cedente.Cedente)).EmpresaId)._Banco.NossoNumero, cliente.Sum(c => c.SacadoCleinte.ListaBoleto.Sum(f => f.ValorDocumento)).ToString(), String.Format(Diavenci+"/"+Vencimento.Month+"/"+Vencimento.Year), Counts, (decimal)cliente.Sum(c => c.SacadoCleinte.ListaBoleto.Sum(f => f.ValorDocumento)), 0, DateTime.Now);
                    lst.Add(BoletoAgrupado);
                    _cdd.SacadoCleinte = new Utils.Sacado();
                    _cdd.SacadoCleinte.ListaBoleto = new List<BoletoInfo>();
                    _cdd.SacadoCleinte._SacadoInfo = cliente.First().SacadoCleinte._SacadoInfo;
                    _cdd.SacadoCleinte.ListaBoleto = lst;
                    Utils.Agrupado.ListaRemessaAgrupado.Add(new Utils.Remessa { Cedente = _cdd.Cedente, SacadoCleinte = _cdd.SacadoCleinte });
                }

               

                Counts++;
            }


            string Plano = "Planos Agrupados";

            foreach (var item in Utils.Agrupado.ListaRemessaAgrupado.GroupBy(c => c.Cedente.CNPJ))
            {
                int EmpId = 0;
                List<SacadoInfo> lstSacado = new List<SacadoInfo>();
                foreach (var saca in item)
                {
                    lstSacado.Add(saca.SacadoCleinte._SacadoInfo);
                }
                List<BoletoInfo> lstBoleto = new List<BoletoInfo>();
                foreach (var Bol in item)
                {
                    int Ccontagem = 1;

                    foreach (var _boleto in Bol.SacadoCleinte.ListaBoleto)
                    {
                        EmpId = db.Empresa.First(x => x.RazaoSocial.Contains(Bol.Cedente.Cedente)).EmpresaId;
                        BoletoInfo bole = new BoletoInfo();

                        bole = new Utils.FaturamentoDeCenariosBoletos().RetornaBoletoRegra(db.BancoVencimento.First(x => x._Banco.EmpresaId == EmpId)._Banco.NossoNumero, _boleto.ValorDocumento.ToString(), _boleto.DataVencimento.ToShortDateString(), Ccontagem, (decimal)_boleto.ValorDocumento, 0, DateTime.Now);
                        lstBoleto.Add(bole);
                        int atu = Convert.ToInt32(db.BancoVencimento.First(x => x._Banco.EmpresaId == EmpId)._Banco.NossoNumero) + 1;
                        Banco bb = db.BancoVencimento.First(x => x._Banco.EmpresaId == EmpId)._Banco;
                        bb.NossoNumero = atu.ToString();
                        db.Banco.Update(bb);
                        db.SaveChanges();
                        Ccontagem++;
                    }

                }


                GeraRemessaLista(item.First().Cedente, lstSacado, lstBoleto, db.BancoVencimento.First(x => x._Banco.EmpresaId == EmpId)._Banco.NossoNumero, ListaContrato,ListaLinhaCobranca);

            }

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ListaContrato"></param>
        /// <param name="ListaLinhaCobranca"></param>
        public void GeraRemessaBoletoFatEsp(List<Utils.Nota> ListaContrato, List<LinhaCobranca> ListaLinhaCobranca, string MesCompetencia, string AnoCompetencia)
        {
            #region GERA REMESSA BOLETO

            Utils.Agrupado.ListaRemessaAgrupado = new List<Utils.Remessa>();
            string Vvencimento = string.Empty;
            int Counts = 1;

            foreach (var Contratos in Utils.Classes.ListaRemessa.GroupBy(x => new { x.Cedente.CNPJ, x.SacadoCleinte._MapaFaturamento.Vencimento}))
            {
                foreach (var cliente in Contratos.GroupBy(x => new { x.SacadoCleinte._SacadoInfo.Documento }))
                {
                    SacadoInfo ced = new SacadoInfo();
                    Utils.Remessa _cdd = new Utils.Remessa();
                    _cdd.Cedente = new CedenteInfo();
                    ced = cliente.First().SacadoCleinte._SacadoInfo;
                    _cdd.Cedente = new CedenteInfo();
                    _cdd.Cedente = cliente.First().Cedente;
                    List<BoletoInfo> lst = new List<BoletoInfo>();
                    BoletoInfo BoletoAgrupado = new BoletoInfo();
                    string dias = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month).ToString();
                   
                   
                        Vvencimento = cliente.First().SacadoCleinte._MapaFaturamento.Vencimento + "/" + MesCompetencia + "/" + AnoCompetencia;

                    
                    BoletoAgrupado = new Utils.FaturamentoDeCenariosBoletos().RetornaBoletoRegra(db.BancoVencimento.FirstOrDefault(x => x._Banco.EmpresaId == db.Empresa.FirstOrDefault(f => f.RazaoSocial.Contains(_cdd.Cedente.Cedente)).EmpresaId)._Banco.NossoNumero, cliente.Sum(c => c.SacadoCleinte.ListaBoleto.Sum(f => f.ValorDocumento)).ToString(), Vvencimento, Counts, (decimal)cliente.Sum(c => c.SacadoCleinte.ListaBoleto.Sum(f => f.ValorDocumento)), 0, DateTime.Now);
                    lst.Add(BoletoAgrupado);
                    _cdd.SacadoCleinte = new Utils.Sacado();
                    _cdd.SacadoCleinte.ListaBoleto = new List<BoletoInfo>();
                    _cdd.SacadoCleinte._SacadoInfo = cliente.First().SacadoCleinte._SacadoInfo;
                    _cdd.SacadoCleinte.ListaBoleto = lst;
                    Utils.Agrupado.ListaRemessaAgrupado.Add(new Utils.Remessa { Cedente = _cdd.Cedente, SacadoCleinte = _cdd.SacadoCleinte });
                }



                Counts++;
            }


            string Plano = "Planos Agrupados";

            foreach (var item in Utils.Agrupado.ListaRemessaAgrupado.GroupBy(c => c.Cedente.CNPJ))
            {
                int EmpId = 0;
                List<SacadoInfo> lstSacado = new List<SacadoInfo>();
                foreach (var saca in item)
                {
                    lstSacado.Add(saca.SacadoCleinte._SacadoInfo);
                }
                List<BoletoInfo> lstBoleto = new List<BoletoInfo>();
                foreach (var Bol in item)
                {
                    int Ccontagem = 1;

                    foreach (var _boleto in Bol.SacadoCleinte.ListaBoleto)
                    {
                        EmpId = db.Empresa.First(x => x.RazaoSocial.Contains(Bol.Cedente.Cedente)).EmpresaId;
                        BoletoInfo bole = new BoletoInfo();

                        bole = new Utils.FaturamentoDeCenariosBoletos().RetornaBoletoRegra(db.BancoVencimento.First(x => x._Banco.EmpresaId == EmpId)._Banco.NossoNumero, _boleto.ValorDocumento.ToString(), Vvencimento, Ccontagem, (decimal)_boleto.ValorDocumento, 0, DateTime.Now);
                        lstBoleto.Add(bole);
                        int atu = Convert.ToInt32(db.BancoVencimento.First(x => x._Banco.EmpresaId == EmpId)._Banco.NossoNumero) + 1;
                        Banco bb = db.BancoVencimento.First(x => x._Banco.EmpresaId == EmpId)._Banco;
                        bb.NossoNumero = atu.ToString();
                        db.Banco.Update(bb);
                        db.SaveChanges();
                        Ccontagem++;
                    }

                }


                GeraRemessaListaFatEsp(item.First().Cedente, lstSacado, lstBoleto, db.BancoVencimento.First(x => x._Banco.EmpresaId == EmpId)._Banco.NossoNumero, ListaContrato, ListaLinhaCobranca,MesCompetencia,AnoCompetencia);

            }

            #endregion
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContratoId"></param>
        /// <returns></returns>
        public bool Prorrata(Contrato Contrato)
        {
            try
            {
                if (Convert.ToDateTime(Contrato.DataInstalacao).Month == Convert.ToInt32(DateTime.Now.Month - 1) && Convert.ToDateTime(Contrato.DataInstalacao).Year == DateTime.Now.Year)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {

                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Cedente"></param>
        /// <param name="ListaSacado"></param>
        /// <param name="ListBoleto"></param>
        /// <param name="Lote"></param>
        /// <param name="ListaCOntrato"></param>
        public void GeraRemessaLista(CedenteInfo Cedente, List<SacadoInfo> ListaSacado, List<BoletoInfo> ListBoleto, string Lote, List<ContratoNrc> ListaCOntrato, List<LinhaCobranca> ListaLinhaCobranca)
        {
            LayoutBancos r = new LayoutBancos();
            r.Init(Cedente);
            r.Lote = CobUtil.GetInt(Lote);
            r.ShowDumpLine = false;

            for (int i = 0; i < ListaSacado.Count; i++)
            {
                ListBoleto[i].Instrucoes = string.Empty;
                r.Add(ListBoleto[i], ListaSacado[i]);
                string Arquivo = HttpContext.Current.Server.MapPath(@"~/App_Data/Remessa/CNAB400" + Lote + ".TXT");
                var Remessa = RegistraRemessa(Cedente.Cedente, Lote, Cedente.CNPJ, ListBoleto.Count(), (decimal)ListBoleto.Sum(x => x.ValorDocumento),Arquivo);
                new Utils.CNAB400.Boleto().GeraBoleto(Cedente, ListaSacado[i], ListBoleto[i], "TESTE", "TESTE", Lote, ListaLinhaCobranca, Remessa.BoletoRemessaId);
                r.RemessaTo(Arquivo);
            }

            try
            {
                foreach (var item in ListaCOntrato)
                {
                    if (db.ContratoMovimentoFiscal.Count(x => x.MapaFaturamentoId == item._Contrato._MapaFaturamento.First().MapaFaturamentoId && x.DataMovimentacao.Month == DateTime.Now.Month ) == 0)
                    {
                        string cnnpj = item._Contrato._Cliente.CpfCnpj;
                        var Cliente = db.cliente.First(x => x.CpfCnpj == cnnpj);
                        ContratoMovimentoFiscal movimento = new ContratoMovimentoFiscal();
                        movimento.ContratoMovimentoFiscalId = db.ContratoMovimentoFiscal.Count() + 1;
                        movimento.MapaFaturamentoId = db.Contrato.First(x => x.Codigo == item._Contrato.Codigo)._MapaFaturamento.First().MapaFaturamentoId;
                        movimento.ContratoCodigo = item._Contrato.Codigo;
                        movimento.DataMovimentacao = DateTime.Now;
                        movimento.Desconto = (decimal)item._MapaFaturamento.FirstOrDefault().Desconto;
                        movimento.ValorContrato = (item.Valor);
                        movimento.CpfCnpj = item._Contrato._Cliente.CpfCnpj;
                        movimento.LinhaCobrancaId = ListaLinhaCobranca.First(x => x.ClienteId == Cliente.ClienteId).LinhaCobrancaId;
                        db.ContratoMovimentoFiscal.Add(movimento);
                        db.SaveChanges();
                    }

                }
            }
            catch (Exception erro)
            {

            }
            try
            {
                Infra.Entidades.Bau bau = new Infra.Entidades.Bau();
                foreach (var item in ListBoleto)
                {

                    bau.Cenario = "TESTE";
                    try
                    {
                        bau.CNPJ = "CNPJ: " + @String.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToInt64(Cedente.CNPJ));
                    }
                    catch
                    {

                        bau.CNPJ = "CNPJ: " + @String.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToInt64(Cedente.CNPJ.Replace("/", "").Replace("-", "").Replace(".", "")));
                    }

                    bau.TipoBau = "Remessa";
                    bau.TipoArquivo = ListaCOntrato.Count() > 1 ? "Agrupado" : "Avulso";
                    bau.Remessa = "CNAB400" + Lote + ".TXT";
                    bau.CodigoBr = Cedente.Banco;
                    bau.Nome = Cedente.Cedente;
                    bau.Valor = (decimal)ListBoleto.Sum(x => x.ValorDocumento);
                    bau.SetorSolicitante = "TESTE";
                    bau.Usuario = "Teste";
                    bau.Status = "Não Processado";
                    //bau.Cidade = ListaDetalhesDados.First().Cidade;
                    bau.Detalhes = ListBoleto.Count().ToString();
                    bau.DataSolicitada = DateTime.Now;
                    bau.Plano = ListaCOntrato.Count() > 1 ? "Planos Agrupados" : "TESTE";
                    bau.DataSolicitada = DateTime.Now;
                    bau.ObsDocumento = ListBoleto.First().NumeroDocumento;

                }
                db.Bau.Add(bau);
                db.SaveChanges();
            }
            catch (Exception erro)
            {


            }
        }
        
        //AQUIIIIIIII
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ListaRemessa"></param>
        /// <param name="Tipo"></param>
        /// <returns></returns>
        public List<LinhaCobranca> GravaLinhaCobranca(List<Utils.Remessa> ListaRemessa, string Tipo)
        {
            List<LinhaCobranca> List = new List<LinhaCobranca>();

            foreach (var _nota in ListaRemessa.GroupBy(x => new { x.SacadoCleinte._SacadoInfo.Documento }))
            {
                Cliente _Cliente = new Cliente();
                string cn = _nota.First().SacadoCleinte._SacadoInfo.Documento;
                _Cliente = db.cliente.First(x => x.CpfCnpj.Contains(cn));
                var Contrato = _nota.First().SacadoCleinte._MapaFaturamento._Contrato;
                DateTime Vencimento = new DateTime();
                DateTime Prestacao = new DateTime();
                new Utils.retornos().RetornaPrestacao(Contrato._MapaFaturamento.First().MesParcelas, ref Prestacao, ref Vencimento);

                try
                {
                    
                    LinhaCobranca linhaCobranca = new LinhaCobranca();
                    linhaCobranca.CenarioId = 1;
                    linhaCobranca.ClienteId = _Cliente.ClienteId;
                    linhaCobranca.DataCadastro = DateTime.Now;
                    linhaCobranca.Tipo = Tipo;
                    linhaCobranca.MesCompetencia = Prestacao.Month.ToString();
                    linhaCobranca.AnoCompetencia = Prestacao.Year.ToString();
                    linhaCobranca.ValorFaturado = Math.Round(Convert.ToDecimal(_nota.Sum(x => x.SacadoCleinte.ListaBoleto.Sum(c => c.ValorDocumento))), 2);
                    db.LinhaCobranca.Add(linhaCobranca);
                    db.SaveChanges();
                    List.Add(linhaCobranca);
                }
                catch (Exception erro)
                {

                    
                }
            }

            return List;


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Cedente"></param>
        /// <param name="Lote"></param>
        /// <param name="Cnpj"></param>
        /// <param name="Boletos"></param>
        /// <param name="ValorArquivo"></param>
        /// <param name="Caminho"></param>
        /// <returns></returns>
        public BoletoRemessa RegistraRemessa(string Cedente,string Lote, string Cnpj, int Boletos,decimal ValorArquivo,string Caminho)
        {
            BoletoRemessa Remessa = new BoletoRemessa();
            if (db.BoletoRemessa.Count(x => x.Lote == Lote) == 0)
            {
              
                Remessa.Cedente = Cedente;
                Remessa.Cnpj = Cnpj;
                Remessa.Lote = Lote;
                Remessa.Boletos = Boletos;
                Remessa.ValorRemessa = ValorArquivo;
                Remessa.DataCadastro = DateTime.Now;
                Remessa.Arquivo = System.Text.Encoding.ASCII.GetBytes(ArquivoLer(Caminho));
                db.BoletoRemessa.Add(Remessa);
                db.SaveChanges();
            }
            return Remessa;
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="caminhoArquivo"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Cedente"></param>
        /// <param name="ListaSacado"></param>
        /// <param name="ListBoleto"></param>
        /// <param name="Lote"></param>
        /// <param name="ListaCOntrato"></param>
       
        public void GeraRemessaListaFatEsp(CedenteInfo Cedente, List<SacadoInfo> ListaSacado, List<BoletoInfo> ListBoleto, string Lote, List<Utils.Nota> ListaContrato, List<LinhaCobranca> ListaLinhaCobranca, string MesCompetencia, string AnoCompetencia)
        {
            LayoutBancos r = new LayoutBancos();
            r.Init(Cedente);
            r.Lote = CobUtil.GetInt(Lote);
            r.ShowDumpLine = false;
            
            for (int i = 0; i < ListaSacado.Count; i++)
            {
                ListBoleto[i].Instrucoes = string.Empty;
                r.Add(ListBoleto[i], ListaSacado[i]);
                string Arquivo = HttpContext.Current.Server.MapPath(@"~/App_Data/Remessa/CNAB400" + Lote + ".TXT");
                var Remessa = RegistraRemessa(Cedente.Cedente, Lote, Cedente.CNPJ, ListBoleto.Count(), (decimal)ListBoleto.Sum(x => x.ValorDocumento), Arquivo);
                new Utils.CNAB400.Boleto().GeraBoleto(Cedente, ListaSacado[i], ListBoleto[i], "TESTE", "TESTE", Lote, ListaLinhaCobranca, Remessa.BoletoRemessaId);
                r.RemessaTo(Arquivo);
            }

            try
            {
                foreach (var item in ListaContrato.Where(x => x.Vinculado == false))
                {
                    if (db.ContratoMovimentoFiscal.Count(x => x.ContratoCodigo == item.Contrato && x.DataMovimentacao.Month == DateTime.Now.Month) == 0)
                    {
                        using (var context = new Class1(true))
                        {

                            var Contrato = db.Contrato.First(x => x.ContratoId == item.ContratoId);
                            ContratoMovimentoFiscal movimento = new ContratoMovimentoFiscal();
                            movimento.ContratoMovimentoFiscalId = db.ContratoMovimentoFiscal.Count() + 1;
                            movimento.MapaFaturamentoId = Contrato._MapaFaturamento.First().MapaFaturamentoId;
                            movimento.ContratoCodigo = Contrato.Codigo;
                            movimento.DataMovimentacao = Convert.ToDateTime(DateTime.Now.Day+"/"+MesCompetencia+"/"+AnoCompetencia);
                            movimento.Desconto = (decimal)Contrato._MapaFaturamento.First().Desconto;
                            movimento.ValorContrato = (Contrato.Valor);
                            movimento.CpfCnpj = Contrato._Cliente.CpfCnpj;
                            int clienteId = Contrato._Cliente.ClienteId;
                            movimento.LinhaCobrancaId = ListaLinhaCobranca.First(x => x.ClienteId == clienteId).LinhaCobrancaId;
                            context.Entry(movimento).State = EntityState.Detached;
                            context.ContratoMovimentoFiscal.Add(movimento);
                            context.SaveChanges();
                        }
                    }

                }
            }
            catch (Exception erro)
            {

            }
            try
            {
                Bau bau = new Bau();
                foreach (var item in ListBoleto)
                {

                    bau.Cenario = "TESTE";
                    try
                    {
                        bau.CNPJ = "CNPJ: " + @String.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToInt64(Cedente.CNPJ));
                    }
                    catch
                    {

                        bau.CNPJ = "CNPJ: " + @String.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToInt64(Cedente.CNPJ.Replace("/", "").Replace("-", "").Replace(".", "")));
                    }

                    bau.TipoBau = "Remessa";
                    bau.TipoArquivo = ListaContrato.Count() > 1 ? "Agrupado " : "Avulso";
                    bau.Remessa = "CNAB400" + Lote + ".TXT";
                    bau.CodigoBr = Cedente.Banco;
                    bau.Nome = Cedente.Cedente;
                    bau.Valor = (decimal)ListBoleto.Sum(x => x.ValorDocumento);
                    bau.SetorSolicitante = "TESTE";
                    bau.Usuario = "Teste";
                    bau.Status = "Não Processado";
                    //bau.Cidade = ListaDetalhesDados.First().Cidade;
                    bau.Detalhes = ListBoleto.Count().ToString();
                    bau.DataSolicitada = DateTime.Now;
                    bau.Plano = ListaContrato.Count() > 1 ? "Planos Agrupados" : "TESTE";
                    bau.DataSolicitada = DateTime.Now;
                    bau.ObsDocumento = ListBoleto.First().NumeroDocumento;

                }
                db.Bau.Add(bau);
                db.SaveChanges();
            }
            catch (Exception erro)
            {


            }
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="Cedente"></param>
        /// <param name="ListaSacado"></param>
        /// <param name="ListBoleto"></param>
        /// <param name="Lote"></param>
        /// <param name="ListaContrato"></param>
        /// <param name="ListaLinhaCobranca"></param>

        public void GeraRemessaLista(CedenteInfo Cedente, List<SacadoInfo> ListaSacado, List<BoletoInfo> ListBoleto, string Lote, List<Utils.Nota> ListaContrato, List<LinhaCobranca> ListaLinhaCobranca)
        {
            LayoutBancos r = new LayoutBancos();
            r.Init(Cedente);
            r.Lote = CobUtil.GetInt(Lote);
            r.ShowDumpLine = false;
           
            for (int i = 0; i < ListaSacado.Count; i++)
            {
                ListBoleto[i].Instrucoes = string.Empty;
                r.Add(ListBoleto[i], ListaSacado[i]);
                string Arquivo = HttpContext.Current.Server.MapPath(@"~/App_Data/Remessa/CNAB400" + Lote + ".TXT");
                var Remessa = RegistraRemessa(Cedente.Cedente, Lote, Cedente.CNPJ, ListBoleto.Count(), (decimal)ListBoleto.Sum(x => x.ValorDocumento), Arquivo);
                new Utils.CNAB400.Boleto().GeraBoleto(Cedente, ListaSacado[i], ListBoleto[i], "TESTE", "TESTE", Lote, ListaLinhaCobranca, Remessa.BoletoRemessaId);
                r.RemessaTo(Arquivo);
             }

            try
            {
                foreach (var item in ListaContrato.Where(x=>x.Vinculado == false))
                {
                    if (db.ContratoMovimentoFiscal.Count(x => x.ContratoCodigo == item.Codigo && x.DataMovimentacao.Month == DateTime.Now.Month) == 0)
                    {

                        using (var context = new Class1(true))
                        {
                            var Contrato = db.Contrato.First(x => x.ContratoId == item.ContratoId);
                            ContratoMovimentoFiscal movimento = new ContratoMovimentoFiscal();
                            movimento.ContratoMovimentoFiscalId = db.ContratoMovimentoFiscal.Count() + 1;
                            movimento.MapaFaturamentoId = Contrato._MapaFaturamento.First().MapaFaturamentoId;
                            movimento.ContratoCodigo = Contrato.Codigo;
                            movimento.DataMovimentacao = DateTime.Now;
                            movimento.Desconto = (decimal)Contrato._MapaFaturamento.First().Desconto;
                            movimento.ValorContrato = (Contrato.Valor);
                            movimento.CpfCnpj = Contrato._Cliente.CpfCnpj;
                            int clienteId = Contrato._Cliente.ClienteId;
                            movimento.LinhaCobrancaId = ListaLinhaCobranca.First(x => x.ClienteId == clienteId).LinhaCobrancaId;
                            context.Entry(movimento).State = EntityState.Detached;
                            context.ContratoMovimentoFiscal.Add(movimento);
                            context.SaveChanges();

                            foreach(var itt in ListaContrato.Where(x=>x.ContratoId == Contrato.ContratoId && x.Vinculado == true))
                            {   
                                
                                ContratoMovimentoFiscal movimentov = new ContratoMovimentoFiscal();
                                movimentov.ContratoMovimentoFiscalId = db.ContratoMovimentoFiscal.Count() + 1;
                                movimentov.MapaFaturamentoId = Contrato._MapaFaturamento.First().MapaFaturamentoId;
                                movimentov.ContratoCodigo = Contrato.Codigo;
                                movimentov.DataMovimentacao = DateTime.Now;
                                movimentov.Desconto = (decimal)Contrato._MapaFaturamento.First().Desconto;
                                movimentov.ValorContrato = (itt.valorDocumento);
                                movimentov.CpfCnpj = Contrato._Cliente.CpfCnpj;
                                movimentov.LinhaCobrancaId = ListaLinhaCobranca.First(x => x.ClienteId == clienteId).LinhaCobrancaId;
                                context.Entry(movimentov).State = EntityState.Detached;
                                context.ContratoMovimentoFiscal.Add(movimentov);
                                context.SaveChanges();
                            }
                        }

                    }

                }
            }
            catch (Exception erro)
            {

            }
            try
            {
                Bau bau = new Bau();
                foreach (var item in ListBoleto)
                {

                    bau.Cenario = "TESTE";
                    try
                    {
                        bau.CNPJ = "CNPJ: " + @String.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToInt64(Cedente.CNPJ));
                    }
                    catch
                    {

                        bau.CNPJ = "CNPJ: " + @String.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToInt64(Cedente.CNPJ.Replace("/", "").Replace("-", "").Replace(".", "")));
                    }

                    bau.TipoBau = "Remessa";
                    bau.TipoArquivo = ListaContrato.Count() > 1 ? "Agrupado " : "Avulso";
                    bau.Remessa = "CNAB400" + Lote + ".TXT";
                    bau.CodigoBr = Cedente.Banco;
                    bau.Nome = Cedente.Cedente;
                    bau.Valor = (decimal)ListBoleto.Sum(x => x.ValorDocumento);
                    bau.SetorSolicitante = "TESTE";
                    bau.Usuario = "Teste";
                    bau.Status = "Não Processado";
                    bau.Detalhes = ListBoleto.Count().ToString();
                    bau.DataSolicitada = DateTime.Now;
                    bau.Plano = ListaContrato.Count() > 1 ? "Planos Agrupados" : "TESTE";
                    bau.DataSolicitada = DateTime.Now;
                    bau.ObsDocumento = ListBoleto.First().NumeroDocumento;

                }
                db.Bau.Add(bau);
                db.SaveChanges();
            }
            catch (Exception erro)
            {


            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Cedente"></param>
        /// <param name="ListaSacado"></param>
        /// <param name="ListBoleto"></param>
        /// <param name="Lote"></param>
        /// <param name="ListaCOntrato"></param>
        public void GeraRemessaLista(CedenteInfo Cedente, List<SacadoInfo> ListaSacado, List<BoletoInfo> ListBoleto, string Lote, List<Contrato> ListaContrato, List<LinhaCobranca> ListaLinhaCobranca)
        {
            LayoutBancos r = new LayoutBancos();
            r.Init(Cedente);
            r.Lote = CobUtil.GetInt(Lote);
            r.ShowDumpLine = false;

            for (int i = 0; i < ListaSacado.Count; i++)
            {
               
                ListBoleto[i].Instrucoes = string.Empty;
                r.Add(ListBoleto[i], ListaSacado[i]);
                string Arquivo = HttpContext.Current.Server.MapPath(@"~/App_Data/Remessa/CNAB400" + Lote + ".TXT");
                var Remessa = RegistraRemessa(Cedente.Cedente, Lote, Cedente.CNPJ, ListBoleto.Count(), (decimal)ListBoleto.Sum(x => x.ValorDocumento),Arquivo);
                new Utils.CNAB400.Boleto().GeraBoleto(Cedente, ListaSacado[i], ListBoleto[i], "TESTE", "TESTE", Lote,ListaLinhaCobranca,Remessa.BoletoRemessaId);
               
                r.RemessaTo(Arquivo);

                
            }

            try
            {
                foreach (var item in ListaContrato)
                {
                    if (db.ContratoMovimentoFiscal.Count(x => x.ContratoCodigo == item.Codigo && x.DataMovimentacao.Month == DateTime.Now.Month) == 0)
                    {
                        using (var context = new Class1(true))
                        {

                            ContratoMovimentoFiscal movimento = new ContratoMovimentoFiscal();
                            movimento.ContratoMovimentoFiscalId = db.ContratoMovimentoFiscal.Count() + 1;
                            movimento.MapaFaturamentoId = db.Contrato.First(x => x.Codigo == item.Codigo)._MapaFaturamento.First().MapaFaturamentoId;
                            movimento.ContratoCodigo = item.Codigo;
                            movimento.DataMovimentacao = DateTime.Now;
                            movimento.Desconto = (decimal)item._MapaFaturamento.FirstOrDefault().Desconto;
                            movimento.ValorContrato = (item.Valor);
                            movimento.CpfCnpj = item._Cliente.CpfCnpj;
                            int clienteId = item._Cliente.ClienteId;
                            movimento.LinhaCobrancaId = ListaLinhaCobranca.First(x => x.ClienteId == clienteId).LinhaCobrancaId;
                            context.Entry(movimento).State = EntityState.Detached;
                            context.ContratoMovimentoFiscal.Add(movimento);
                            context.SaveChanges();
                        }
                    }

                }
            }
            catch (Exception erro)
            {

            }
            try
            {
                Bau bau = new Bau();
                foreach (var item in ListBoleto)
                {

                    bau.Cenario = "TESTE";
                    try
                    {
                        bau.CNPJ = "CNPJ: " + @String.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToInt64(Cedente.CNPJ));
                    }
                    catch
                    {

                        bau.CNPJ = "CNPJ: " + @String.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToInt64(Cedente.CNPJ.Replace("/", "").Replace("-", "").Replace(".", "")));
                    }

                    bau.TipoBau = "Remessa";
                    bau.TipoArquivo = ListaContrato.Count() > 1 ? "Agrupado ": "Avulso";
                    bau.Remessa = "CNAB400" + Lote + ".TXT";
                    bau.CodigoBr = Cedente.Banco;
                    bau.Nome = Cedente.Cedente;
                    bau.Valor = (decimal)ListBoleto.Sum(x => x.ValorDocumento);
                    bau.SetorSolicitante = "TESTE";
                    bau.Usuario = "Teste";
                    bau.Status = "Não Processado";
                    //bau.Cidade = ListaDetalhesDados.First().Cidade;
                    bau.Detalhes = ListBoleto.Count().ToString();
                    bau.DataSolicitada = DateTime.Now;
                    bau.Plano = ListaContrato.Count() > 1 ? "Planos Agrupados" : "TESTE";
                    bau.DataSolicitada = DateTime.Now;
                    bau.ObsDocumento = ListBoleto.First().NumeroDocumento;

                }
                db.Bau.Add(bau);
                db.SaveChanges();
            }
            catch (Exception erro)
            {


            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContratoId"></param>
        /// <returns></returns>
        public bool ProrrataCalcelado(Contrato Contrato)
        {
           
            try
            {
                if (Convert.ToDateTime(Contrato.DataAlteracao).Month == Convert.ToInt32(DateTime.Now.Month - 1) && Convert.ToDateTime(Contrato.DataAlteracao).Year == DateTime.Now.Year && Contrato.ContratoStatusId == 3)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {

                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContratoNrcId"></param>
        /// <returns></returns>
        public bool ProrrataCalceladoVinculado(int ContratoNrcId)
        {
            AbreConexao();
            var Contrato = db.ContratoNrc.First(x => x.ContratoNrcId == ContratoNrcId);
            try
            {
                if (Convert.ToDateTime(Contrato.DataAlteracao).Month == Convert.ToInt32(DateTime.Now.Month - 1) && Convert.ToDateTime(Contrato.DataAlteracao).Year == DateTime.Now.Year)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {

                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContratoNrcId"></param>
        /// <returns></returns>
        public bool ProrrataVinculado(int ContratoNrcId)
        {
            AbreConexao();
            var ContratoVinculado = db.ContratoNrc.First(x => x.ContratoNrcId == ContratoNrcId);
            try
            {
                if (Convert.ToDateTime(ContratoVinculado._Contrato.DataInstalacao).Month == Convert.ToInt32(DateTime.Now.Month - 1) && Convert.ToDateTime(ContratoVinculado._Contrato.DataInstalacao).Year == DateTime.Now.Year)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {

                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContratoId"></param>
        /// <returns></returns>
        public List<ContratoNrc> RetornaContratosRc(int ContratoId)
        {
            var db = new Class1(true);
            var resposta = db.ContratoNrc.Where(x => x.ContratoId == ContratoId).ToList();
            return resposta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<CenarioDetalhes.SumarioDados> SumarioDados()
        {
            int mes = Convert.ToInt32(DateTime.Now.Month - 1);
            DateTime Parameto = DateTime.Now.AddMonths(-1);
            db = new Class1(true);
            var LinhaCobranca = db.LinhaCobranca;
            var Contratos = db.Contrato.Where(x =>
            x._Produto.Tipo == "Dados" && x.ContratoStatusId == 5 && x.Ativo == true && x._MapaFaturamento.FirstOrDefault().PlanoId != 174 && x._MapaFaturamento.FirstOrDefault(f => f.PlanoId != 174).Vencimento > 0 && x.DataInstalacao < Parameto
            ||
            x._Produto.Tipo == "Dados" && x.ContratoStatusId == 3 && x.Ativo == true && x._MapaFaturamento.FirstOrDefault().PlanoId != 174 && x._MapaFaturamento.FirstOrDefault(f => f.PlanoId != 174).Vencimento > 0 && x.DataAlteracao.Month == mes
            ||
            x.ContratoStatusId == 9 && x.Ativo == true && x._MapaFaturamento.FirstOrDefault().PlanoId != 174 && x._MapaFaturamento.FirstOrDefault(f => f.PlanoId != 174).Vencimento > 0);
            SumarioDados sumario = new SumarioDados();
            try
            {
                sumario.Codigo = LinhaCobranca.First().Tipo;

                sumario.QtdFaturado = LinhaCobranca.Sum(x => x._ContratoMovimentoFiscal.Count());

                sumario.Totalfaturado = LinhaCobranca.Sum(x => x.ValorFaturado);
                sumario.TotalSemfatura = (Contratos.Sum(x => x.Valor) - LinhaCobranca.Sum(x => x.ValorFaturado));

                sumario.QtdSemFatura = (Contratos.Count() - LinhaCobranca.Sum(x => x._ContratoMovimentoFiscal.Count()));
            }
            catch 
            {
                sumario.Codigo = "Dados";

                sumario.QtdFaturado = 0;

                sumario.Totalfaturado = 0;
                sumario.TotalSemfatura = (Contratos.Sum(x => x.Valor));

                sumario.QtdSemFatura = (Contratos.Count());
            }
           

            List<CenarioDetalhes.SumarioDados> Lista = new List<SumarioDados>();
            Lista.Add(sumario);
            return Lista;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<CenarioDetalhes.SumarioDados> SumarioVinculado()
        {
            db = new Class1(true);

            IEnumerable<SumarioDados> corss =
                from t in

                 db.Contrato.Where(x => x.ParceiroMvx == false && x.Ativo == true && x.ContratoStatusId == 5 && x._MapaFaturamento.FirstOrDefault().PlanoId != 174 && x._MapaFaturamento.FirstOrDefault(d => d.PlanoId != 174).Vencimento != 0 && x._ContratoNrc.Count() > 0)
                from v in db.ContratoNrc.Where(x => x._Produto.Faturamento == "NRC")
                where t.ContratoId == v.ContratoId
                select new SumarioDados
                {
                    Codigo = "Vinculado",
                    Totalfaturado = t._ContratoNrc.Where(x => x.Faturado).Sum(x => x.Valor),
                    QtdFaturado = t._ContratoNrc.Count(x => x.Faturado),
                    TotalSemfatura = t._ContratoNrc.Where(x => x.Faturado == false).Sum(x => x.Valor),
                    QtdSemFatura = t._ContratoNrc.Count(x => x.Faturado == false),

                };
            List<CenarioDetalhes.SumarioDados> Lista = new List<SumarioDados>();
            Lista = corss.ToList();
            return Lista;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContratoNrcId"></param>
        /// <returns></returns>
        public decimal ValorProrrataCalceladoVinculado(int ContratoNrcId)
        {
            var Contrato = db.ContratoNrc.First(x => x.ContratoNrcId == ContratoNrcId);
            return  (Math.Round(Contrato.Valor / DateTime.DaysInMonth(DateTime.Now.Year, Convert.ToDateTime(Contrato.DataAlteracao).Month), 2) * Convert.ToDateTime(Contrato.DataAlteracao).Day - (decimal)Contrato._MapaFaturamento.Last().Desconto);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContratoId"></param>
        /// <returns></returns>
        public decimal ValorProrrata(Contrato Contrato)
        {
           
            return (Math.Round(Contrato.Valor / DateTime.DaysInMonth(DateTime.Now.Year, Convert.ToDateTime(Contrato.DataInstalacao).Month), 2) * ((DateTime.DaysInMonth(DateTime.Now.Year, Convert.ToDateTime(Contrato.DataInstalacao).Month)) - Convert.ToDateTime(Contrato.DataInstalacao).Day) - (decimal)Contrato._MapaFaturamento.FirstOrDefault().Desconto);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContratoNrcId"></param>
        /// <returns></returns>
        public decimal ValorProrrataVinculado(int ContratoNrcId)
        {
            var ContratoVinculado = db.ContratoNrc.First(x => x.ContratoNrcId == ContratoNrcId);
            return  (Math.Round(ContratoVinculado.Valor / DateTime.DaysInMonth(DateTime.Now.Year, Convert.ToDateTime(ContratoVinculado.DataInstalacao).Month), 2) * ((DateTime.DaysInMonth(DateTime.Now.Year, Convert.ToDateTime(ContratoVinculado.DataInstalacao).Month)) - Convert.ToDateTime(ContratoVinculado.DataInstalacao).Day) - (decimal)ContratoVinculado._Contrato._MapaFaturamento.FirstOrDefault().Desconto);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContratoId"></param>
        /// <returns></returns>
        public decimal ValorProrrataCalcelado(Contrato Contrato)
        {
         
            return  (Math.Round(Contrato.Valor / DateTime.DaysInMonth(DateTime.Now.Year, Convert.ToDateTime(Contrato.DataAlteracao).Month), 2) * Convert.ToDateTime(Contrato.DataAlteracao).Day - (decimal)Contrato._MapaFaturamento.FirstOrDefault().Desconto);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Contratos"></param>
        /// <param name="Competencias"></param>
        /// <param name="Observacoes"></param>
        /// <returns></returns>
        public string FaturamentoEspecial(string[] _Contratos, string[] Competencias, string Observacao,  string AnoCompetencia)
        {
                string[] ListaContratos = _Contratos;
                
                List<LinhaCobranca> LinhaCobranca = new List<LinhaCobranca>();
                string Msgg = Observacao == string.Empty ? "ND" : Observacao;
                HttpContext.Current.Session.Add("_Obs", Msgg);
                HttpContext.Current.Session.Add("_Ano", AnoCompetencia);

            try
            {
                var Contratos = RetornaContratos(ListaContratos);
                if (Contratos[0]._Produto.Tipo == "Dados" || Contratos[0]._Produto.Tipo == "Grupo")
                {
                    var retorno = GerarBoletoRemessa(Contratos);
                    foreach (var mes in Competencias)
                    {
                        HttpContext.Current.Session.Add("_Mes", mes);
                        LinhaCobranca = GravaLinhaCobranca(Utils.Classes.ListaRemessa, "Dados", mes, AnoCompetencia);
                        ProcessaNF(retorno, "TESTE", LinhaCobranca);
                        GeraRemessaBoletoFatEsp(retorno, LinhaCobranca, mes, AnoCompetencia);
                    }
                  
                }
             

                GC.Collect();
                GC.WaitForPendingFinalizers();
                return String.Format("Contratos Faturados com sucesso.");
            }
            catch (Exception erro)
            {
                foreach (var item in LinhaCobranca)
                {
                    db.LinhaCobranca.Remove(item);
                    db.SaveChanges();
                }
                return erro.Message;
            }

        }

        #region FATURAMENTO ESPECIAL

        public List<LinhaCobranca> GravaLinhaCobranca(List<Utils.Remessa> ListaRemessa, string Tipo, string MesCompetencia, string AnoCompetencia)
        {
            List<LinhaCobranca> List = new List<LinhaCobranca>();

            foreach (var _nota in ListaRemessa.GroupBy(x => new { x.SacadoCleinte._SacadoInfo.Documento }))
            {
                Cliente _Cliente = new Cliente();
                string cn = _nota.First().SacadoCleinte._SacadoInfo.Documento;
                _Cliente = db.cliente.First(x => x.CpfCnpj.Contains(cn));
                try
                {
                    LinhaCobranca linhaCobranca = new LinhaCobranca();
                    linhaCobranca.CenarioId = 1;
                    linhaCobranca.ClienteId = _Cliente.ClienteId;
                    linhaCobranca.DataCadastro = DateTime.Now;
                    linhaCobranca.Tipo = Tipo;
                    linhaCobranca.MesCompetencia = MesCompetencia;
                    linhaCobranca.AnoCompetencia = AnoCompetencia;
                    linhaCobranca.ValorFaturado = Math.Round(Convert.ToDecimal(_nota.Sum(x => x.SacadoCleinte.ListaBoleto.Sum(c => c.ValorDocumento))), 2);
                    db.LinhaCobranca.Add(linhaCobranca);
                    db.SaveChanges();
                    List.Add(linhaCobranca);
                }
                catch (Exception erro)
                {


                }
            }

            return List;


        }

      
       #endregion


    }
}
