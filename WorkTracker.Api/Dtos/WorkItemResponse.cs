namespace WorkTracker.Api.Dtos
{
    public class WorkItemResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsComplete { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
