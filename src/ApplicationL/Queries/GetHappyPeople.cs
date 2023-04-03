using ApplicationL.Common.Interfaces;
using ApplicationL.Common.ModelsDto;
using Domain.Entities;
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
            var getPeopleWithNamesdays = new GetPeopleWithNamesdays(_readDataFile, _readCalendar);
            var peopleWithNamesdays = getPeopleWithNamesdays.PeopleWithNamesdays();

            var todayDay = DateOnly.FromDateTime(DateTime.Now);
            var todayDayMonth = todayDay.Day.ToString() + "." + todayDay.Month.ToString() + ".";

            //Compare: today with date of birth, namesday
            var peopleDto = peopleWithNamesdays.Where(x => x.DateOfBirth.Equals(todayDay) || (x.NamesdayDate == todayDayMonth));

            Queue<HappyPersonDto> happyPeople = new Queue<HappyPersonDto>();

            foreach (var person in peopleDto)
            {
                HappyPersonDto happyPersonDto = new();
                happyPersonDto.Name = person.Name;
                happyPersonDto.Surname = person.Surname;
                happyPersonDto.NickName = person.NickName;
                happyPersonDto.Suffix = person.Suffix;
   
                string holidayType = string.Empty;
                
                if (person.DateOfBirth.Day == todayDay.Day && person.DateOfBirth.Month == todayDay.Month) { holidayType = "Narodeniny"; }
                if (person.NamesdayDate == todayDayMonth) { holidayType += "Meniny"; }
                if (holidayType == "NarodeninyMeniny") { holidayType = "Narodeniny a Meniny"; }
                
                happyPersonDto.HolidayType = holidayType;
                happyPeople.Enqueue(happyPersonDto);
            }

            return happyPeople;
        }
    }
}
