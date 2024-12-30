using ApplicationL.Common.Interfaces;
using Domain.Entities;
using System.Text;
using System.Text.Json;

namespace Infrastructure
{
	public class ReadDataFile : IReadDataFile
	{
		private string dataFileName;

		public ReadDataFile()
		{
			var accessDataFile = new AccessDataFile();
			dataFileName = accessDataFile.GetFileName();
		}

		public string ReadData()
		{
			string fileData = String.Empty;
			fileData = File.ReadAllText(dataFileName, Encoding.UTF8);

			return fileData;
		}

		public IEnumerable<Person> GetAllPeople()
		{
			var fileData = ReadData();
			IEnumerable<Person> allPeople = null;
			if (fileData != String.Empty) { allPeople = JsonSerializer.Deserialize<Person[]>(fileData); }
			return allPeople;
		}
	}
}
