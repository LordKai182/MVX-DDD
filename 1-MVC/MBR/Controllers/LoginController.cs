using Infra.EntidadesNaoPersistidas;
using MBR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBR.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            var Usuario = (string)Session["_Acesso"];
            if (Usuario == null)
            {
                return PartialView("~/Views/Login/Index.cshtml");
            }
            return View();
        }
    }
}