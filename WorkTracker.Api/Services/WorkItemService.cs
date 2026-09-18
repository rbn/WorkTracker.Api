using WorkTracker.Api.Data;
using WorkTracker.Api.Dtos;
using WorkTracker.Api.Models;
using WorkTracker.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace WorkTracker.Api.Services
{
    public class WorkItemService : IWorkItemService
    {
        private readonly WorkTrackerDbContext _context;

        public WorkItemService(WorkTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<List<WorkItemResponse>> GetAllAsync()
        {
            var workitems = await _context.WorkItems.Select(wi => new WorkItemResponse
            {
                Id = wi.Id,
                Title = wi.Title,
                Description = wi.Description,
                IsComplete = wi.IsComplete,
                CreatedAt = wi.CreatedAt
            }).ToListAsync();

            return workitems;
        }

        public async Task<WorkItemResponse?> GetByIdAsync(int id)
        {
            var workItem = await _context.WorkItems.Where(x => x.Id == id).Select(x => new WorkItemResponse
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                IsComplete = x.IsComplete,
                CreatedAt = x.CreatedAt
            }).FirstOrDefaultAsync();

            return workItem;
        }

        public async Task<WorkItemResponse> CreateAsync(CreateWorkItemRequest request)
        {
            var workItem = new WorkItem
            {
                Title = request.Title,
                Description = request.Description,
                IsComplete = false,
                CreatedAt = DateTime.UtcNow
            };
            _context.WorkItems.Add(workItem);
            await _context.SaveChangesAsync();
            return new WorkItemResponse
            {
                Id = workItem.Id,
                Title = workItem.Title,
                Description = workItem.Description,
                IsComplete = workItem.IsComplete,
                CreatedAt = workItem.CreatedAt
            };
        }

        public async Task<WorkItemResponse?> UpdateAsync(int id, UpdateWorkItemRequest request)
        {
            var workItem = await _context.WorkItems.FindAsync(id);
            if (workItem == null)
            {
              return null;
            }
            workItem.Title = request.Title;
            workItem.Description = request.Description;
            workItem.IsComplete = request.IsComplete;
            await _context.SaveChangesAsync();
            return new WorkItemResponse
            {
                Id = workItem.Id,
                Title = workItem.Title,
                Description = workItem.Description,
                IsComplete = workItem.IsComplete,
                CreatedAt = workItem.CreatedAt
            };
        }

        public async Task<WorkItemResponse?> DeleteAsync(int id)
        {
            var workItem = await _context.WorkItems.FindAsync(id);
            if (workItem == null)
            {
                return null;
            }
            _context.WorkItems.Remove(workItem);
            await _context.SaveChangesAsync();
            return new WorkItemResponse
            {
                Id = workItem.Id,
                Title = workItem.Title,
                Description = workItem.Description,
                IsComplete = workItem.IsComplete,
                CreatedAt = workItem.CreatedAt
            };
        }
    }
}
