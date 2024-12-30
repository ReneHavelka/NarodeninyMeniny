using ApplicationL.Common.Interfaces;
using ApplicationL.Queries;
using Domain.Entities;
using Moq;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace NarodeninyMeninyTesty.UnitTests.ApplicatonL.Queries
{
	[TestClass]
	public class GetHappyPeopleTests
	{
		Mock<IReadCalendar> readCalendar;
		Mock<IReadDataFile> readDataFile;

		public GetHappyPeopleTests()
		{
			readCalendar = new Mock<IReadCalendar>();
			readDataFile = new Mock<IReadDataFile>();
		}

		[TestMethod]
		public void GetHappyPeople()
		{
			var calendarFileName = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NarodeninyMeniny", "Calendar.json");

			var calendarData = File.ReadAllText(calendarFileName, Encoding.UTF8);

			IEnumerable<Calendar> calendarDays = JsonSerializer.Deserialize<Calendar[]>(calendarData);
			var today = DateTime.Today;

			IList<Person> personList = new List<Person>();
			for (int i = 0; i < 4; ++i)
			{
				var day = today.AddDays(i).Day;
				var month = today.AddDays(i).Month;
				var calendarDay = calendarDays.FirstOrDefault(x => x.Day == day && x.Month == month);

				Person person = new Person();
				person.Name = "Nm";
				person.Surname = "Sn";
				person.DateOfBirth = new DateOnly(2001, 1, 1);

				if (i == 1 || i == 2) person.Name = calendarDay.Name;
				if (i == 0 || i == 2) person.DateOfBirth = new DateOnly(2001, calendarDay.Month, calendarDay.Day);
				personList.Add(person);
			}

			var fileData = JsonSerializer.Serialize(personList);

			//Setting up mocks
			readDataFile.Setup(x => x.ReadData()).Returns(fileData);
			readCalendar.Setup(x => x.ReadCaledarData()).Returns(calendarData);

			var getHappyPeople = new GetHappyPeople(readDataFile.Object, readCalendar.Object);
			var happyPeople = getHappyPeople.HappyPeople();

			foreach (var happyPerson in happyPeople)
			{
				Debug.WriteLine($"{happyPerson.Name}   {happyPerson.Surname}   {happyPerson.HolidayType}");
			}

		}
	}
}
