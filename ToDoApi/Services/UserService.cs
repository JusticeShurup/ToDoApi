using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoApi.Data.Models;
using ToDoApi.DTOs;
using ToDoApi.Interfaces;

namespace ToDoApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService([FromServices] IUserRepository userRepository, [FromServices] IConfiguration configuration) 
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<RegisterResponseDTO> Register(AuthRequestDTO request) 
        {

            User user;
            
            try
            {
                user = await _userRepository.CreateUserAsync(request.Email, request.Password);
            } 
            catch (InvalidOperationException) 
            {
                throw;
            }

            if (user == null)
            {
                throw new Exception("User cant");
            }

            var claims = new List<Claim> { new Claim(ClaimTypes.Email, user.Email)};

            var accessToken = CreateJwtAccessToken(claims);
            var refreshToken = CreateJwtRefreshToken(claims);

            user.RefreshToken = refreshToken;
            await _userRepository.UpdateUserAsync(user);

            var response = new RegisterResponseDTO { User = user, AccessToken = accessToken };

            return response;
        }

        public async Task<AuthResponseDTO> Login(AuthRequestDTO request)
        {
            User? user = await _userRepository.GetUserByEmailAsync(request.Email);

            if (user == null)
            {
                throw new Exception("User didn't not found");
            }


            var claims = new List<Claim> { new Claim(ClaimTypes.Email, user.Email) };

            var response = new AuthResponseDTO { Id = user.Id, AccessToken = CreateJwtAccessToken(claims) };

            return response;
        }

        public async Task<AuthResponseDTO> Refresh(string refreshToken)
        {
            JwtSecurityToken token = new JwtSecurityTokenHandler().ReadJwtToken(refreshToken);


            string? email = token.Payload.Claims.FirstOrDefault(x => x.Type.ToString() == ClaimTypes.Email)?.Value;

            if (email == null && !new EmailAddressAttribute().IsValid(email))
            {
                throw new NullReferenceException("Token signature is invalid");
            }

            User? user = await _userRepository.GetUserByEmailAsync(email!);

            if (user == null) 
            {
                throw new NullReferenceException("User with this Email not found");
            }

            List<Claim> claims = new List<Claim> { new Claim(ClaimTypes.Email, user.Email) };

            var response = new AuthResponseDTO { Id = user.Id, AccessToken = CreateJwtAccessToken(claims) };

            return response;
        }

        private string CreateJwtAccessToken(List<Claim> claims)
        {
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtToken:Issuer"]!,
                audience: _configuration["JwtToken:Audience"]!,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddSeconds(double.Parse(_configuration["JwtToken:AccessTokenLifetimeSeconds"]!)),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtToken:SecretKey"]!)), SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token)!;
        }

        private string CreateJwtRefreshToken(List<Claim> claims)
        {
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtToken:Issuer"]!,
                audience: _configuration["JwtToken:Audience"]!,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(double.Parse(_configuration["JwtToken:RefreshTokenLifetimeDays"]!)),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtToken:SecretKey"]!)), SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token)!;
        }

    }
}
