using ToDoApi.Data.Models;

namespace ToDoApi.Interfaces
{
    
    public interface IUserRepository
    {
        public Task<User> CreateUserAsync(string email, string password);

        public Task<User> GetUserByIdAsync(Guid id);

        public Task<User> GetUserByEmailAsync(string email);

        public Task<bool> CheckPassword(User user, string hashedPassword);

        public Task UpdateUserAsync(User user);
        
        public Task DeleteUserAsync(User user);

        public Task<HashSet<User>> GetAllUsers();
        
        
    }
}
