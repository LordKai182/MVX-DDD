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
    public class PendenciaController : Controller
    {
        IPendenciaService _pendencia = new PendenciaService(true);
        // GET: Pendencia
        public ActionResult Index()
        {
            var Usuario = (string)Session["_Acesso"];
            if (Usuario == null)
            {
                return PartialView("~/Views/Login/Index.cshtml");
            }
            ViewBag.ListaVendedor = _pendencia.RetornaVendedores();

            return View();
        }
        [HttpGet]
        public ActionResult _PartialEventos()
        {
            return PartialView(_pendencia.RetornaEventos());
        }

        [HttpGet]
        public ActionResult Contratos()
        {
            return PartialView(_pendencia.RetornaContratos());
        }

        [HttpGet]
        public ActionResult Vinculados()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ProcessaEvento(FormCollection formulario)
        {
            _pendencia.ProcessaPendencia(formulario);
            return RedirectToAction("Index");
        }
        
    }
}