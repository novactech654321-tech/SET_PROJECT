using JewelryManagementApi.Data;
using JewelryManagementApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JewelryManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BenefitsController : ControllerBase
{
    private readonly AppDbContext _context;

    public BenefitsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Benefit>>> GetBenefits()
    {
        return await _context.Benefits
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Benefit>> GetBenefit(int id)
    {
        var item = await _context.Benefits.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        return item;
    }

    [HttpPost]
    public async Task<ActionResult<Benefit>> PostBenefit(Benefit item)
    {
        _context.Benefits.Add(item);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBenefit), new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutBenefit(int id, Benefit item)
    {
        if (id != item.Id)
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
            if (!await _context.Benefits.AnyAsync(e => e.Id == id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteBenefit(int id)
    {
        var item = await _context.Benefits.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        _context.Benefits.Remove(item);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
