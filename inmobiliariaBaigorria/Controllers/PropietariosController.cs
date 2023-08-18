using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using inmobiliariaBaigorria.Models;

namespace inmobiliariaBaigorria.Controllers;

public class PropietariosController : Controller
{
    private readonly ILogger<PropietariosController> _logger;

    public PropietariosController(ILogger<PropietariosController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        RepositorioPropietario repositorio = new RepositorioPropietario();
        List<Propietario> propietarios = repositorio.ObtenerPropietarios();
        return View(propietarios);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Propietario propietario)
    {
        try
        {
            RepositorioPropietario repositorio = new RepositorioPropietario();
            repositorio.Alta(propietario);
            return RedirectToAction("Index");
        }
        catch (System.Exception)
        {
            throw;
        }

    }

}
