namespace ToDoApi.DTOs
{
    public sealed class AuthResponseDTO
    {
        public required Guid Id { get; init; }
        public required string AccessToken { get; init; }

    }
}
