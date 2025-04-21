using practicoInmobiliaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace practicoInmobiliaria.Controllers
{

    //-----------------PAGOS CONTROLLER -----------------
    public class PagosController : Controller
    {
        private readonly ConexionDB _db;

        public PagosController()
        {
            _db = new ConexionDB(); // O usa inyección si tienes configurado
        }

        public ActionResult Index(int idContrato)
        {
            var pagos = _db.ObtenerPagosPorContrato(idContrato)
                           .OrderBy(p => p.NroPago) // ordenamos por número de pago
                           .ToList();

            ViewBag.IdContrato = idContrato;
            return View(pagos);
        }


        [HttpPost]
        public JsonResult AnularPago(int idPago)
        {
            try
            {
                var db = new ConexionDB();
                db.AnularPago(idPago);  // Método en tu clase ConexionDB
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult ActualizarDetalle(int idPago, string detalle)
        {
            try
            {
                ConexionDB db = new ConexionDB();
                db.ActualizarDetalle(idPago, detalle);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }


    }
}