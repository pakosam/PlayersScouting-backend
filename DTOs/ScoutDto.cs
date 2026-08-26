namespace PlayersScouting_backend.DTOs
{
    public class RegistrationDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly Birthdate { get; set; }
        public string Birthplace { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class CreateScoutDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly Birthdate { get; set; }
        public string Birthplace { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<string> PlayerFullNames { get; set; } = new();
    }

    public class UpdateScoutDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly Birthdate { get; set; }
        public string Birthplace { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<string> PlayerFullNames { get; set; } = new();
    }

    public class ScoutDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly Birthdate { get; set; }
        public string Birthplace { get; set; }
        public string Email { get; set; }
        public List<int> PlayersId { get; set; } = new();
    }
}
