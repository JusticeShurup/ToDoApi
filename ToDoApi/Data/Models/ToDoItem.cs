using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoApi.Data.Models.Enum;

namespace ToDoApi.Data.Models
{
    [Table("ToDoItems")]
    public class ToDoItem
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public required string Title { get; set; }
        
        public string? Description { get; set; }

        public ToDoItemStatusType Status { get; set; } 

        public required DateTime CreatedDate { get; set; }

        [Required]
        public required Guid UserId;

        [ForeignKey("UserId")]
        [Required]
        public required User User { get; set; }
    
    }
}
