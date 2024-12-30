using ApplicationL.Common.Interfaces;
using Domain.Entities;
using System.Text;
using System.Text.Json;

namespace Infrastructure
{
	public class ReadCalendar : IReadCalendar
	{
		private string calendarFileName;

		public ReadCalendar()
		{
			var accessDataFile = new AccessDataFile();
			calendarFileName = accessDataFile.GetCalendarFileName();
		}

		public string ReadCaledarData()
		{
			string fileData = String.Empty;
			fileData = File.ReadAllText(calendarFileName, Encoding.UTF8);

			return fileData;
		}

		public IEnumerable<Calendar> CalendarDays()
		{
			IEnumerable<Calendar> calendarDays = null;

			var calendarData = ReadCaledarData();
			if (calendarData != null) calendarDays = JsonSerializer.Deserialize<IEnumerable<Calendar>>(calendarData);

			return calendarDays;
		}
	}
}
