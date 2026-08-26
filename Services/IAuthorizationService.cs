using PlayersScouting_backend.DTOs;

namespace PlayersScouting_backend.Services
{
    public interface IAuthorizationService
    {
        Task RegisterAsync(RegistrationDto registrationDto);
        Task<string> LoginAsync(LoginDto loginDto);
    }
}
