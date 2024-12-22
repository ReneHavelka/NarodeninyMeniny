namespace Domain.Entities
{
    public record class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NickName { get; set; }
        public string Suffix { get; set; }
        public DateOnly DateOfBirth { get; set; }
    }
}
