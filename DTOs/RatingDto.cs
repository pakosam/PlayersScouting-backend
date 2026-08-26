namespace PlayersScouting_backend.DTOs
{
    public class RatingDto
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
    public class CreateRatingDto
    {
        public double Attack { get; set; }
        public double Defense { get; set; }
        public double Tactics { get; set; }
        public double Technique { get; set; }
        public double PhysicalStrength { get; set; }
        public double MentalStrength { get; set; }
        public string FullName { get; set; }
    }

    public class UpdateRatingDto
    {
        public int Id { get; set; }
        public double Attack { get; set; }
        public double Defense { get; set; }
        public double Tactics { get; set; }
        public double Technique { get; set; }
        public double PhysicalStrength { get; set; }
        public double MentalStrength { get; set; }
        public string FullName { get; set; }
    }
}
