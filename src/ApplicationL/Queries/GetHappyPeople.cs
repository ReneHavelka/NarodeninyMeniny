using ApplicationL.Common.Interfaces;
using ApplicationL.Common.ModelsDto;
using Domain.Entities;
using System.Diagnostics;
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

        public IList<HappyPersonDto> HappyPeople()
        {
            //happyPeople - 7 dní odo dnešného dňa včítane
            IList<HappyPersonDto> happyPeople = new List<HappyPersonDto>();
            var getPeopleWithNameDay = new GetPeopleWithNameDay(_readDataFile, _readCalendar);
            var peopleWithNameDay = getPeopleWithNameDay.PeopleWithNameDay();
            DateTime today = DateTime.Now;

            for (int i = 0; i < 7; ++i)
            {
                var newDateTime = today.AddDays(i);
                var month = newDateTime.Month;
                var day = newDateTime.Day;
                var selectedPeople = peopleWithNameDay.Where(x => (x.DateOfBirth.Month == month && x.DateOfBirth.Day == day) || (x.NameDayMonth == month && x.NameDayDay == day));

                foreach (var selectedPerson in selectedPeople)
                {
                    HappyPersonDto happyPersonDto = new HappyPersonDto();
                    happyPersonDto.Name = selectedPerson.Name;
                    happyPersonDto.Surname = selectedPerson.Surname;
                    happyPersonDto.NickName = selectedPerson.NickName;
                    happyPersonDto.Suffix = selectedPerson.Suffix;

                    switch (true)
                    {
                        case true when (selectedPerson.DateOfBirth.Month == month && selectedPerson.DateOfBirth.Day == day) && !(selectedPerson.NameDayMonth == month && selectedPerson.NameDayDay == day):
                            happyPersonDto.HolidayType = "Narodeniny";
                            break;
                        case true when !(selectedPerson.DateOfBirth.Month == month && selectedPerson.DateOfBirth.Day == day) && (selectedPerson.NameDayMonth == month && selectedPerson.NameDayDay == day):
                            happyPersonDto.HolidayType = "Meniny";
                            break;
                        case true when (selectedPerson.DateOfBirth.Month == month && selectedPerson.DateOfBirth.Day == day) && (selectedPerson.NameDayMonth == month && selectedPerson.NameDayDay == day):
                            happyPersonDto.HolidayType = "Narodeniny a Meniny";
                            break;
                    }

                    switch (i)
                    {
                        case 0:
                            happyPersonDto.AdditionalNote = $"{day}.{month}. - dnes";
                            break;
                        case 1:
                            happyPersonDto.AdditionalNote = $"{day}.{month}. - zajtra";
                            break;
                        case 2:
                            happyPersonDto.AdditionalNote = $"{day}.{month}. - pozajtra";
                            break;
                        case 3:
                        case 4:
                            happyPersonDto.AdditionalNote = $"{day}.{month}. - o {i} dni";
                            break;
                        case > 4:
                            happyPersonDto.AdditionalNote = $"{day}.{month}. - o {i} dní";
                            break;
                    }
                    happyPeople.Add(happyPersonDto); 
                }
            }
            
            

            return happyPeople;
        }
    }
}
