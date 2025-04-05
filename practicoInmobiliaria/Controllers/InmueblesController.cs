using System.Web.Mvc;
using practicoInmobiliaria.Models;
using System.Collections.Generic;

namespace practicoInmobiliaria.Controllers
{
    public class InmueblesController : Controller
    {
        private ConexionDB conexionDB = new ConexionDB();

        // GET: Inmuebles
        public ActionResult Index()
        {
            List<Inmueble> inmuebles = conexionDB.ObtenerInmuebles();
            return View(inmuebles);
        }

        // GET: Inmuebles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inmuebles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inmueble inmueble)
        {
            if (ModelState.IsValid)
            {
                conexionDB.AgregarInmueble(inmueble); // Corrección: uso de instancia
                return RedirectToAction("Index");
            }

            return View(inmueble);
        }

        // GET: Inmuebles/Edit/5
        public ActionResult Edit(int id)
        {
            var inmueble = conexionDB.ObtenerInmueblePorId(id);

            if (inmueble == null)
            {
                ViewBag.Mensaje = "No se encontró el inmueble con ID " + id;
                return View("Index", conexionDB.ObtenerInmuebles());
            }

            return View(inmueble);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Inmueble inmueble)
        {
            if (ModelState.IsValid)
            {
                conexionDB.ActualizarInmueble(inmueble);
                return RedirectToAction("Index");
            }
            return View(inmueble);
        }

        // GET: Inmuebles/Delete/5
        public ActionResult Delete(int id)
        {
            var inmueble = conexionDB.ObtenerInmueblePorId(id);
            if (inmueble == null)
            {
                return HttpNotFound();
            }
            return View(inmueble);
        }

        // POST: Inmuebles/Delete/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarConfirmado(int id)
        {
            conexionDB.EliminarInmueble(id);
            return RedirectToAction("Index");
        }
    }
}
