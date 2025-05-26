namespace PlayersScouting_backend.Entities
{
    public class Scout
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly Birthdate { get; set; }
        public string Birthplace { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PlayerId { get; set; }
    }
}
