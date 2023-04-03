using ApplicationL.Commands;
using ApplicationL.Common.Interfaces;
using ApplicationL.Queries;
using Domain.Entities;
using System.Collections.Generic;
using System.Windows;

namespace WinUI.Views
{
    /// <summary>
    /// Interaction logic for DeletePersonRecord.xaml
    /// </summary>
    public partial class DeletePersonRecord : Window
    {
        IReadDataFile _readDataFile;
        IWriteDataFile _writeDataFile;
        int _id;
        IEnumerable<Person> _allPeople;
        Person Person { get; set; }
        public DeletePersonRecord(IReadDataFile readDataFile, IWriteDataFile writeDataFile, int id)
        {
            _readDataFile = readDataFile;
            _writeDataFile = writeDataFile;
            _id = id;

            (Person person, IEnumerable<Person> allPeople) = GetPeople();
            _allPeople = allPeople;
            Person = person;

            DataContext = Person;
            InitializeComponent();
        }

        private (Person person, IEnumerable<Person> allPeople) GetPeople()
        {
            var getPerson = new GetPerson(_readDataFile);
            (Person person, IEnumerable<Person> allPeople) = getPerson.GetSpecificPerson(_id);
            return (person, allPeople);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var removePersonrecord = new RemovePersonRecord(_readDataFile, _writeDataFile, _id);
            removePersonrecord.WriteToFile(Person, _allPeople);

            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click_Zoznam(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
