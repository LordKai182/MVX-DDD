using Infra.EntidadesNaoPersistidas;
using MBR.Models;
using Repositorios.Interface;
using Repositorios.Repositorio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBR.Controllers
{
    public class ArquivosController : Controller
    {
        IArquivosServices _Arquivos = new ArquivosServices();
        IArquivoBauService _ArqBau = new ArquivoBauService();
        // GET: Arquivos
        public ActionResult Index()
        {
            var Usuario = (string)Session["_Acesso"];
            if (Usuario == null)
            {
                return PartialView("~/Views/Login/Index.cshtml");
            }
            ViewBag.Impressoras = new Utils.retornos().RetornaImpressoras();
            ViewBag.Filtro = _ArqBau.RetornoArquivos();
            return View();
        }

        [HttpGet]
        public PartialViewResult _Detalhes()
        {
            var Resposta = _ArqBau.RetornoArquivos();

            return PartialView("_Arquivos", Resposta);
        }

        [HttpPost]
        public PartialViewResult _Detalhes(string Cliente, int Vencimento, string TipoArquivo)
        {
            var Resposta = _Arquivos.RetornaListaArquivos().ToList();
            if(Cliente != "" )
            {
                if (Resposta.Count(x => x.cliente == Cliente ) > 0)
                    Resposta = Resposta.Where(x => x.cliente == Cliente).ToList();
            }
            if (Vencimento != 0)
            {

                Resposta = Resposta.Where(x => x.Vencimento == Vencimento).ToList();
            }
            if (TipoArquivo != "")
            {
                if (Resposta.Count(x => x.tipoArquivo == TipoArquivo) > 0)
                    Resposta = Resposta.Where(x => x.tipoArquivo == TipoArquivo).ToList();
            }


            return PartialView("_Arquivos", Resposta);
        }
        public static Bitmap GetImageByName(string imageName)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            string resourceName = asm.GetName().Name + ".Properties.Resources";
            var rm = new System.Resources.ResourceManager(resourceName, asm);
            return (Bitmap)rm.GetObject(imageName);

        }
        [HttpPost]
        public ActionResult ProcessaEventos(FormCollection form)
        {

            try
            {
                string evento = form["EventoArquivo"];
                if (evento == "Imprimir")
                {
                    _Arquivos.Imprimir(form);
                    return RedirectToAction("Index");
                }
                if (evento == "Download")
                {
                    var Retorno = _Arquivos.Download(form);
                    return File(Retorno, "application/zip", "Download" + DateTime.Now.ToString("MMyy") + ".zip");
                }
                if (evento == "Descricao")
                {
                    _Arquivos.Descricao(form);
                }
            }
            catch(Exception erro)
            {

                return RedirectToAction("Index");
            }


            return null;
        }
    }
}