using ApplicationL.Common.Interfaces;
using Domain.Entities;

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
			return _readDataFile.GetAllPeople();
		}
	}
}
