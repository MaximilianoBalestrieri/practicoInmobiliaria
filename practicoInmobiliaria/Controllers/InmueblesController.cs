using System.Web.Mvc;
using practicoInmobiliaria.Models;
using System.Collections.Generic;
using System.Web;
using System;
using System.IO;
using System.Linq;

using static practicoInmobiliaria.Models.ConexionDB;


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

        public ActionResult Edit(int id)
        {
            var inmueble = conexionDB.ObtenerInmueblePorId(id);
            var fotos = conexionDB.ObtenerFotosCarruselPorInmueble(id); // Este debe devolver una lista de objetos InmuebleFotoCarrusel.

            var viewModel = new InmuebleEditViewModel
            {
                Inmueble = inmueble,
                RutasCarrusel = fotos // Aquí pasas la lista de objetos InmuebleFotoCarrusel.
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InmuebleEditViewModel model, HttpPostedFileBase FotoPortada, IEnumerable<HttpPostedFileBase> FotosCarrusel)
        {
            System.Diagnostics.Debug.WriteLine("¿ModelState válido?: " + ModelState.IsValid);

            if (ModelState.IsValid)
            {
                try
                {
                    string rutaOriginalPortada = model.Inmueble.ImagenPortada; // respaldo

                    // ✅ Procesar imagen de portada
                    if (FotoPortada != null && FotoPortada.ContentLength > 0)
                    {
                        System.Diagnostics.Debug.WriteLine("Archivo recibido: " + FotoPortada.FileName);

                        // Eliminar imagen anterior
                        if (!string.IsNullOrEmpty(model.Inmueble.ImagenPortada))
                        {
                            var rutaAnterior = Server.MapPath(model.Inmueble.ImagenPortada);
                            if (System.IO.File.Exists(rutaAnterior))
                            {
                                System.IO.File.Delete(rutaAnterior);
                            }
                        }

                        // Guardar nueva imagen
                        string extension = Path.GetExtension(FotoPortada.FileName);
                        string nombreArchivo = Path.GetFileNameWithoutExtension(FotoPortada.FileName);
                        string nombreUnico = $"{nombreArchivo}_{Guid.NewGuid()}{extension}";
                        string rutaRelativa = "~/Imagenes/Inmuebles/" + nombreUnico;
                        string rutaFisica = Server.MapPath(rutaRelativa);

                        Directory.CreateDirectory(Path.GetDirectoryName(rutaFisica));
                        FotoPortada.SaveAs(rutaFisica);

                        model.Inmueble.ImagenPortada = rutaRelativa;
                    }
                    else
                    {
                        // ✅ Si no subió una nueva imagen, mantenemos la original
                        model.Inmueble.ImagenPortada = rutaOriginalPortada;
                    }

                    // ✅ Guardar imágenes del carrusel
                    if (FotosCarrusel != null)
                    {
                        foreach (var foto in FotosCarrusel)
                        {
                            if (foto != null && foto.ContentLength > 0)
                            {
                                string extension = Path.GetExtension(foto.FileName);
                                string nombreArchivo = Path.GetFileNameWithoutExtension(foto.FileName);
                                string nombreUnico = $"{nombreArchivo}_{Guid.NewGuid()}{extension}";
                                string rutaRelativa = "~/Imagenes/Carrusel/" + nombreUnico;
                                string rutaFisica = Server.MapPath(rutaRelativa);

                                Directory.CreateDirectory(Path.GetDirectoryName(rutaFisica));
                                foto.SaveAs(rutaFisica);

                                conexionDB.InsertarFotoCarrusel(model.Inmueble.IdInmueble, rutaRelativa);
                            }
                        }
                    }

                    conexionDB.ActualizarInmueble(model.Inmueble);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error al guardar imagen: " + ex.Message);
                    ModelState.AddModelError("", "Ocurrió un error al guardar las imágenes.");
                    return View(model);
                }
            }

            return View(model);
        }




        // Acción para eliminar una foto del carrusel
        [HttpPost]
        public JsonResult EliminarFotoCarrusel(int idFoto)
        {
            System.Diagnostics.Debug.WriteLine("ID RECIBIDO: " + idFoto); // <-- esto

            var foto = conexionDB.ObtenerFotoCarruselPorId(idFoto);

            if (foto != null)
            {
                var rutaRelativa = foto.RutaFoto.StartsWith("~") ? foto.RutaFoto : "~" + foto.RutaFoto;
                var rutaFisica = Server.MapPath(rutaRelativa);
                if (System.IO.File.Exists(rutaFisica))
                {
                    System.IO.File.Delete(rutaFisica);
                }

                conexionDB.EliminarFotoCarrusel(idFoto);

                // Devolvemos JSON en lugar de redireccionar
                return Json(new { success = true });
            }

            return Json(new { success = false, mensaje = "No se encontró la foto." });
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection form)
        {
            conexionDB.EliminarInmueble(id);
            return RedirectToAction("Index");
        }

        // POST: Inmuebles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inmueble inmueble, HttpPostedFileBase FotoPortada, List<HttpPostedFileBase> fotosCarrusel)
        {
            if (ModelState.IsValid)
            {
                // Guardar la imagen de portada
                if (FotoPortada != null && FotoPortada.ContentLength > 0)
                {
                    string rutaPortada = System.IO.Path.Combine(Server.MapPath("~/Uploads/Portadas"), System.IO.Path.GetFileName(FotoPortada.FileName));
                    FotoPortada.SaveAs(rutaPortada);
                    inmueble.ImagenPortada = "/Uploads/Portadas/" + FotoPortada.FileName;
                }

                // Guardar imágenes del carrusel
                List<string> rutasCarrusel = new List<string>();
                if (fotosCarrusel != null && fotosCarrusel.Count > 0)
                {
                    foreach (var foto in fotosCarrusel)
                    {
                        if (foto != null && foto.ContentLength > 0)
                        {
                            string ruta = System.IO.Path.Combine(Server.MapPath("~/Uploads/Carrusel"), System.IO.Path.GetFileName(foto.FileName));
                            foto.SaveAs(ruta);
                            rutasCarrusel.Add("/Uploads/Carrusel/" + foto.FileName);
                        }
                    }
                   // inmueble.FotosCarrusel = string.Join(";", rutasCarrusel);
                }

                // 👇 Aquí usamos la nueva versión que devuelve el ID generado
                int idGenerado = conexionDB.AgregarInmueble(inmueble);

                return RedirectToAction("Index");
            }

            return View(inmueble);
        }


        [HttpGet]
        public JsonResult BuscarPorDni(string dni)
        {
            var inmuebles = conexionDB.ObtenerInmuebles()
                .Where(i => i.DniPropietario == dni)
                .ToList();

            return Json(inmuebles);
        }

        [HttpGet]
        public ActionResult ObtenerDireccionesPorDni(string dni)
        {
            var inmuebles = conexionDB.ObtenerInmueblesPorDni(dni);

            var direcciones = inmuebles.Select(i => new
            {
                id = i.IdInmueble,
                direccion = $"{i.Calle} {i.Nro} " +
                            $"{(i.Piso != 0 ? $"Piso {i.Piso} " : "")}" +
                            $"{(!string.IsNullOrEmpty(i.Dpto) ? $"Dto. {i.Dpto} " : "")}" +
                            $", {i.Localidad}"
            }).ToList();

            return Json(direcciones, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult Ocupados(DateTime desde, DateTime hasta)
        {
            try
            {
                var lista = conexionDB.ObtenerInmueblesOcupados(desde, hasta);

                // Asegurarse de que siempre devolvemos una lista, incluso si está vacía
                return Json(lista ?? new List<Inmueble>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }







    }
}
