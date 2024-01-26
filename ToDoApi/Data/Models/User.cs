using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ToDoApi.Data.Models
{
    [Table("Users")]
    public class User
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        public bool IsEmailConfirmed { get; set; } = false;

        [Required]
        public required string Password { get; set; }

        public string? Name { get; set; }

        public string? RefreshToken { get; set; }      

        public List<ToDoItem> ToDoItems { get; set; }

    }
}
