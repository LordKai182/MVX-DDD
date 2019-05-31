using Infra.Entidades;
using Repositorios.Interface;
using Repositorios.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBR.Controllers
{
    public class ContratoVinculadoController : Controller
    {
        IContratoVinculadoService _Vinculado = new ContratoVinculadoService();
        // GET: ContratoVinculado
        public ActionResult Index(int ContratoNrcId)
        {
            var Contrato = _Vinculado.RetornaContratoVinculado(ContratoNrcId);
            HttpContext.Session.Add("_ContratoVinculado", Contrato);
            return View();
        }
        [HttpPost]
        public ActionResult AlteraDetalhes(FormCollection formulario)
        {

            _Vinculado.AlterarDetalhesDoProduto(formulario);
            var contrato = (ContratoNrc)Session["_ContratoVinculado"];
            Session.Remove("_ContratoVinculado");
            return RedirectToAction("Index", new { ContratoNrcId = contrato.ContratoNrcId });
        }
        [HttpGet]
        public ActionResult ExcluirVinculado(int ContratoNrcId)
        {
            _Vinculado.DeleteContratoVinculado(ContratoNrcId);

            return PartialView();
        }

        [HttpGet]
        public ActionResult _Detalhes()
        {

            return PartialView();
        }

        [HttpPost]
        public ActionResult _MapaFaturamento(FormCollection formulario)
        {
            _Vinculado.SalvaMapaDeFaturamento(formulario);
            return PartialView();
        }

        [HttpGet]
        public ActionResult _PartialCliente()
        {

            return PartialView();
        }
        [HttpGet]
        public ActionResult _Produto()
        {

            return PartialView();
        }

        [HttpGet]
        public ActionResult _MapaFaturamento()
        {

            return PartialView();
        }
    }
}