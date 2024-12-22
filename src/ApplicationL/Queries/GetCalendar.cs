using ApplicationL.Common.Interfaces;
using Domain.Entities;
using System.Text.Json;

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
			IEnumerable<Calendar> calendarDays = null;

			var calendarData = _readCalendar.ReadCaledarData();
			if (calendarData != null) calendarDays = JsonSerializer.Deserialize<IEnumerable<Calendar>>(calendarData);

			return calendarDays;
		}
	}
}
