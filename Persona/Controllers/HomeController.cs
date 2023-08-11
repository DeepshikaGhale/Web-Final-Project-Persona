using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Persona.Models;
using PersonaClassLibrary;

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
        List<JournalModel> journals = GetLatestJournalList();
        return View(journals);
    }

    // Get the latest list of journal entries from the static list
    private List<JournalModel> GetLatestJournalList()
    {
        // In this example, we simply return the existing dummyJournals list.
        // You can replace this with your own logic to fetch data from a database or other sources.
        return journals;
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
    public IActionResult PostJournal(JournalModel journal)
    {
        journal.CreatedDate = DateTime.Now;
        journal.UserEnteredDate = DateTime.Now;

        // Perform any validation you need, for example:
        if (string.IsNullOrEmpty(journal.JournalName) || string.IsNullOrEmpty(journal.Description))
        {
            ModelState.AddModelError("", "Journal Name and Description are required.");
            return View(journal); // Return to the form with validation errors if necessary.
        }

        // Generate a unique JournalID (you might need to adjust this based on your data source).
        int newJournalID = journals.Count + 1;
        journal.JournalID = newJournalID;

        // Add the new journal to the list.
        journals.Add(journal);
      

        // Redirect back to the home page(assuming you have an 'Index' action in the 'HomeController').
        return RedirectToAction("Index", journals);
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

