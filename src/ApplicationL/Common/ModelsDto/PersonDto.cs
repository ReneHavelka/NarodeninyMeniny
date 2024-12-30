using Domain.Entities;

namespace ApplicationL.Common.ModelsDto
{
	public record class PersonDto : Person
	{
		public int? NameDayMonth { get; set; }
		public int? NameDayDay { get; set; }
	}
}
