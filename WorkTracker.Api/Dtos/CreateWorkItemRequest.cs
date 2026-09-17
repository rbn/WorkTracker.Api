using System.ComponentModel.DataAnnotations;

namespace WorkTracker.Api.Dtos

{
    public class CreateWorkItemRequest
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;
    }
}
