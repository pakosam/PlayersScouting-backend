namespace PlayersScouting_backend.Entities
{
    public class Ratings
    {
        public int Id { get; set; }
        public double Attack { get; set; }
        public double Defense { get; set; }
        public double Tactics { get; set; }
        public double Technique { get; set; }
        public double PhysicalStrength { get; set; }
        public double MentalStrength { get; set; }
        public int PlayerId { get; set; }
    }
}
