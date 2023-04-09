using System.Diagnostics;

namespace Infrastructure
{
    internal class AccessDataFile
    {
        internal string GetFileName()
        {
            var dataFileName = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NarodeninyMeniny", "birthnamedays.json");
            var directory = Path.GetDirectoryName(dataFileName);

            Debug.WriteLine(dataFileName);

            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            if (!Path.Exists(dataFileName)) File.Create(dataFileName).Dispose();

            return dataFileName;
        }

        internal string GetCalendarFileName()
        {
            var calendarFileName = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NarodeninyMeniny", "Calendar.json");
            Debug.WriteLine(calendarFileName);


            if (!Path.Exists(calendarFileName)) return String.Empty;

            return calendarFileName;
        }
    }
}
