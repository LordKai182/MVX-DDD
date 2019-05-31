using Infra.Entidades;
using Infra.EntidadesNaoPersistidas;
using Repositorios.Interface;
using Repositorios.Repositorio;
using RestMvx.Interface;
using RestMvx.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MBR.Controllers
{
    public class ContratoController : Controller
    {
        
        IContratoService _ContratoServ = new ContratoService();
        IContratoVinculadoService _Vinculado = new ContratoVinculadoService();
        IContratoGrupoService _Grupo = new ContratoGrupoService();
        ISoService _SO = new SoService();
        IRestMvxService rest = new RestMvxService();
        public string RetornaToken()
        {
            var Usuario = (string)Session["_Acesso"];
            return Usuario;
        }
        // GET: Contrato
        [HttpPost]
        public ActionResult AlteraDetalhes(FormCollection formulario)
        {
            _ContratoServ.AlterarDetalhesDoProduto(formulario);
            var contrato = (Contrato)Session["_Contrato"];
            Session.Remove("_Contrato");
            return RedirectToAction("EditarContrato", new { ContratoId = contrato.ContratoId });
        }

        [HttpPost]
        public JsonResult SalvarGrupo(FormCollection formulario)
        {
            try
            {
                string[] Contratos = formulario["Contratos"].Split(',');
                string[] Imposto = formulario["Imposto"].Split(',');
                string[] Observacao = formulario["Obs"].Split(',');
                string Titulo = formulario["Titulo"];
                string Razao = formulario["Razao"];

                _Grupo.SalvarGrupo(Contratos, Imposto, Observacao, Titulo, Razao);
                return Json("Grupo Salvo com sucesso.");
            }
            catch (Exception erro )
            {

                return Json(erro.Message);
            }
          
        }

        [HttpGet]
        public ActionResult CriarGrupo(string Contratos)
        {
            string[] ContratosId = Contratos.Split(',');
            List<Contrato> list = new List<Contrato>();
           
            foreach (var item in ContratosId)
            {
                try
                {
                    int codigo = Convert.ToInt32(item);

                    var contrato = _ContratoServ.RetornaContrato(codigo);
                    list.Add(contrato);

                    ViewBag.Impostos = _Grupo.RetornaImpostos(list);

                }
                catch
                {

                }

            }
           
            return PartialView("~/Views/BasicoPartial/_ContratoGrupo.cshtml",list);

        }

        [HttpPost]
        public ActionResult AgruparContratos()
        {
           
            return View();
        }
        [HttpGet]
        public ActionResult Index()
        {
            var Usuario = (string)Session["_Acesso"];
            if (Usuario == null)
            {
                return PartialView("~/Views/Login/Index.cshtml");
            }
            if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_IndexGrid", Newtonsoft.Json.JsonConvert.DeserializeObject<List<ApiRetornoCliente>>(rest.GetRequisicao("Contrato/Clientes", RetornaToken())));
            return View();
        }
        [HttpGet]
        public ActionResult _PartialCliente()
        {
            var _contrato = (Contrato)Session["_Contrato"];
            return PartialView(_contrato._Cliente);
        }
        [HttpPost]
        public ActionResult _PartialCliente(FormCollection formulario)
        {
           
            return PartialView(_ContratoServ.AlteraCliente(formulario));
           
        }
        [HttpGet]
        public ActionResult _PartialEndereco()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult _PartialEndereco(FormCollection formulario)
        {
            string Msg = string.Empty;
            int ContratoId = Convert.ToInt32(formulario["Contra"]);
            _ContratoServ.InsereClienteEndereco(formulario, ContratoId);
            return PartialView();
        }
        [HttpGet]
        public ActionResult _ListaContratoVinculados()
        {
            var _contrato = (Contrato)Session["_Contrato"];
            var ContatosVinculados = _contrato._ContratoNrc.ToList();
            return PartialView(ContatosVinculados);
        }
        [HttpPost]
        public ActionResult _ListaContratoVinculados(FormCollection formulario)
        {
            _Vinculado.IniciarContratoVinculado(formulario);
            var contrato = (Contrato)Session["_Contrato"];
            return RedirectToAction("EditarContrato", new { ContratoId = contrato.ContratoId });
        }
        [HttpGet]
        public ActionResult _Produto()
        {
            var _contrato = (Contrato)Session["_Contrato"];

            return PartialView(_contrato);
        }
        [HttpGet]
        public ActionResult DeletaContatoVinculado(int ContatoNrcId)
        {
            _ContratoServ.DeletaContratoVinculado(ContatoNrcId);
            var _contrato = (Contrato)Session["_Contrato"];
            return RedirectToAction("EditarContrato", new { ContratoId = _contrato.ContratoId });

        }
        [HttpGet]
        public ActionResult DeletaContato(int ContatoId)
        {
            _ContratoServ.DeletaContato(ContatoId);
            var _contrato = (Contrato)Session["_Contrato"];
            return RedirectToAction("EditarContrato", new { ContratoId = _contrato.ContratoId });

        }
        [HttpGet]
        public ActionResult _PartialContatos()
        {
            var _contrato = (Contrato)Session["_Contrato"];
            var Contatos = _contrato._ClienteContato.ToList();
            return PartialView(Contatos);
        }
        [HttpPost]
        public ActionResult _PartialContatos(FormCollection form)
        {
            _ContratoServ.InsereContato(form);
            var contrato = (Contrato)Session["_Contrato"];
            return RedirectToAction("EditarContrato", new { ContratoId = contrato.ContratoId });

        }
        
        [HttpGet]
        public PartialViewResult _PartialEventos()
        {
            var contrato = (Contrato)Session["_Contrato"];
            return PartialView(contrato._EventosDeContrato.OrderBy(x=>x.DataCriacao).ToList());
        }
        [HttpGet]
        public PartialViewResult IndexGrid()
        {
            var Resposta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ApiRetornoCliente>>(rest.GetRequisicao("Contrato/Clientes", RetornaToken()));
          
            return PartialView("_IndexGrid", Resposta);
        }
        [HttpPost]
        public ActionResult EventosCliente(FormCollection formulario)
        {
            return View("~/Views/BasicoPartial/_FormularioEventoCliente.cshtml");
        }
        
        [HttpPost]
        public ActionResult SalvarEventos()
        {
            var Contrato = (List<EventosDeContrato>)Session["_Eventos"];
          
            _SO.SalvarEventos(Contrato);
            int ContratoID = Contrato[0].ContratoId;
            Session.Remove("_Eventos");

            return RedirectToAction("EditarContrato", new { ContratoId = ContratoID });
        }
        [HttpPost]
        public PartialViewResult EventosContrato(FormCollection formulario)
        {
            return PartialView();
        }
        public ActionResult EditarCliente(int ContratoId, string Mensagem)
        {
            return View(_ContratoServ.RetornaContratosPorCliente(ContratoId));
        }
        public ActionResult EditarContrato(int ContratoId)
        {
            var Contrato = _ContratoServ.RetornaContrato(ContratoId);
            HttpContext.Session.Add("_Contrato", Contrato);
            return View(Contrato);
        }
        public ActionResult Endereco(string TipoEndereco, int ContratoId)
        {

            #region ENDERECO
            var listaEndereco = new List<object>();
            int EnderecoId = Convert.ToInt32(TipoEndereco);

            ClienteEndereco Ende = new ClienteEndereco();
            try
            {
               
                    Ende = _ContratoServ.RetornaEnderecoPorContrato(ContratoId, EnderecoId);

              
                listaEndereco.Add(new
                {
                    Enderecoid = Ende.ClienteEnderecoId,
                    Predio = Ende._Predio.Nome,
                    _Cep = Ende.Cep,
                    Ibge = Ende._Bairro._Cidade.CodIbge,
                    _Numero = Ende.Numero,
                    _Logradouro = Ende.Logradouro,
                    Cidade = Ende._Bairro._Cidade.Nome,
                    Ende._Bairro.Nome,
                    Uf = Ende._Bairro._Cidade._Estado.Estadosigla,
                    _Complemento = Ende.Complemento,
                    Bairro = Ende._Bairro.Nome,
                    _Andar = Ende.Andar,
                    _Bloco = Ende.Bloco,
                    _Sala = Ende.Sala,
                    _predioid = Ende._Predio.PredioId
                });

            }
            catch
            {

                listaEndereco.Add(new { Enderecoid = 0, Predio = "ENDERECO NAO CADASTRADO" });

            }
            #endregion

            return Json(listaEndereco, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BuscarEventoSO(string NumeroSO)
        {
            var contrato = (Contrato)Session["_Contrato"];
            int codigoSo = Convert.ToInt32(NumeroSO);
            var resultado = _SO.BuscaEventoSo(codigoSo,contrato.ContratoId);
            HttpContext.Session.Add("_Eventos", resultado);
            return PartialView(resultado);

        }
        public ActionResult BuscarSO(string NumeroSO)
        {
            int codigoSo = Convert.ToInt32(NumeroSO);
            int resultado = _SO.CriarContratoPorSO(codigoSo);

            return RedirectToAction("EditarContrato", new { ContratoId = resultado });
          
        }
        public ActionResult _Detalhes()
        {
            var _contrato = (Contrato)Session["_Contrato"];
            return PartialView(_ContratoServ.PropriedadeContratoDigitado(_contrato.ContratoId));
           
        }
        public ActionResult _MapaFaturamento()
        {
            var Contrato = (Contrato)Session["_Contrato"];
            var Mapa = Contrato._MapaFaturamento.FirstOrDefault();
            return PartialView(Mapa);
        }
        [HttpPost]
        public ActionResult _MapaFaturamento(FormCollection formulario)
        {
            var Contrato = (Contrato)Session["_Contrato"];
            _ContratoServ.SalvaMapaDeFaturamento(formulario);
            return RedirectToAction("EditarContrato", new { ContratoId = Contrato.ContratoId });
           
        }
        [HttpPost]
        public ActionResult CadastroContraManual(FormCollection formulario)
        {
            return RedirectToAction("EditarContrato", new { ContratoId = _ContratoServ.InicaContratoNovo(formulario) });
        }
    }
}