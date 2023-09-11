using inmobiliariaBaigorria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace inmobiliariaBaigorria.Controllers
{
    public class InmueblesController : Controller
    {
        private RepositorioPropietario repoP = new RepositorioPropietario();
        private RepositorioInmueble repoI = new RepositorioInmueble();
        // GET: Inmuebles
        public ActionResult Index()
        {
            ViewBag.Id = TempData["Id"];
            return View(repoI.ObtenerTodosLosInmuebles());
        }

        // GET: Inmuebles/Details/5
        public ActionResult Details(int id)
        {
            return View(repoI.ObtenerPorId(id));
        }

        // GET: Inmuebles/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.Propietario = repoP.ObtenerPropietarios();
                return View();
            }
            catch (System.Exception)
            {
                ModelState.AddModelError(string.Empty, "Ha ocurrido un error al cargar la página.");
                return View();
            }
        }

        // POST: Inmuebles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inmueble inmueble)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repoI.Alta(inmueble);
                    TempData["Id"] = inmueble.Id;
                    return RedirectToAction(nameof(Index));

                }
                else
                    return View(inmueble);

            }
            catch (System.Exception)
            {
                ModelState.AddModelError(string.Empty, "Ha ocurrido un error al guardar el inmueble.");
                return View(inmueble); // Retorna la vista con el modelo para que el usuario pueda corregir la entrada.
            }
        }

        // GET: Inmuebles/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var aux = repoI.ObtenerPorId(id);
                ViewBag.Propietario = repoP.ObtenerPropietarios();
                return View(aux);
            }
            catch (System.Exception)
            {
                ModelState.AddModelError(string.Empty, "Ha ocurrido un error al cargar la página.");
                return View();
            }

        }

        // POST: Inmuebles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Inmueble inmueble)
        {
            try
            {
                Inmueble i = repoI.ObtenerPorId(id);

                i.Tipo = inmueble.Tipo;
                i.Uso = inmueble.Uso;
                i.Ambientes = inmueble.Ambientes;
                i.Longitud = inmueble.Longitud;
                i.Latitud = inmueble.Latitud;
                i.Precio = inmueble.Precio;
                i.Estado = inmueble.Estado;
                i.Duenio = inmueble.Duenio;

                repoI.ModificarInmueble(i);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Maneja la excepción o imprime detalles para depuración
                Console.WriteLine(ex.ToString());
                return View();
            }
        }

        // GET: Inmuebles/Delete/5
        public ActionResult Delete(int id)
        {
            return View(repoI.ObtenerPorId(id));
        }

        // POST: Inmuebles/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                repoI.EliminarInmueble(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}