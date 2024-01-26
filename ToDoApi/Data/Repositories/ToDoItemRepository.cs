using ToDoApi.Data.Models;
using ToDoApi.DTOs;
using ToDoApi.Interfaces;

namespace ToDoApi.Data.Repositories
{
    public class ToDoItemRepository : IToDoItemRepository
    {
        private readonly ApplicationContext _context;
        private readonly IUserRepository _userRepository;


        public ToDoItemRepository(ApplicationContext context, IUserRepository userRepository) 
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<ToDoItem> CreateToDoItemAsync(ToDoItemCreateRequestDTO requestDTO, string email)
        {
            User? user = await _userRepository.GetUserByEmailAsync(email);
        
            if (user == null)
            {
                throw new InvalidOperationException("User isn't exists");
            }

            ToDoItem item = new ToDoItem { 
                Title = requestDTO.Title, 
                UserId = user.Id, 
                User = user, 
                Description = requestDTO.Description,
                Status = Models.Enum.ToDoItemStatusType.NotStarted,
                CreatedDate = DateTime.UtcNow
            };

            await _context.AddAsync(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public Task DeleteToDoItemAsync(ToDoItem item)
        {
            throw new NotImplementedException();
        }

        public Task<HashSet<ToDoItem>> GetAllUserToDoItem(User user)
        {
            throw new NotImplementedException();
        }

        public Task<ToDoItem> GetToDoItemByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ToDoItem> GetToDoItemByUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateToDoItemAsync(ToDoItem item)
        {
            throw new NotImplementedException();
        }
    }
}
