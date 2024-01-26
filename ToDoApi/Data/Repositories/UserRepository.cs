using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ToDoApi.Data.Models;
using ToDoApi.Interfaces;

namespace ToDoApi.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserRepository(ApplicationContext context, IPasswordHasher<User> passwordHasher) 
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> CreateUserAsync(string email, string password)
        {
            User? existsUser = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);

            if (existsUser != null)
            {
                throw new InvalidOperationException("User already exists in database");
            }

            User user = new User { Email = email, Password = password};
            user.Password = _passwordHasher.HashPassword(user, password);


            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<HashSet<User>> GetAllUsers()
        {
            return (await _context.Users.ToListAsync()).ToHashSet();
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email) 
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            return user;
        }


        public async Task<bool> CheckPassword(User user, string providedPassword)
        {
            var databaseUser = await GetUserByEmailAsync(user.Email);
            if (databaseUser is null)
            {
                return false;
            }

            PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(user, user.Password, providedPassword);

            return result == PasswordVerificationResult.Success;
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);

            await _context.SaveChangesAsync();
        }
    }
}
