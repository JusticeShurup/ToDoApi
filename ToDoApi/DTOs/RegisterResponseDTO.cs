using ToDoApi.Data.Models;

namespace ToDoApi.DTOs
{
    public sealed class RegisterResponseDTO
    {
        public required User User { get; init; }
        public required string AccessToken { get; init; }

    }
}
