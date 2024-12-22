using ApplicationL.Common.Interfaces;
using ApplicationL.Common.ModelsDto;
using ApplicationL.Queries;
using Domain.Entities;
using Infrastructure;
using System.Windows;
using WinUI.Views;

namespace WinUI
{
    public partial class MainWindow : Window
    {
        private IReadDataFile readDataFile;
        private IReadCalendar readCalendar;
        private IWriteDataFile writeDataFile;

        public MainWindow()
        {
            readDataFile = new ReadDataFile();
            writeDataFile = new WriteDataFile();
            readCalendar = new ReadCalendar();

            SetDataContext();
            InitializeComponent();
        }

        private void SetDataContext()
        {
            var getAllPeople = new GetPeopleWithNameDay(readDataFile, readCalendar).PeopleWithNameDay();

            DataContext = getAllPeople;
        }

        internal void Button_Click_HappyPerson(object sender, RoutedEventArgs e)
        {
            var happyPerson = new ViewHappyPeople(readDataFile, readCalendar);
            happyPerson.Show();
            this.Close();
        }

        private void Button_Click_NewPerson(object sender, RoutedEventArgs e)
        {
            var newPerson = new NewPerson(readDataFile, writeDataFile);
            newPerson.Show();
            this.Close();
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            var frameworkElement = (FrameworkElement)e.OriginalSource;
            var person = (PersonDto)frameworkElement.DataContext;
            var id = person.Id;

            var editPersonDetails = new EditPersonDetails(readDataFile, writeDataFile, id);
            editPersonDetails.Show();
            this.Close();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            var frameworkElement = (FrameworkElement)e.OriginalSource;
            var person = (Person)frameworkElement.DataContext;
            var id = person.Id;

            var deletePersonRecord = new DeletePersonRecord(readDataFile, writeDataFile, id);
            deletePersonRecord.Show();
            this.Close();
        }
    }
}
