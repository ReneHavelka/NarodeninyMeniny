using ApplicationL.Common.Interfaces;
using ApplicationL.Queries;
using Domain.Entities;
using System.Text.Json;

namespace ApplicationL.Commands
{
    public class CreatePerson
    {
        IReadDataFile _readDataFile;
        IWriteDataFile _writeDataFile;

        public CreatePerson(IReadDataFile readDataFile, IWriteDataFile writeDataFile)
        {
            _readDataFile = readDataFile;
            _writeDataFile = writeDataFile;
        }

        public void WriteToFile(Person person)
        {
            var getPeople = new GetPeople(_readDataFile);
            var getAllPeople = getPeople.GetAllPeople();
            IList<Person> allPeopleList = new List<Person>();

            if (getAllPeople != null)
            {
                int id = 0;
                if (getAllPeople.Count() > 0)
                {
                    getAllPeople = getAllPeople.OrderBy(x => x.Id);
                    allPeopleList = getAllPeople.ToList();
                    id = allPeopleList.Last().Id + 1;
                }
                person.Id = id;
            }
            else
            {
                person.Id = 1;
            }

            allPeopleList.Add(person);

            string updateAllPeople = JsonSerializer.Serialize(allPeopleList);

            _writeDataFile.WriteData(updateAllPeople);
        }

    }
}