using Infra;
using Infra.Entidades;
using Infra.EntidadesNaoPersistidas;
using Repositorios.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiposEnum;
using static Infra.EntidadesNaoPersistidas.CenarioDetalhes;

namespace Repositorios.Repositorio
{
    public class ContratoGrupoService : IContratoGrupoService
    {
        Class1 db;
        void AbreConexao()
        {
            db = new Class1(true);
        }

        public bool AdicionarContratoAoGrupo(int GrupoId, int ContratoId)
        {
            AbreConexao();

            try
            {
                var Contrato = db.Contrato.First(x => x.ContratoId == ContratoId);
                Contrato.ContratoGrupoId = GrupoId;
                db.Contrato.Update(Contrato);
                db.SaveChanges();

                return true;
            }
            catch 
            {

                return false;
            }
        }

        public bool RetirarContratoDoGrupo(int GrupoId, int ContratoId)
        {
            AbreConexao();

            try
            {
                var Contrato = db.Contrato.First(x => x.ContratoId == ContratoId);
                Contrato.ContratoGrupoId = null;
                db.Contrato.Update(Contrato);
                db.SaveChanges();

                return true;
            }
            catch
            {

                return false;
            }
        }

        public bool DesfazerGrupo(int GrupoId)
        {
            AbreConexao();
            try
            {
                foreach (var contrato in db.Contrato.Where(x=>x.ContratoGrupoId == GrupoId))
                {
                    RetirarContratoDoGrupo((int)contrato.ContratoGrupoId, contrato.ContratoId);
                }

                var Grupo = db.ContratoGrupo.First(x => x.ContratoGrupoId == GrupoId);
                db.ContratoGrupo.Remove(Grupo);
                db.SaveChanges();
                 
                return true;
            }
            catch 
            {

                return false;
            }
        }

        public List<Contrato> ListaDeContratosPorGrupos(int GrupoId)
        {
            AbreConexao();
            return db.Contrato.Where(x => x.ContratoGrupoId == GrupoId).ToList();
        }

        public List<ContratoGrupo> ListaDeGrupos()
        {
            AbreConexao();
            return db.ContratoGrupo.ToList();
        }

