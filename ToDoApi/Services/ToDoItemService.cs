using Microsoft.AspNetCore.Mvc;
using ToDoApi.Data.Models;
using ToDoApi.DTOs;
using ToDoApi.Interfaces;

namespace ToDoApi.Services
{
    public class ToDoItemService : IToDoItemService
    {
        private readonly IToDoItemRepository _toDoItemRepository;


        public ToDoItemService([FromServices] IToDoItemRepository toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository;
        }

        public async Task<ToDoItemCreateResponseDTO> CreateToDoItem(ToDoItemCreateRequestDTO request, string email)
        {
            ToDoItem item;

            try
            {
                item = await _toDoItemRepository.CreateToDoItemAsync(request, email);
            } 
            catch (Exception)
            {
                throw;
            }

            var response = new ToDoItemCreateResponseDTO 
            {
                Id = item.Id, 
                Title = item.Title, 
                CreateTime = item.CreatedDate, 
                Description = item.Description 
            };

            return response;

        }
    }
}
