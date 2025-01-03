﻿namespace Infrastructure
{
	internal class AccessDataFile
	{
		internal string GetFileName()
		{
			var dataFileName = System.IO.Path.Combine(@"C:\Users\Public\NarodeninyMeniny", "birthnamedays.json");
			var directory = Path.GetDirectoryName(dataFileName);

			if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
			if (!Path.Exists(dataFileName)) File.Create(dataFileName).Dispose();

			return dataFileName;
		}

		internal string GetCalendarFileName()
		{
			var calendarFileName = System.IO.Path.Combine(@"C:\Users\Public\NarodeninyMeniny", "Calendar.json");

			if (!Path.Exists(calendarFileName)) return String.Empty;

			return calendarFileName;
		}
	}
}
