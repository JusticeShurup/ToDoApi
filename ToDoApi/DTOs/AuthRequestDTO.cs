using System.ComponentModel.DataAnnotations;

namespace ToDoApi.DTOs
{
    public sealed class AuthRequestDTO
    {
        [Required]
        public required string Email { get; init; }
        
        [Required]  
        public required string Password { get; init; }
    }
}
