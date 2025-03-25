using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using practicoInmobiliaria.Models;


namespace practicoInmobiliaria.Controllers
{
    public class PropietariosController : Controller
    {
        // Acción que obtiene los propietarios y los pasa a la vista
        public ActionResult Index()
        {
            ConexionDB db = new ConexionDB();
            List<Propietario> propietarios = db.ObtenerPropietarios();  // Obtén los propietarios de la base de datos

            // Depuración: Verifica los datos
            foreach (var propietario in propietarios)
            {
                Console.WriteLine($"Propietario ID: {propietario.IdPropietario}, Nombre: {propietario.NombrePropietario}, Apellido: {propietario.ApellidoPropietario}");
            }

            // Aquí pasamos el mensaje que se generó al intentar abrir la conexión
          //  if (string.IsNullOrEmpty(ViewBag.Mensaje))
            //{
           //     ViewBag.Mensaje = "Conexión exitosa a la base de datos.";
           // }

            return View(propietarios);  // Pasa la lista de propietarios a la vista
        }

        // GET: Propietarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Propietarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Propietario propietario)
        {
            if (ModelState.IsValid)
            {
                ConexionDB db = new ConexionDB();
                db.AgregarPropietario(propietario);  // Asegúrate de implementar el método AgregarPropietario en tu clase de conexión a la base de datos
                ViewBag.Mensaje = "Propietario agregado exitosamente.";
                return RedirectToAction("Index");
            }
            return View(propietario);
        }
        // GET: Propietarios/Edit/5
        public ActionResult Edit(int id)
        {
            ConexionDB db = new ConexionDB();
            Propietario propietario = db.ObtenerPropietarioPorId(id);  // Implementa este método para obtener un propietario por su ID
            if (propietario == null)
            {
                return HttpNotFound();
            }
            return View(propietario);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Propietario propietario)
        {
            if (ModelState.IsValid)
            {
                ConexionDB db = new ConexionDB();
                db.ActualizarPropietario(propietario);  // Implementa este método para actualizar un propietario en la base de datos
                ViewBag.Mensaje = "Propietario actualizado exitosamente.";
                return RedirectToAction("Index");
            }
            return View(propietario);
        }

        // GET: Propietarios/Delete/5
        public ActionResult Delete(int id)
        {
            ConexionDB db = new ConexionDB();
            Propietario propietario = db.ObtenerPropietarioPorId(id);  // Implementa este método para obtener un propietario por su ID
            if (propietario == null)
            {
                return HttpNotFound();
            }
            return View(propietario);
        }

        // POST: Propietarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ConexionDB db = new ConexionDB();
            db.EliminarPropietario(id);  // Implementa este método para eliminar un propietario de la base de datos
            ViewBag.Mensaje = "Propietario eliminado exitosamente.";
            return RedirectToAction("Index");
        }

    }
}
