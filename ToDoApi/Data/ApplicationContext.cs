using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Data.Models;

namespace ToDoApi.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users{ get; set; }

        public DbSet<ToDoItem> ToDoItems { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options) {}
    }
}
