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

}
