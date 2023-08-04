using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Persona.Models;

namespace Persona.Controllers;

public class HomeController : Controller
{
    //list of dummy journals
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

    private readonly ILogger<HomeController> _logger; 

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        //passing the value of journals to the index page
        ViewBag.JournalModel = journals;
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult JournalDetails(int id)
    {
        var journal = journals.FirstOrDefault(j => j.JournalID == id);
        if (journal == null)
        {
            return NotFound();
        }
        return View(journal);
    }

    public IActionResult PostJournal() {
        return View();
    }

    [HttpPost]
    public IActionResult DeleteJournal(int id)
    {
        var journal = journals.FirstOrDefault(j => j.JournalID == id);

        if (journal == null)
        {
            return NotFound();
        }

        //remove journal from the list
        journals.Remove(journal);

        //returning result in json format indicating the data has been deleted successfully
        return Json(new { success = true, message = "Journal deleted successfully." });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

