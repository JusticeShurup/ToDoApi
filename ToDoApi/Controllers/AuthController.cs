using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using ToDoApi.Data.Models;
using ToDoApi.DTOs;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using ToDoApi.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ToDoApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        public AuthController([FromServices] IUserService userService)
            : base()
        {
            _userService = userService;
        }


        [Route("/register")]
        [HttpPost]
        /// <summary>
        /// StackOverflow example
        /// </summary>
        /// <response code="201">User succesfully created</response>
        public async Task<IActionResult> Register([FromBody] AuthRequestDTO request)
        {

            if (!new EmailAddressAttribute().IsValid(request.Email))
            {
                return BadRequest("Email is invalid");
            }

            RegisterResponseDTO response;
            try
            {
                response = await _userService.Register(request);

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return new JsonResult(response) { StatusCode = StatusCodes.Status201Created };
        }

        [Route("/login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthRequestDTO request)
        {
            if (!new EmailAddressAttribute().IsValid(request.Email))
            {
                return BadRequest("Email is invalid");
            }

            AuthResponseDTO response;

            try
            {
                response = await _userService.Login(request);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(response);
        }

        [Route("/refresh")]
        [HttpPost]
        public async Task<IActionResult> Refresh([FromBody] string refreshToken)
        {
            AuthResponseDTO response;
            try
            {
                response = await _userService.Refresh(refreshToken);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok(response);
        }
    }
}
