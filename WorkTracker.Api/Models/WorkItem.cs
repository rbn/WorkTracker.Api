using System.ComponentModel.DataAnnotations;

namespace WorkTracker.Api.Models
{
    public class WorkItem
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;
        public bool IsComplete { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
