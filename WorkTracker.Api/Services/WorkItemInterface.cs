using WorkTracker.Api.Dtos;

namespace WorkTracker.Api.Services
{
    public interface IWorkItemService
    {
        Task<List<WorkItemResponse>> GetAllAsync();
        Task<WorkItemResponse?> GetByIdAsync(int id);

        Task<WorkItemResponse> CreateAsync(CreateWorkItemRequest request);
        Task<WorkItemResponse?> UpdateAsync(int id, UpdateWorkItemRequest request);
        Task<WorkItemResponse?> DeleteAsync(int id);
    }
}
