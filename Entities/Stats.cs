namespace PlayersScouting_backend.Entities
{
    public class Stats
    {
        public int Id { get; set; }
        public string Season { get; set; }
        public string Club { get; set; }
        public int MatchesPlayed { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int PlayerId { get; set; }
    }
}
