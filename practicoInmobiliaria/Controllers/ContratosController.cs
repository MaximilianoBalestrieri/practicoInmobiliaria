using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace practicoInmobiliaria.Controllers
{
    using practicoInmobiliaria.Models;
    using System.Web.Mvc; // IMPORTANTE

    public class ContratosController : Controller
    {
        ConexionDB db = new ConexionDB();

        public ActionResult Index()
        {
            var contratos = db.ObtenerTodosLosContratos();
            return View(contratos);
        }

        // GET: Contratos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contratos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contrato contrato)
        {
            if (ModelState.IsValid)
            {
                db.AgregarContrato(contrato); // Asegurate de tener este método en ConexionDB
                return RedirectToAction("Index");
            }
            return View(contrato);
        }

        public ActionResult BuscarPropietarios(string termino)
        {
            var propietarios = db.BuscarPropietarios(termino);  // Método para buscar propietarios
            return PartialView("_ListaPropietarios", propietarios); // Vista parcial que muestra los resultados
        }

        // Acción para buscar inquilinos
        public ActionResult BuscarInquilinos(string termino)
        {
            var inquilinos = db.BuscarInquilinos(termino);  // Método para buscar inquilinos
            return PartialView("_ListaInquilinos", inquilinos); // Vista parcial que muestra los resultados
        }

        [HttpGet]
        public JsonResult BuscarInmueblesPorDni(string dniPropietario)
        {
            var conexion = new ConexionDB();
            var lista = conexion.ObtenerInmuebles()
                .Where(i => i.DniPropietario == dniPropietario)
                .ToList();
            return Json(lista);
        }


    }
}