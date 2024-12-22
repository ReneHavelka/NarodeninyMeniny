using ApplicationL.Common.Interfaces;
using Domain.Entities;
using System.Text.Json;

namespace ApplicationL.Queries
{
	public class GetPeople
	{
		private IReadDataFile _readDataFile;
		public GetPeople(IReadDataFile readDataFile)
		{
			_readDataFile = readDataFile;
		}

		public IEnumerable<Person> GetAllPeople()
		{
			var fileData = _readDataFile.ReadData();
			IEnumerable<Person> allPeople = null;
			if (fileData != String.Empty) { allPeople = JsonSerializer.Deserialize<Person[]>(fileData); }
			return allPeople;
		}
	}
}
