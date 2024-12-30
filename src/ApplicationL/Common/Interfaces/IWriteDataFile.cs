using Domain.Entities;

namespace ApplicationL.Common.Interfaces
{
	public interface IWriteDataFile
	{
		public void WriteData(IList<Person> peopleList);
	}
}
