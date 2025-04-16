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
        private ConexionDB conexionDB = new ConexionDB(); // o por inyección si usás DI

        [HttpGet]
        public JsonResult ObtenerPorDni(string dni)
        {
            try
            {
                int dniInt = int.Parse(dni); // conversión de string a int
                ConexionDB conexion = new ConexionDB();
                var propietario = conexion.ObtenerPropietarioPorDni(dniInt); // ahora sí le pasás un int

                if (propietario != null)
                {
                    return Json(new
                    {
                        dniPropietario = propietario.DniPropietario,
                        apellidoPropietario = propietario.ApellidoPropietario,
                        nombrePropietario = propietario.NombrePropietario,
                        contactoPropietario = propietario.ContactoPropietario
                    }, JsonRequestBehavior.AllowGet);
                }

                return Json(null, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

       
        [HttpGet]
        public JsonResult Buscar(string termino)
        {
            var propietarios = conexionDB.BuscarPropietarios(termino)
                .Where(p => p.DniPropietario.Contains(termino) || p.NombrePropietario.Contains(termino) || p.ApellidoPropietario.Contains(termino))
                .Select(p => new { p.DniPropietario, p.NombrePropietario, p.ApellidoPropietario })
                .ToList();

            return Json(propietarios, JsonRequestBehavior.AllowGet);
        }








    }


}
