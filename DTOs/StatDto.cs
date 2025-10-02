namespace PlayersScouting_backend.DTOs
{
    public class CreateStatDto
    {
        public string Season { get; set; }
        public string Club { get; set; }
        public int MatchesPlayed { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public string? FullName { get; set; }
    }

    public class UpdateStatDto
    {
        public int Id { get; set; }
        public string? Season { get; set; }
        public string? Club { get; set; }
        public int MatchesPlayed { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public string FullName { get; set; }
    }
}
