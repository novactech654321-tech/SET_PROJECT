using JewelryManagementApi.Data;
using JewelryManagementApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JewelryManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VendorsController : ControllerBase
{
    private readonly AppDbContext _context;

    public VendorsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Vendor>>> GetVendors()
    {
        return await _context.Vendors
            .Include(x => x.Jewelleries)
            .Include(x => x.Deals)
            .OrderBy(x => x.VendorId)
            .ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Vendor>> GetVendor(int id)
    {
        var item = await _context.Vendors
            .Include(x => x.Jewelleries)
            .Include(x => x.Deals)
            .FirstOrDefaultAsync(x => x.VendorId == id);

        if (item == null)
        {
            return NotFound();
        }

        return item;
    }

    [HttpPost]
    public async Task<ActionResult<Vendor>> PostVendor(Vendor item)
    {
        _context.Vendors.Add(item);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetVendor), new { id = item.VendorId }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutVendor(int id, Vendor item)
    {
        if (id != item.VendorId)
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
            if (!await _context.Vendors.AnyAsync(e => e.VendorId == id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteVendor(int id)
    {
        var item = await _context.Vendors.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        _context.Vendors.Remove(item);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
