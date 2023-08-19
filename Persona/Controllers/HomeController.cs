using System;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Persona.Models;
using PersonaClassLibrary;

namespace Persona.Controllers;

public class HomeController : Controller
{   
    //api setup
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _httpClient;

    private readonly ILogger<HomeController> _logger; 
    
    //variable to store id
    int JournalID = 0;

    public HomeController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _httpClient = _httpClientFactory.CreateClient("PersonaAPIClient");
    }
    
    //dashboard
    public async Task<IActionResult> Index()
    {
        //passing the value of journals to the index page
        var response = await _httpClient.GetAsync("api/Journal");
        var journalList = await response.Content.ReadFromJsonAsync<List<JournalModel>>();
        return View(journalList);
    }
    
    public IActionResult Login()
    {
        return View();
    }
    
    //details screen
    public async Task<IActionResult> JournalDetails(int id)
    {
        var response = await _httpClient.GetAsync($"api/Journal/{id}");
        var journalDetails = await response.Content.ReadFromJsonAsync<JournalModel>();
       
        if (journalDetails == null)
        {
            return NotFound();
        }
        return View(journalDetails);
    }
    
    //create journal
    [HttpGet]
    public IActionResult PostJournal()
    {
        JournalModel journalModel = new JournalModel();
        return View(journalModel);
    }

    //create journal 
    [HttpPost]
    public async Task<IActionResult> PostJournal(JournalModel journal)
    {
        //do the API Create Action and send the control to Index Action
        journal.CreatedDate = DateTime.Now;
        journal.UserEnteredDate = DateTime.Now;
        
        // Perform any validation you need, for example:
        if (string.IsNullOrEmpty(journal.JournalName) || string.IsNullOrEmpty(journal.Description))
        {
            ModelState.AddModelError("", "Journal Name and Description are required.");
            return View(journal); // Return to the form with validation errors if necessary.
        }
        
        var json = JsonSerializer.Serialize(journal);
        
        var response = await _httpClient.PostAsync(
            $"api/Journal/", 
            new StringContent(json, Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
        {
            var result = response.Content.ReadAsStringAsync().Result;
            var data = JsonSerializer.Deserialize<JournalModel>(result);
            if (data != null)
            {
                var journalData = data;
            }
        }
        else
        {
                var result = response.Content.ReadAsStringAsync().Result;
                throw new Exception("error" + result);

        }
        // Redirect back to the home page(assuming you have an 'Index' action in the 'HomeController').
        return RedirectToAction("Index", journal);
    }

    [HttpGet]
    public  IActionResult DeleteJournal(int id)
    {
       
        
        /*
        var journal = journals.FirstOrDefault(j => j.JournalId == id);

        if (journal == null)
        {
            return NotFound();
        }

        //remove journal from the list
        journals.Remove(journal);
        */
        //returning result in json format indicating the data has been deleted successfully
        return Json(new { success = true, message = "Journal deleted successfully." });
       //return View(journalModel);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/Journal/{id}");
        if (!response.IsSuccessStatusCode)
        {
            var result = response.Content.ReadAsStringAsync().Result;
            throw new Exception("error" + result);
        }
        
        //var script = "alert('Item deleted successfully');";
        //return Content(script, "application/javascript");
        return RedirectToAction("Index");
    }

    //edit
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        JournalID = id;
        var response = await _httpClient.GetAsync($"api/Journal/{id}");
        var journalDetails = await response.Content.ReadFromJsonAsync<JournalModel>();
       
        if (journalDetails == null)
        {
            return NotFound();
        }
        //fetch the journal modal data from the api and show the journal details in the Edit view
        return View(journalDetails);
    }
    
    //edit
    [HttpPost]
    public async Task<IActionResult> Edit(JournalModel journal)
    {
        var id = journal.JournalId;
        
        // Perform any validation you need, for example:
        if (string.IsNullOrEmpty(journal.JournalName) || string.IsNullOrEmpty(journal.Description))
        {
            ModelState.AddModelError("", "Journal Name and Description are required.");
            return View(journal); // Return to the form with validation errors if necessary.
        }
        
        var json = JsonSerializer.Serialize(journal);
        
        var response = await _httpClient.PutAsync(
            $"api/Journal/{id}", 
            new StringContent(json, Encoding.UTF8, "application/json"));

        if (!response.IsSuccessStatusCode)
        {
            var result = response.Content.ReadAsStringAsync().Result;
            throw new Exception("error" + result);

        }

        // Redirect back to the home page(assuming you have an 'Index' action in the 'HomeController').
        return RedirectToAction("Index", journal);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

