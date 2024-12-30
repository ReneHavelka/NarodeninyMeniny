using ApplicationL.Common.Interfaces;
using Domain.Entities;

namespace ApplicationL.Queries
{
	public class GetPerson
	{
		private IReadDataFile _readDataFile;
		public GetPerson(IReadDataFile readDataFile)
		{
			_readDataFile = readDataFile;
		}
		public (Person, IEnumerable<Person>) GetSpecificPerson(int id)
		{
			var getPeople = new GetPeople(_readDataFile);
			var allPeople = getPeople.GetAllPeople();
			Person specificPerson = allPeople.FirstOrDefault(x => x.Id == id);
			return (specificPerson, allPeople);
		}
	}
}
