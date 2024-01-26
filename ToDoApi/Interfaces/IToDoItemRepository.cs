using ToDoApi.Data.Models;
using ToDoApi.DTOs;

namespace ToDoApi.Interfaces
{
    public interface IToDoItemRepository
    {
        public Task<ToDoItem> CreateToDoItemAsync(ToDoItemCreateRequestDTO request, string email);

        public Task<ToDoItem> GetToDoItemByIdAsync(Guid id);

        public Task<ToDoItem> GetToDoItemByUserAsync(User user);

        public Task UpdateToDoItemAsync(ToDoItem item);

        public Task DeleteToDoItemAsync(ToDoItem item);

        public Task<HashSet<ToDoItem>> GetAllUserToDoItem(User user);
    }
}
