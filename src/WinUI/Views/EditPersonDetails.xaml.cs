using ApplicationL.Commands;
using ApplicationL.Common.Interfaces;
using ApplicationL.Common.Validators;
using ApplicationL.Queries;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace WinUI.Views
{
    public partial class EditPersonDetails : Window
    {
        IReadDataFile _readDataFile;
        IWriteDataFile _writeDataFile;
        int _id;
        IEnumerable<Person> _allPeople;
        Person Person { get; set; }


        public EditPersonDetails(IReadDataFile readDataFile, IWriteDataFile writeDataFile, int id)
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
            var personValidator = new PersonValidator(Person);
            string validationResult = personValidator.PersonValidate(Person);

            var dateOfBirthValidator = new DateOfBirthValidator(dateOfBirth.Text);
            validationResult += dateOfBirthValidator.DateOfBirthValidate(dateOfBirth.Text);

            if (validationResult == String.Empty)
            {
                var updatePerson = new UpdatePersonDetails(_readDataFile, _writeDataFile, _id);
                updatePerson.WriteToFile(Person, _allPeople);

                var mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                nameWarning.Text = String.Empty;
                if (validationResult.Contains("No name")) nameWarning.Text = "Meno je povinné.";
                surnameWarning.Text = String.Empty;
                if (validationResult.Contains("No surname")) surnameWarning.Text = "Priezvisko je povinné.";
                dateOfBirthWarning.Text = String.Empty;
                if (validationResult.Contains("Date too early")) dateOfBirthWarning.Text = "Dátum je nesprávny - príliš skorý.";
                if (validationResult.Contains("No date")) dateOfBirthWarning.Text = "Dátum je povinný.";
                if (validationResult.Contains("Date too late")) dateOfBirthWarning.Text = "Dátum je nesprávny - príliš neskorý.";
                if (validationResult.Contains("Incorrect date format")) dateOfBirthWarning.Text = "Dátum je v nesprávnom formáte.";
            }
        }

        private void GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string textBoxName = textBox.Name;

            switch (textBoxName)
            {
                case "name":
                    nameWarning.Text = String.Empty;
                    break;
                case "surname":
                    surnameWarning.Text = String.Empty;
                    break;
                case "dateOfBirth":
                    dateOfBirthWarning.Text = null;
                    break;
            }
        }

        private void Button_Click_Zoznam(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
