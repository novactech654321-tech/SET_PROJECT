using JewelryManagementApi.Data;
using JewelryManagementApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JewelryManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TransactionsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
    {
        return await _context.Transactions
            .Include(x => x.Jewellery)
            .OrderBy(x => x.TransactionId)
            .ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Transaction>> GetTransaction(int id)
    {
        var item = await _context.Transactions
            .Include(x => x.Jewellery)
            .FirstOrDefaultAsync(x => x.TransactionId == id);

        if (item == null)
        {
            return NotFound();
        }

        return item;
    }

    [HttpPost]
    public async Task<ActionResult<Transaction>> PostTransaction(Transaction item)
    {
        _context.Transactions.Add(item);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTransaction), new { id = item.TransactionId }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutTransaction(int id, Transaction item)
    {
        if (id != item.TransactionId)
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
            if (!await _context.Transactions.AnyAsync(e => e.TransactionId == id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTransaction(int id)
    {
        var item = await _context.Transactions.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        _context.Transactions.Remove(item);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
