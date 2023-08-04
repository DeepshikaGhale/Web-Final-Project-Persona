using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Persona.Models;

namespace Persona.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        List<JournalModel> journals = new List<JournalModel> {
            new JournalModel{
                JournalID = 1,
                JournalName = "My First Journal",
                Description = "This is my first journal entry.",
                UserEnteredDate = DateTime.Now,
                CreatedDate = DateTime.Now
            },
            new JournalModel
            {
                JournalID = 2,
                JournalName = "Second Journal",
                Description = "Another journal entry.",
                UserEnteredDate = DateTime.Now.AddDays(-3),
                CreatedDate = DateTime.Now.AddDays(-4)
            }
        };

        ViewBag.JournalModel = journals;
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