        public List<Detalhes> ListaFaturamento()
        {
            AbreConexao();
        
            Utils.Calculos _Calculos = new Utils.Calculos();
            db = new Class1(true
                );
            IEnumerable<Detalhes> corss =
            from t in
            db.Contrato.Where(x =>x.ContratoGrupoId != null)
           
            from m in db.MapaFaturamento.OrderByDescending(i => i.MapaFaturamentoId).Take(1)

            where m.ContratoId == t.ContratoId

            select new Detalhes
            {
                ContratoId = t.ContratoId,
                StatusId = t.ContratoStatusId,
                Codigo = t.Codigo,
                //Cidade = t._InstalacaoEndereco.FirstOrDefault(x => x.TipoLogradouroId == 1)._Bairro._Cidade.Nome,
                //UF = t._InstalacaoEndereco.FirstOrDefault(x => x.TipoLogradouroId == 1)._Bairro._Cidade._Estado.Estadosigla,
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
                StatusCenario = m._ContratoMovimentoFiscal.Count() == 0 ? 1 : m._ContratoMovimentoFiscal.Count() > 0 ? 2 : 0,
                StatusContrato = m._Contrato.DataCriacao.Month == DateTime.Now.Month ? 3 : 0,
                PlanoId = m.PlanoId,
                Plano = m._Plano.PlanoNome,
                RenovacaoAutomatica = m.RenovacaoAutomatica == true ? "SIM" : "NÃO",
                Desconto = m.Desconto,
                Historico = t._ContratoHistorico.Count(x => x.Campo == "Valor") > 0 ? true : false,
                CodigoMundiData = t._ContratoRelacionamento.FirstOrDefault().DadosBabelId.ToString(),
                CodPar = t._ContratoRelacionamento.FirstOrDefault().CodPar.ToString(),
                VencimentoCorrente = m.MesParcelas,
                Grupo = t._ContratoGrupo.Codigo,
                GrupoId = (int)t.ContratoGrupoId,
             
                TipoPagamento = m.Tipo == 1 ? "Débito Automatico" : m.Tipo == 2 ? "Depósito" : "Boleto",
                VencidoVencimento = m.MesParcelas == 1 ? "Vencimento" : m.MesParcelas == 2 ? "Vencido" : "Não Definido",
                Competencia = new Utils.retornos().RetornaPrestacao(m.MesParcelas).Month + "/" + new Utils.retornos().RetornaPrestacao(m.MesParcelas).Year + " - " + new Utils.retornos().RetornaVencimento(m.MesParcelas).Month + "/" + new Utils.retornos().RetornaVencimento(m.MesParcelas).Year,


            };

            List<Detalhes> Lista = new List<Detalhes>();
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
        
        public List<Detalhes> ListafaturamentoPorGrupo(int GrupoId)
        {
            AbreConexao();
            int mes = Convert.ToInt32(DateTime.Now.Month - 1);
            DateTime Parameto = DateTime.Now.AddMonths(-1);
            Utils.Calculos _Calculos = new Utils.Calculos();
            db = new Class1(true
                );
            IEnumerable<Detalhes> corss =
            from t in
            db.Contrato.Where(x => x.ContratoGrupoId == GrupoId && x._Produto.Tipo == "Dados")
            from m in db.MapaFaturamento.Where(x => x.PlanoId != 174)
            where m.ContratoId == t.ContratoId

            select new Detalhes
            {
                ContratoId = t.ContratoId,
                StatusId = t.ContratoStatusId,
                Codigo = t.Codigo,
                //Cidade = t._InstalacaoEndereco.FirstOrDefault(x => x.TipoLogradouroId == 1)._Bairro._Cidade.Nome,
                //UF = t._InstalacaoEndereco.FirstOrDefault(x => x.TipoLogradouroId == 1)._Bairro._Cidade._Estado.Estadosigla,
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
                StatusCenario = m._ContratoMovimentoFiscal.Count() == 0 ? 1 : m._ContratoMovimentoFiscal.Count() > 0 ? 2 : 0,
                StatusContrato = m._Contrato.DataCriacao.Month == DateTime.Now.Month ? 3 : 0,
                PlanoId = m.PlanoId,
                Plano = m._Plano.PlanoNome,
                RenovacaoAutomatica = m.RenovacaoAutomatica == true ? "SIM" : "NÃO",
                Desconto = m.Desconto,
                Historico = t._ContratoHistorico.Count(x => x.Campo == "Valor") > 0 ? true : false,
                CodigoMundiData = t._ContratoRelacionamento.FirstOrDefault().DadosBabelId.ToString(),
                CodPar = t._ContratoRelacionamento.FirstOrDefault().CodPar.ToString(),
                VencimentoCorrente = m.MesParcelas,
                Grupo = t._ContratoGrupo.Codigo,
                GrupoId = (int)t.ContratoGrupoId,
                TipoPagamento = m.Tipo == 1 ? "Débito Automatico" : m.Tipo == 2 ? "Depósito" : "Boleto"
            };

            List<Detalhes> Lista = new List<Detalhes>();
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
          
      
            
        public SumarioDados ApiSumarioGrupos()
        {
            AbreConexao();
            DateTime Parameto = DateTime.Now.AddMonths(-1);
            IEnumerable<SumarioDados> corss =
                from t in
                db.Contrato.Where(x => x.ContratoGrupoId != null && x._Produto.Tipo == "Dados")
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


        public List<SumarioDados> SumarioGrupos()
        {
            AbreConexao();
            DateTime Parameto = DateTime.Now.AddMonths(-1);
            IEnumerable<SumarioDados> corss =
                from t in
                db.Contrato.Where(x => x.ContratoGrupoId != null && x._Produto.Tipo == "Dados")
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
            return Lista;
        }

        public void SalvarGrupo(string[] Contratos, string[] Imposto, string[] Observacao, string Titulo, string RazaoNota)
        {
                string obs = string.Empty;

                AbreConexao();
                for (int i = 0; i < (Imposto.Count()); i++)
                {
                   try
                   {
                  
                        obs = obs + Imposto[i] + "," + Observacao[i] + "|";

                    
                }
                   catch
                   {
                   
                     
                   }
                  
                }
                    ContratoGrupo grp = new ContratoGrupo { Codigo = Titulo, DataCriacao = DateTime.Now, Observacoes = obs, RazaoNota = RazaoNota , ContratoGrupoId = (db.ContratoGrupo.Count() + 1) };
                    db.ContratoGrupo.Add(grp);
                    db.SaveChanges();
                    foreach (var item in Contratos)
                    {
                        try
                        {
                            int Codigo = Convert.ToInt32(item);
                            var Contrato = db.Contrato.First(x => x.ContratoId == Codigo);
                            Contrato.ContratoGrupoId = grp.ContratoGrupoId;
                            db.Contrato.Update(Contrato);
                            db.SaveChanges();
                   
                        }
                        catch
                        {
                   
                          
                        }
                    }
            
          
        }

        public List<GrupoImposto> RetornaImpostos(List<Contrato> ListaCOntratos)
        {
            AbreConexao();
            List<GrupoImposto> ListaImposto = new List<GrupoImposto>();

            foreach (var contr in ListaCOntratos)
            {
                foreach (var imposto in contr._MapaFaturamento.First()._Plano._PlanoEmpresa)
                {
                    var empresa = db.Empresa.First(x => x.EmpresaId == imposto.EmpresaId);
                    ListaImposto.Add(new GrupoImposto {Empresa = empresa.NomeFantasia,Imposto = imposto.Imposto,Observacao = "" });
                }
            }
            List<GrupoImposto> ListaUnica = new List<GrupoImposto>();
            foreach (var item in ListaImposto.GroupBy(x=>new {x.Empresa,x.Imposto }))
            {
                ListaUnica.Add(new GrupoImposto { Empresa = item.First().Empresa, Imposto = item.First().Imposto, Observacao = "" });
            }


            return ListaUnica;
        }



      
    }
}
