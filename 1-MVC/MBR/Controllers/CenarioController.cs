using Infra.EntidadesNaoPersistidas;
using Repositorios.Interface;
using Repositorios.Repositorio;
using RestMvx.Interface;
using RestMvx.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using static Infra.EntidadesNaoPersistidas.CenarioDetalhes;

namespace MBR.Controllers
{
    public class CenarioController : Controller
    {
        // GET: Cenario
        ICenarioService _Cenario = new CenarioService();
        IContratoGrupoService _Grupo = new ContratoGrupoService();
        IRestMvxService rest = new RestMvxService();



        #region GET

        [HttpGet]
        public ActionResult _ContratosRc(int ContratoId)
        {
            var resposta = _Cenario.RetornaContratosRc(ContratoId);
            return PartialView(resposta);
        }
        [HttpGet]
        public ActionResult MenuDadosVinculado()
        {

            return PartialView("_ContratosCenariosVinculado", _Cenario.DetalhesVinculado("Vinculado"));
        }
        [HttpGet]
        public ActionResult MenuGrupo()
        {

            return PartialView("_ContratosCenariosGrupo", _Grupo.ListaFaturamento());
        }
        [HttpGet]
        public ActionResult MenuDados()
        {

            return PartialView("_ContratosCenarios", _Cenario.Detalhes("Dados"));
        }
        [HttpGet]
        public PartialViewResult SumarioGrupo()
        {
            ViewBag.Cenario = "Grupo";

            //var retorno = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SumarioDados>>(rest.GetRequisicao("Cenario/SumarioGrupo", RetornaToken()));

            return PartialView("_SumarioDados", _Cenario.ApiSumarioDados());
        }
        [HttpGet]
        public PartialViewResult SumarioDados()
        {
            ViewBag.Cenario = "Dados";

            //var retorno = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SumarioDados>>(rest.GetRequisicao("Cenario/SumarioDados", RetornaToken()));

            return PartialView("_SumarioDados", _Cenario.ApiSumarioDados());
        }
        [HttpGet]
        public PartialViewResult SumarioVoz()
        {
            //var retorno = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SumarioDados>>(rest.GetRequisicao("Cenario/SumarioVoz", RetornaToken()));
            return PartialView("_SumarioVoz", _Cenario.ApiSumarioDados());
        }
        [HttpGet]
        public PartialViewResult SumarioVinculado()
        {
            return PartialView("_SumarioVinculado", _Cenario.SumarioVinculado().Where(x => x.Codigo == "Vinculado"));
        }
        [HttpGet]
        public ActionResult Detalhes(string Tipo)
        {
            ViewBag.Cenario = Tipo;
            var resposta = Tipo == "Vinculado" ? _Cenario.DetalhesVinculado(Tipo) : Tipo == "Grupo" ? _Grupo.ListaFaturamento() : _Cenario.Detalhes(Tipo);

            return View(resposta);
        }
        [HttpGet]
        public PartialViewResult _Detalhes(string Tipo, List<Detalhes> model)
        {
            var resposta = Tipo == "Vinculado" ? _Cenario.DetalhesVinculado(Tipo) : Tipo == "Grupo" ? _Grupo.ListaFaturamento() : _Cenario.Detalhes(Tipo);

            return PartialView("_DetalhesCenario", resposta);
        }
       
        #endregion


        #region POST

        [HttpPost]
        public PartialViewResult _DetalhesCenario(string nome)
        {


            return PartialView();
        }
        [HttpPost]
        public PartialViewResult _Detalhes(string Produto, string Plano, string Tipo, int Vencimento, string NomeRazao, string TipoCliente, int StatusCenario, int Grupo)
        {
            ViewBag.Cenario = Tipo;
            List<Detalhes> resposta = new List<Detalhes>();
            if (Tipo == "Dados")
            {
                resposta = _Cenario.Detalhes(Tipo);

            }
            if (Tipo == "Grupo")
            {
                resposta = Grupo == 0 ? _Grupo.ListaFaturamento() : _Grupo.ListafaturamentoPorGrupo(Grupo);

            }
            if (Produto != "")
            {
                if (resposta.Count(x => x.Produto == Produto) > 0)
                    resposta = resposta.Where(x => x.Produto == Produto).ToList();
            }
            if (Plano != "")
            {
                if (resposta.Count(x => x.Plano == Plano) > 0)
                    resposta = resposta.Where(x => x.Plano == Plano).ToList();
            }
            if (TipoCliente != "")
            {
                if (resposta.Count(x => x.TipoCliente == TipoCliente) > 0)
                    resposta = resposta.Where(x => x.TipoCliente == TipoCliente).ToList();
            }
            if (NomeRazao != "")
            {
                if (resposta.Count(x => x.CnpjCpf == NomeRazao) > 0)
                    resposta = resposta.Where(x => x.CnpjCpf == NomeRazao).ToList();
            }
            if (Vencimento != 0)
            {

                resposta = resposta.Where(x => x.Vencimento == Vencimento).ToList();
            }
            if (StatusCenario != 0)
            {
                if (StatusCenario == 3 || StatusCenario == 4)
                {
                    resposta = resposta.Where(x => x.StatusContrato == StatusCenario).ToList();

                }
                if (StatusCenario == 1 || StatusCenario == 2)
                {

                    resposta = resposta.Where(x => x.StatusCenario == StatusCenario).ToList();
                }
                if (StatusCenario == 5)
                {
                    resposta = resposta.Where(x => x.Prorrata).ToList();
                }
                if (StatusCenario == 6)
                {
                    resposta = resposta.Where(x => x.StatusId == 3).ToList();
                }
                if (StatusCenario == 7)
                {
                    resposta = resposta.Where(x => x.StatusId == 9).ToList();
                }
            }

            return PartialView("_DetalhesCenario", resposta);
        }
        [HttpPost]
        public ActionResult FaturamentoEspecial(FormCollection formulario)
        {
            string[] Contratos = formulario["ContratosIdsSp"].Split(',');
            string[] Competencias = formulario["competencias"].Split(',');
            string Ano = formulario["Ano"];
            string Cenario = formulario["Cenario"];
            string Observacao = formulario["Observacao"];

            _Cenario.FaturamentoEspecial(Contratos, Competencias, Observacao, Ano);

            return RedirectToAction("Index");


        }
      
        #endregion

        public string RetornaToken()
        {
            var Usuario = (string)Session["_Acesso"];
            return Usuario;
        }
       
        public string Requisicao(string Uri)
        {
             var _Acesso = (AcessoUsuario)Session["_Acesso"];
           
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            var webUrl = "http://192.168.0.102:1230/api/";
            var uri = Uri;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(webUrl);
                client.DefaultRequestHeaders.Add("Authorization", _Acesso.UsuarioToken);
                var response = client.GetAsync(uri).Result;
                return  response.Content.ReadAsStringAsync().Result;
            }
           
        }

        public ActionResult Index()
        {
            //var Usuario = (string)Session["_Acesso"];
            //if (Usuario == null)
            //{
            //    return PartialView("~/Views/Login/Index.cshtml");
            //}
            return View();
        }

        public ActionResult ProcessaEvento(FormCollection formulario)
        {
            try
            {


                return Json(new { success = true, responseText = _Cenario.ProcessarEvento(formulario) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception erro)
            {

                return Json(new { success = false, responseText = erro.Message }, JsonRequestBehavior.AllowGet);
            }


        }
       

     

    }
    }