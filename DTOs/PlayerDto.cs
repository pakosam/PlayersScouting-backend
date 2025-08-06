namespace PlayersScouting_backend.DTOs
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }

    public class PlayerDescriptionDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateOnly Birthdate { get; set; }
        public string Birthplace { get; set; }
        public int Age { get; set; }
        public string Positions { get; set; }
        public string Club { get; set; }
    }

    public class PlayerFullReportDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateOnly Birthdate { get; set; }
        public string Birthplace { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public string Foot { get; set; }
        public int ShirtNumber { get; set; }
        public string Positions { get; set; }
        public string Club { get; set; }
    }

    public class CreatePlayerDto
    {
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

    public class UpdatePlayerDto
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
