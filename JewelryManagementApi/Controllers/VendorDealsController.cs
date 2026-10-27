using JewelryManagementApi.Data;
using JewelryManagementApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JewelryManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VendorDealsController : ControllerBase
{
    private readonly AppDbContext _context;

    public VendorDealsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VendorDeal>>> GetVendorDeals()
    {
        return await _context.VendorDeals
            .Include(x => x.Vendor)
            .Include(x => x.Jewellery)
            .OrderBy(x => x.VendorDealId)
            .ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<VendorDeal>> GetVendorDeal(int id)
    {
        var item = await _context.VendorDeals
            .Include(x => x.Vendor)
            .Include(x => x.Jewellery)
            .FirstOrDefaultAsync(x => x.VendorDealId == id);

        if (item == null)
        {
            return NotFound();
        }

        return item;
    }

    [HttpPost]
    public async Task<ActionResult<VendorDeal>> PostVendorDeal(VendorDeal item)
    {
        _context.VendorDeals.Add(item);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetVendorDeal), new { id = item.VendorDealId }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutVendorDeal(int id, VendorDeal item)
    {
        if (id != item.VendorDealId)
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
            if (!await _context.VendorDeals.AnyAsync(e => e.VendorDealId == id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteVendorDeal(int id)
    {
        var item = await _context.VendorDeals.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        _context.VendorDeals.Remove(item);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
