using JewelryManagementApi.Data;
using JewelryManagementApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JewelryManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginLogsController : ControllerBase
{
    private readonly AppDbContext _context;

    public LoginLogsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LoginLog>>> GetLoginLogs()
    {
        return await _context.LoginLogs
            .OrderBy(x => x.LoginLogId)
            .ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<LoginLog>> GetLoginLog(int id)
    {
        var item = await _context.LoginLogs.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        return item;
    }

    [HttpPost]
    public async Task<ActionResult<LoginLog>> PostLoginLog(LoginLog item)
    {
        _context.LoginLogs.Add(item);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetLoginLog), new { id = item.LoginLogId }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutLoginLog(int id, LoginLog item)
    {
        if (id != item.LoginLogId)
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
            if (!await _context.LoginLogs.AnyAsync(e => e.LoginLogId == id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteLoginLog(int id)
    {
        var item = await _context.LoginLogs.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        _context.LoginLogs.Remove(item);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
