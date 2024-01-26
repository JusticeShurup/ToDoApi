using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Data;
using ToDoApi.Data.Models;
using ToDoApi.DTOs;

namespace ToDoApi.Interfaces
{

    public interface IUserService 
    {
        public Task<RegisterResponseDTO> Register(AuthRequestDTO request);

        public Task<AuthResponseDTO> Login(AuthRequestDTO request);

        public Task<AuthResponseDTO> Refresh(string refreshToken);

    
    }
}
