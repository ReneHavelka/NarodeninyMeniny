using Domain.Entities;

namespace ApplicationL.Common.Interfaces
{
	public interface IReadCalendar
	{
		public string ReadCaledarData();
		public IEnumerable<Calendar> CalendarDays();

	}
}
