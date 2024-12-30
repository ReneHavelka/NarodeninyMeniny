using ApplicationL.Common.Interfaces;
using Domain.Entities;
using System.Text;
using System.Text.Json;

namespace Infrastructure
{
	public class WriteDataFile : IWriteDataFile
	{
		private string dataFileName;

		public WriteDataFile()
		{
			var accessDataFile = new AccessDataFile();
			dataFileName = accessDataFile.GetFileName();
		}

		public void WriteData(IList<Person> peopleList)
		{
			string updateAllPeople = JsonSerializer.Serialize(peopleList);
			File.WriteAllText(dataFileName, updateAllPeople, Encoding.UTF8);
		}
	}
}
