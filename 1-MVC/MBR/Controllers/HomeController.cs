using Infra;
using Infra.EntidadesNaoPersistidas;
using RestMvx.Interface;
using RestMvx.Repositorio;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace MBR.Controllers
{
    public class HomeController : Controller
    {

        IRestMvxService rest = new RestMvxService();


        [HttpPost]
        public ActionResult Index(FormCollection formulario)
        {
            //var login = rest.LoginPortalMundiBr(formulario["Login"], formulario["Senha"]);
            //if (login != null)
            //{
            //    Session.Add("_Acesso", login);
            //}
            //var Usuario = (string)Session["_Acesso"];
            //if (Usuario == null)
            //{
            //    return PartialView("~/Views/Login/Index.cshtml");
            //}

            return View();
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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}