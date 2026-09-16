using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkTracker.Api.Data;
using WorkTracker.Api.Models;

namespace WorkTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkItemsController : ControllerBase
{
    private readonly WorkTrackerDbContext _context;

    public WorkItemsController(WorkTrackerDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        // Use _context.WorkItems here.
        // Read all rows asynchronously.
        // Return them with Ok(...).

        var workitems = await _context.WorkItems.ToListAsync();
        return Ok(workitems);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        // Your task:
        // 1. Find the WorkItem with this id asynchronously.
        // 2. If it doesn't exist, return NotFound().
        // 3. Otherwise return Ok(workItem).

        var workItem = await _context.WorkItems.FirstOrDefaultAsync(x => x.Id == id);
        if (workItem == null)
        {
            return NotFound();
        }

        return Ok(workItem);
    }

    [HttpPost]
    public async Task<IActionResult> Create(WorkItem workItem)
    {
        // 1. Add the work item to the DbContext
        // 2. Save changes asynchronously
        // 3. Return an appropriate response

        _context.WorkItems.Add(workItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
           nameof(GetById),
           new { id = workItem.Id },
           workItem);
    }
}