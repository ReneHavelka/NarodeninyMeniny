using Domain.Entities;

namespace ApplicationL.Common.Interfaces
{
	public interface IReadDataFile
	{
		public string ReadData();
		public IEnumerable<Person> GetAllPeople();
	}
}
