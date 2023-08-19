using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonaClassLibrary;

namespace Persona.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JournalController : Controller
{
    private readonly JournalDBContext _context;

    public JournalController(JournalDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<JournalModel>>> GetJournal()
    {
        return Ok(await _context.JournalList.ToListAsync());
    }
    
    // GET: by id
    [HttpGet("{id:int}")]
    public async Task<ActionResult<JournalModel>> GetJournalById(int id)
    {
        var journal = await _context.JournalList.FindAsync(id);

        if (journal == null)
        {
            return NotFound();
        }

        return journal;
    }

    [HttpPost]
    public async Task<ActionResult<JournalModel>> Create(JournalModel journalModel)
    {
        _context.Add(journalModel);
        await _context.SaveChangesAsync();
        return Ok(journalModel);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, JournalModel journalModel)
    {
        if (id != journalModel.JournalId)
            return BadRequest();

        _context.Entry(journalModel).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var journal = await _context.JournalList.FindAsync(id);
        if (journal == null)
        {
            return NotFound();
        }

        _context.JournalList.Remove(journal);
        await _context.SaveChangesAsync();

        return Ok();
    }

}