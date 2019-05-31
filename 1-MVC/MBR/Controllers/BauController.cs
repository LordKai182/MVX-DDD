using Infra.EntidadesNaoPersistidas;
using MBR.Models;
using Repositorios.Interface;
using Repositorios.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBR.Controllers
{
    public class BauController : Controller
    {
        // GET: Bau
      
        IArquivoBauService _ArqBau = new ArquivoBauService();
        // GET: Bau
        public ActionResult Index()
        {
            var Usuario = (string)Session["_Acesso"];
            if (Usuario == null)
            {
                return PartialView("~/Views/Login/Index.cshtml");
            }
            return View();
        }

        [HttpGet]
        public PartialViewResult _Detalhes()
        {
            var Resposta = _ArqBau.RetornaBau().OrderBy(x => x.DataSolicitada).ToList();

            return PartialView("_BauArquivos", Resposta);
        }

        [HttpPost]
        public ActionResult ProcessaEventos(FormCollection formulario)
        {
            string evento = formulario["EventoBau"];

            if (evento == "GeraNf")
            {
                var resposta = _ArqBau.CriaNfse(formulario);
            }
            if (evento == "GeraRemessa")
            {
                var resposta = _ArqBau.CriaRemessa(formulario);
                return File(resposta, "application/zip", "Remessa" + DateTime.Now.ToString("MMyy") + ".zip");
            }
            if (evento == "ConsultaNf")
            {
                _ArqBau.ConsultaNF(formulario);
            }
            return RedirectToAction("Index");
        }
    }
}