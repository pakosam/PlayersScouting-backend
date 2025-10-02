namespace PlayersScouting_backend.DTOs
{
    public class CreateScoutDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly Birthdate { get; set; }
        public string Birthplace { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PlayerFullName { get; set; }
    }

    public class UpdateScoutDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly Birthdate { get; set; }
        public string Birthplace { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PlayerFullName { get; set; }
    }
}
