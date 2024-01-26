using ToDoApi.Data.Models;
using ToDoApi.DTOs;

namespace ToDoApi.Interfaces
{
    public interface IToDoItemService
    {
        public Task<ToDoItemCreateResponseDTO> CreateToDoItem(ToDoItemCreateRequestDTO request, string email); 
       

    }
}
