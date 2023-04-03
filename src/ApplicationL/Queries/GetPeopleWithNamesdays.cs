using ApplicationL.Common.Interfaces;
using ApplicationL.Common.ModelsDto;
using Domain.Entities;
using System.Diagnostics;
using System.Text.Json;

namespace ApplicationL.Queries
{
    public class GetPeopleWithNamesdays
    {
        private IReadDataFile _readDataFile;
        private IReadCalendar _readCalendar;
        public GetPeopleWithNamesdays(IReadDataFile readDataFile, IReadCalendar readCalendar)
        {
            _readDataFile = readDataFile;
            _readCalendar = readCalendar;
        }

        public IEnumerable<PersonDto> PeopleWithNamesdays()
        {
            var fileData = _readDataFile.ReadData();
            IEnumerable<Person> allPeople = null;
            if (fileData != String.Empty) { allPeople = JsonSerializer.Deserialize<Person[]>(fileData); }

            var calendarData = _readCalendar.ReadCaledarData();
            IEnumerable<Calendar> calendarDays = null;
            if (calendarData != String.Empty) { calendarDays = JsonSerializer.Deserialize<Calendar[]>(calendarData); }

            Queue<Calendar> calendarQueue = new Queue<Calendar>();

            foreach (var namesDay in calendarDays)
            {
                if (namesDay.Name.Contains(","))
                {
                    string[] separatedNames = namesDay.Name.Split(',');
                    foreach (var singleName in separatedNames)
                    {
                        Calendar calendarDay = new Calendar();
                        calendarDay.Name = singleName.Trim();
                        calendarDay.Month = namesDay.Month;
                        calendarDay.Day = namesDay.Day;
                        calendarQueue.Enqueue(calendarDay);
                    }
                }
                else
                {
                    calendarQueue.Enqueue(namesDay);
                }
            }

            calendarDays = calendarQueue;

            var peopleWithNamesday = from person in allPeople
                                     join day in calendarDays on person.Name equals day.Name into namesDays
                                     from namesDay in namesDays.DefaultIfEmpty()
                                     select new PersonDto
                                     {
                                         Id = person.Id,
                                         Name = person.Name,
                                         Surname = person.Surname,
                                         NickName = person.NickName,
                                         Suffix = person.Suffix,
                                         DateOfBirth = person.DateOfBirth,
                                         NamesdayDate = namesDay == null ? string.Empty : $"{namesDay.Day}.{namesDay.Month}.",
                                     };

            return peopleWithNamesday;
        }
    }
}
