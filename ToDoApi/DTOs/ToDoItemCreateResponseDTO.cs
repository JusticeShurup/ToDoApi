using System.ComponentModel.DataAnnotations;

namespace ToDoApi.DTOs
{
    public sealed class ToDoItemCreateResponseDTO
    {
        [Required]
        public required Guid Id { get; set; }

        [Required]
        public required string Title { get; set; } 

        public string? Description { get; set; }

        [Required]  
        public required DateTime CreateTime { get; set; } 

        
    }
}
