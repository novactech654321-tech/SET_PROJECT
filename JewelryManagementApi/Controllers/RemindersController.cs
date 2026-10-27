using JewelryManagementApi.Data;
using JewelryManagementApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JewelryManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RemindersController : ControllerBase
{
    private readonly AppDbContext _context;

    public RemindersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Reminder>>> GetReminders()
    {
        return await _context.Reminders
            .OrderBy(x => x.ReminderId)
            .ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Reminder>> GetReminder(int id)
    {
        var item = await _context.Reminders.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        return item;
    }

    [HttpPost]
    public async Task<ActionResult<Reminder>> PostReminder(Reminder item)
    {
        _context.Reminders.Add(item);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetReminder), new { id = item.ReminderId }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutReminder(int id, Reminder item)
    {
        if (id != item.ReminderId)
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
            if (!await _context.Reminders.AnyAsync(e => e.ReminderId == id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteReminder(int id)
    {
        var item = await _context.Reminders.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        _context.Reminders.Remove(item);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
