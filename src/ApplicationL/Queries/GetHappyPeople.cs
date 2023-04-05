using ApplicationL.Common.Interfaces;
using ApplicationL.Common.ModelsDto;
using Domain.Entities;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace ApplicationL.Queries
{
    public class GetHappyPeople
    {
        IReadDataFile _readDataFile;
        IReadCalendar _readCalendar;

        public GetHappyPeople(IReadDataFile readDataFile, IReadCalendar readCalendar)
        {
            _readDataFile = readDataFile;
            _readCalendar = readCalendar;
        }

        public IEnumerable<HappyPersonDto> HappyPeople()
        {
            //7 dates incl. today: month, day
            IList<(int, int)> sevenDates = new List<(int, int)>();

            for (int i = 0; i < 7; ++i)
            {
                var day = DateTime.Now.AddDays(i).Day;
                var month = DateTime.Now.AddDays(i).Month;
                sevenDates.Add((month, day));
            }

            //People with birthdays within 7 dates
            var getPeopleWithNameDay = new GetPeopleWithNameDay(_readDataFile, _readCalendar);
            var peopleWithNameDay = getPeopleWithNameDay.PeopleWithNameDay();

            var selectedPeople = peopleWithNameDay.Where(x => sevenDates.Contains((x.DateOfBirth.Month, x.DateOfBirth.Day)) || sevenDates.Contains((x.NameDayMonth ?? 0, x.NameDayDay ?? 0)));

            //Transform PersonDto to HappyPersonDto
            var happyPeople = from selectedPerson in selectedPeople
                              let birthday = sevenDates.Contains((selectedPerson.DateOfBirth.Month, selectedPerson.DateOfBirth.Day)) ? "Birthday" : string.Empty
                              let nameDay = sevenDates.Contains((selectedPerson.NameDayMonth ?? 0, selectedPerson.NameDayDay ?? 0)) ? "Name Day" : string.Empty
                              select new HappyPersonDto
                              {
                                  Name = selectedPerson.Name,
                                  Surname = selectedPerson.Surname,
                                  NickName = selectedPerson.NickName,
                                  Suffix = selectedPerson.Suffix,
                                  HolidayType = birthday.Length > 0 && nameDay.Length > 0 ? birthday + " and " + nameDay : birthday + nameDay,
                                  AdditionalNote = birthday.Length > 0 ? selectedPerson.DateOfBirth.Day + "." + selectedPerson.DateOfBirth.Month + "." :
                                                                         selectedPerson.NameDayMonth + "." + selectedPerson.NameDayDay + ".",
                              };

            return happyPeople;
        }
    }
}
