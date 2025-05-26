namespace PlayersScouting_backend.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly Birthdate { get; set; }
        public string Birthplace { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public string Foot { get; set; }
        public int ShirtNumber { get; set; }
        public string Positions { get; set; }
        public string Club { get; set; }
    }
}
