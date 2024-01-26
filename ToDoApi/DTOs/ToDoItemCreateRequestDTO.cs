using System.ComponentModel.DataAnnotations;

namespace ToDoApi.DTOs
{
    public sealed class ToDoItemCreateRequestDTO
    {
        [Required]
        public required string Title { get; init; }
        
        public string? Description { get; init; }


    }
}
