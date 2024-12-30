using ApplicationL.Common.Interfaces;
using Domain.Entities;
using System.Text.Json;

namespace ApplicationL.Commands
{
	public class RemovePersonRecord
	{
		IReadDataFile _readDataFile;
		IWriteDataFile _writeDataFile;
		int _id;

		public RemovePersonRecord(IReadDataFile readDataFile, IWriteDataFile writeDataFile, int id)
		{
			_readDataFile = readDataFile;
			_writeDataFile = writeDataFile;
			_id = id;
		}

		public void WriteToFile(Person person, IEnumerable<Person> allPeople)
		{
			List<Person> allPeopleList = allPeople.ToList();
			int i = allPeopleList.FindIndex(x => x.Id == _id);

			allPeopleList.RemoveAt(i);

			_writeDataFile.WriteData(allPeopleList);
		}
	}
}
