using JewelryManagementApi.Data;
using JewelryManagementApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JewelryManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HistoryOfEventsController : ControllerBase
{
    private readonly AppDbContext _context;

    public HistoryOfEventsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HistoryOfEvent>>> GetHistoryOfEvents()
    {
        return await _context.HistoryOfEvents
            .OrderBy(x => x.EventId)
            .ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<HistoryOfEvent>> GetHistoryOfEvent(int id)
    {
        var item = await _context.HistoryOfEvents.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        return item;
    }

    [HttpPost]
    public async Task<ActionResult<HistoryOfEvent>> PostHistoryOfEvent(HistoryOfEvent item)
    {
        _context.HistoryOfEvents.Add(item);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetHistoryOfEvent), new { id = item.EventId }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutHistoryOfEvent(int id, HistoryOfEvent item)
    {
        if (id != item.EventId)
        {
            return BadRequest();
        }

        _context.Entry(item).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.HistoryOfEvents.AnyAsync(e => e.EventId == id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteHistoryOfEvent(int id)
    {
        var item = await _context.HistoryOfEvents.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        _context.HistoryOfEvents.Remove(item);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
