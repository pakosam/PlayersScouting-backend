using Azure.Core;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using PlayersScouting_backend.DTOs;
using PlayersScouting_backend.Entities;
using PlayersScouting_backend.Persistence;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PlayersScouting_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly DataContext _context;
        public AuthorizationController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDto registrationDto)
        {
            if (_context.Scouts.Any(s => s.Email == registrationDto.Email))
                return Conflict(new { message = "Email already registered." });

            var today = DateOnly.FromDateTime(DateTime.Today);

            if (registrationDto.Birthdate == DateOnly.MinValue)
            {
                throw new NotImplementedException("Birth date is required");
            }

            var age = today.Year - registrationDto.Birthdate.Year;
            if (registrationDto.Birthdate > today.AddYears(-age))
            {
                age--;
            }

            if (age > 70)
            {
                throw new NotImplementedException("Scout cannot be older than 70 years.");
            }

            if (age < 18)
            {
                throw new NotImplementedException("Scout cannot be younger than 18 years.");
            }

            if (registrationDto.Name.Length < 3 ||
                registrationDto.Surname.Length < 3 ||
                registrationDto.Password.Length < 10)
            {
                throw new NotImplementedException("Data is not filled properly");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registrationDto.Password);

            var scout = new Scout
            {
                Name = registrationDto.Name,
                Surname = registrationDto.Surname,
                Birthdate = registrationDto.Birthdate,
                Birthplace = registrationDto.Birthplace,
                Email = registrationDto.Email,
                Password = hashedPassword,

            };

            _context.Scouts.Add(scout);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("login")]
        public async Task<string> Login([FromBody] LoginDto loginDto)
        {
            var scout = await _context.Scouts.FirstOrDefaultAsync(s => s.Email == loginDto.Email);

            if (scout == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, scout.Password))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            return GenerateJwtToken(scout.Email);
        }

        private string GenerateJwtToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
