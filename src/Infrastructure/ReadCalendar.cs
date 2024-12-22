using ApplicationL.Common.Interfaces;
using System.Text;

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
	}
}
