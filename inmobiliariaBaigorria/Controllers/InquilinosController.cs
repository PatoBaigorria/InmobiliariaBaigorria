using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using inmobiliariaBaigorria.Models;

namespace inmobiliariaBaigorria.Controllers
{
    public class InquilinosController : Controller
    {
        private RepositorioInquilino repo = new RepositorioInquilino();
        // GET: Inquilinos
        public ActionResult Index()
        {
            return View(repo.ObtenerInquilinos());
        }

        // GET: Inquilinos/Details/5
        public ActionResult Details(int id)
        {
            return View(repo.ObtenerPorId(id));
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
            try
            {
                repo.Alta(inquilino);
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        // GET: Inquilinos/Edit/5
        public ActionResult Edit(int id)
        {
            return View(repo.ObtenerPorId(id));
        }

        // POST: Inquilinos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Inquilino inquilino)
        {
            try
            {
                // TODO: Add update logic here
                Inquilino i = repo.ObtenerPorId(id);

                i.Dni = inquilino.Dni;
                i.NombreCompleto = inquilino.NombreCompleto;
                i.Direccion = inquilino.Direccion;
                i.Email = inquilino.Email;
                i.Telefono = inquilino.Telefono;

                repo.ModificarInquilino(i);


                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Maneja la excepción o imprime detalles para depuración
                Console.WriteLine(ex.ToString());
                return View();
            }
        }

        // GET: Inquilinos/Delete/5
        public ActionResult Delete(int id)
        {
            return View(repo.ObtenerPorId(id));
        }

        // POST: Inquilinos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                repo.EliminarInquilino(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}