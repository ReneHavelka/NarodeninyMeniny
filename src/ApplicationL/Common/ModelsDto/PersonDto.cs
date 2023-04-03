using Domain.Entities;

namespace ApplicationL.Common.ModelsDto
{
    public record class PersonDto : Person
    {
        public string? NamesdayDate { get; set; }
    }
}
