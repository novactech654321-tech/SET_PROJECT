using JewelryManagementApi.Data;
using JewelryManagementApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JewelryManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JewelleriesController : ControllerBase
{
    private readonly AppDbContext _context;

    public JewelleriesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Jewellery>>> GetJewelleries()
    {
        return await _context.Jewelleries
            .Include(x => x.Vendor)
            .OrderBy(x => x.JewelId)
            .ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Jewellery>> GetJewellery(int id)
    {
        var item = await _context.Jewelleries
            .Include(x => x.Vendor)
            .FirstOrDefaultAsync(x => x.JewelId == id);

        if (item == null)
        {
            return NotFound();
        }

        return item;
    }

    [HttpPost]
    public async Task<ActionResult<Jewellery>> PostJewellery(Jewellery item)
    {
        _context.Jewelleries.Add(item);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetJewellery), new { id = item.JewelId }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutJewellery(int id, Jewellery item)
    {
        if (id != item.JewelId)
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
            if (!await _context.Jewelleries.AnyAsync(e => e.JewelId == id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteJewellery(int id)
    {
        var item = await _context.Jewelleries.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        _context.Jewelleries.Remove(item);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
