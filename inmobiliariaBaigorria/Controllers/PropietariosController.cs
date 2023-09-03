using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using inmobiliariaBaigorria.Models;

namespace inmobiliariaBaigorria.Controllers
{
    public class PropietariosController : Controller
    {
        private RepositorioPropietario repositorio = new RepositorioPropietario();
        // GET: Propietarios
        public ActionResult Index()
        {


            return View(repositorio.ObtenerPropietarios());
        }

        // GET: Propietarios/Details/5
        public ActionResult Details(int id)
        {
            return View(repositorio.ObtenerPorId(id));
        }

        // GET: Propietarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Propietario/Create
        [HttpPost]
        public IActionResult Create(Propietario propietario)
        {
            try
            {
                repositorio.Alta(propietario);
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        // GET: Propietarios/Edit/5
        public ActionResult Edit(int id)
        {
            return View(repositorio.ObtenerPorId(id));
        }

        // POST: Propietarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Propietario propietario)
        {
            try
            {
                // TODO: Add update logic here


                Propietario p = repositorio.ObtenerPorId(id);

                p.Dni = propietario.Dni;
                p.Nombre = propietario.Nombre;
                p.Apellido = propietario.Apellido;
                p.Direccion = propietario.Direccion;
                p.Email = propietario.Email;
                p.Telefono = propietario.Telefono;

                repositorio.ModificarPropietario(p);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Maneja la excepción o imprime detalles para depuración
                Console.WriteLine(ex.ToString());
                return View();
            }
        }

        // GET: Propietarios/Delete/5
        public ActionResult Delete(int id)
        {
            return View(repositorio.ObtenerPorId(id));
        }

        // POST: Propietarios/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                repositorio.EliminarPropietario(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}