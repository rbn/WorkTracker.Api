using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using WorkTracker.Api.Dtos;
using WorkTracker.Api.Services;

namespace WorkTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkItemsController : ControllerBase
{
    private readonly IWorkItemService _workItemService;

    public WorkItemsController(IWorkItemService workItemService)
    {
        _workItemService = workItemService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        // Use _context.WorkItems here.
        // Read all rows asynchronously.
        // Return them with Ok(...).

        var workitems = await _workItemService.GetAllAsync();
        return Ok(workitems);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        // Your task:
        // 1. Find the WorkItem with this id asynchronously.
        // 2. If it doesn't exist, return NotFound().
        // 3. Otherwise return Ok(workItem).

        var workItem = await _workItemService.GetByIdAsync(id);

        if (workItem == null)
        {
            return NotFound();
        }
        return Ok(workItem);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateWorkItemRequest request)
    {
        // 1. Add the work item to the DbContext
        // 2. Save changes asynchronously
        // 3. Return an appropriate response

        var createdWorkItem = await _workItemService.CreateAsync(request);
        return CreatedAtAction(
           nameof(GetById),
           new { id = createdWorkItem.Id },
           createdWorkItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateWorkItemRequest request)
    {
        // 1. Find the existing item by id.
        // 2. If it doesn't exist, return NotFound().
        // 3. Copy the fields you want to allow updating.
        // 4. SaveChangesAsync().
        // 5. Return NoContent().
        var existingWorkItem = await _workItemService.UpdateAsync(id, request);

        if (existingWorkItem == null)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deletedWorkItem = await _workItemService.DeleteAsync(id);

        if (deletedWorkItem == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}