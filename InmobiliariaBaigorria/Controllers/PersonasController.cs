using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using test.Models;

namespace test.Controllers;

public class PersonasController : Controller
{
    private readonly ILogger<PersonasController> _logger;

    public PersonasController(ILogger<PersonasController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

}
