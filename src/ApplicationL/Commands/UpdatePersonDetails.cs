using ApplicationL.Common.Interfaces;
using Domain.Entities;
using System.Text.Json;

namespace ApplicationL.Commands
{
    public class UpdatePersonDetails
    {
        IWriteDataFile _writeDataFile;
        int _id;

        public UpdatePersonDetails(IWriteDataFile writeDataFile, int id)
        {
            _writeDataFile = writeDataFile;
            _id = id;
        }

        public void WriteToFile(Person person, IEnumerable<Person> allPeople)
        {
            List<Person> allPeopleList = allPeople.ToList();
            int i = allPeopleList.FindIndex(x => x.Id == _id);

            allPeopleList[i] = person;

            string updateAllPeople = JsonSerializer.Serialize(allPeopleList);

            _writeDataFile.WriteData(updateAllPeople);
        }
    }
}
