using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using practicoInmobiliaria.Models;

namespace practicoInmobiliaria.Controllers
{
    public class InquilinosController : Controller
    {
        // Acción que obtiene los inquilinos y los pasa a la vista
        public ActionResult Index()
        {
            ConexionDB db = new ConexionDB();
            List<Inquilino> inquilinos = db.ObtenerInquilinos();  // Obtén los inquilinos de la base de datos

            // Depuración: Verifica los datos
            foreach (var inquilino in inquilinos)
            {
                Console.WriteLine($"Inquilino ID: {inquilino.IdInquilino}, Nombre: {inquilino.NombreInquilino}, Apellido: {inquilino.ApellidoInquilino}");
            }

            return View(inquilinos);  // Pasa la lista de inquilinos a la vista
        }

        // GET: Inquilinos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inquilinos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inquilino inquilino)
        {
            if (ModelState.IsValid)
            {
                ConexionDB db = new ConexionDB();
                db.AgregarInquilino(inquilino);  // Asegúrate de implementar el método AgregarInquilino en tu clase de conexión a la base de datos
                ViewBag.Mensaje = "Inquilino agregado exitosamente.";
                return RedirectToAction("Index");
            }
            return View(inquilino);
        }

        // GET: Inquilinos/Edit/5
        public ActionResult Edit(int id)
        {
            ConexionDB db = new ConexionDB();
            Inquilino inquilino = db.ObtenerInquilinoPorId(id);  // Implementa este método para obtener un inquilino por su ID
            if (inquilino == null)
            {
                return HttpNotFound();
            }
            return View(inquilino);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Inquilino inquilino)
        {
            if (ModelState.IsValid)
            {
                ConexionDB db = new ConexionDB();
                db.ActualizarInquilino(inquilino);  // Implementa este método para actualizar un inquilino en la base de datos
                ViewBag.Mensaje = "Inquilino actualizado exitosamente.";
                return RedirectToAction("Index");
            }
            return View(inquilino);
        }

        // GET: Inquilinos/Delete/5
        public ActionResult Delete(int id)
        {
            ConexionDB db = new ConexionDB();
            Inquilino inquilino = db.ObtenerInquilinoPorId(id);  // Implementa este método para obtener un inquilino por su ID
            if (inquilino == null)
            {
                return HttpNotFound();
            }
            return View(inquilino);
        }

        // POST: Inquilinos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ConexionDB db = new ConexionDB();
            db.EliminarInquilino(id);  // Implementa este método para eliminar un inquilino de la base de datos
            ViewBag.Mensaje = "Inquilino eliminado exitosamente.";
            return RedirectToAction("Index");
        }
    }
}
