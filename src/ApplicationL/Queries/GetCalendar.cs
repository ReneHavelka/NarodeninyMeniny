using ApplicationL.Common.Interfaces;
using Domain.Entities;

namespace ApplicationL.Queries
{
	internal class GetCalendar
	{
		IReadCalendar _readCalendar;

		internal GetCalendar(IReadCalendar readCalendar)
		{
			_readCalendar = readCalendar;
		}

		internal IEnumerable<Calendar> CalendarDays()
		{
			return _readCalendar.CalendarDays();
		}
	}
}
