using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ToDoApi.Data.Models;
using ToDoApi.Data.Repositories;
using ToDoApi.DTOs;
using ToDoApi.Interfaces;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private readonly IToDoItemService _toDoItemService;
        private readonly IUserRepository _userRepository;

        public ToDoItemController(IToDoItemService toDoItemService, IUserRepository userRepository)
        {
            _toDoItemService = toDoItemService;
            _userRepository = userRepository;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateToDoItem(ToDoItemCreateRequestDTO request)
        {

            string? email = HttpContext.User.Claims.FirstOrDefault(x => x.Type.ToString() == ClaimTypes.Email)?.Value;

            if (email == null || !new EmailAddressAttribute().IsValid(email))
            {
                return BadRequest("Token is invalid");
            }

            ToDoItemCreateResponseDTO response;
            try
            {
               response = await _toDoItemService.CreateToDoItem(request, email);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

                
            return new JsonResult(response) { StatusCode = StatusCodes.Status201Created};    
        }

    }
}
