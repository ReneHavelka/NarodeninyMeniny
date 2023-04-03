using ApplicationL.Commands;
using ApplicationL.Common.Interfaces;
using Domain.Entities;
using Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

            [TestMethod]
        public void PersonNameShouldNotBeNull()
        {

            //Arrange
            Person person = new Person() { Name = "nm", Surname = "srn" };

            mock.Setup(x => x.WriteData(It.IsAny<string>())).Callback(() => Debug.WriteLine("CreateTest"));
            var createPersonTest = new CreatePerson(readDataFile, mock.Object);

            //act
            createPersonTest.WriteToFile(person);

            //Asert
            string str = "[{\"Id\":1,\"Name\":\"nm\",\"Surname\":\"srn\",\"NickName\":null,\"Suffix\":null,\"DateOfBirth\":\"0001-01-01\"}]";
            mock.Verify(x => x.WriteData(str), Times.Once());
        }
    }
}
