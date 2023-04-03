namespace Domain.Entities
{
    public record class Calendar
    {
        public int Month { get; set; }
        public int Day { get; set; }
        public string Name { get; set; }
    }
}
