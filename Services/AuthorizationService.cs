using Microsoft.IdentityModel.Tokens;
using PlayersScouting_backend.DTOs;
using PlayersScouting_backend.Entities;
using PlayersScouting_backend.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PlayersScouting_backend.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IScoutRepository _scoutRepository;
        private readonly IConfiguration _configuration;
        public AuthorizationService(IScoutRepository scoutRepository, IConfiguration configuration)
        {
            _scoutRepository = scoutRepository;
            _configuration = configuration;
        }
        public async Task RegisterAsync(RegistrationDto registrationDto)
        {
            var existingUser = await _scoutRepository.UserExistsAsync(registrationDto.Email);
            if (existingUser)
            {
                throw new ArgumentException("User already exists");
            }


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
                throw new NotImplementedException("User cannot be older than 70 years.");
            }

            if (age < 18)
            {
                throw new NotImplementedException("User cannot be younger than 18 years.");
            }

            if (registrationDto.Name.Length < 3 ||
                registrationDto.Surname.Length < 3 ||
                registrationDto.Birthplace.Length < 3 ||
                registrationDto.Email.Length < 4 ||
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
                Age = age,
                Email = registrationDto.Email,
                Password = hashedPassword,
                
            };

            await _scoutRepository.CreateScout(scout);
        }
        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            var scout = await _scoutRepository.GetUserByEmailAsync(loginDto.Email);
            if (scout == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, scout.Password))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            return GenerateJwtToken(scout.Email);
        }

        private string GenerateJwtToken(string email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, email),
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
