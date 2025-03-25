using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace practicoInmobiliaria.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Descripción del Software.";

            return View();
        }



        public ActionResult Contact()
        {
            ViewBag.Message = "CONTACTO";

            return View();
        }

        public ActionResult Inquilinos()
        {
            return View(); // Retorna la vista Inquilinos.cshtml
        }

        public ActionResult Propietarios()
        {
            return View(); // Retorna la vista Inquilinos.cshtml
        }

        public ActionResult Contratos()
        {
            return View(); // Retorna la vista Inquilinos.cshtml
        }
    }
}