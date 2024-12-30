using ApplicationL.Commands;
using ApplicationL.Common.Interfaces;
using Domain.Entities;
using Infrastructure;
using Moq;
using System.Diagnostics;

namespace NarodeninyMeninyTesty.UnitTests.Pages
{
	[TestClass]
	public class NewPersonTests
	{
		IReadDataFile readDataFile;
		Mock<IWriteDataFile> mock;

		public NewPersonTests()
		{
			readDataFile = new ReadDataFile();
			mock = new Mock<IWriteDataFile>();
		}

		[TestMethod]
		//Prepracovať!!!
		public void PersonNameShouldNotBeNull()
		{
			//Arrange
			Person person = new Person() { Name = "nm", Surname = "srn" };

			mock.Setup(x => x.WriteData(It.IsAny<IList<Person>>())).Callback(() => Debug.WriteLine("CreateTest"));
			var testCreatePerson = new CreatePerson(readDataFile, mock.Object);
			IList<Person> testPeopleList = new List<Person>() { person };

			//act
			testCreatePerson.WriteToFile(person);

			//Asert
			mock.Verify(x => x.WriteData(testPeopleList), Times.Once());
		}
	}
}
