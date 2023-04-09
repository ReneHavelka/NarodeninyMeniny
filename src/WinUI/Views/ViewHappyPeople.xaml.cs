using ApplicationL.Common.Interfaces;
using ApplicationL.Queries;
using Infrastructure;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace WinUI.Views
{
    public partial class ViewHappyPeople : Window
    {
        IReadDataFile _readDataFile;
        IReadCalendar _readCalendar;

        public ViewHappyPeople(IReadDataFile readDataFile, IReadCalendar readCalendar)
        {
            _readDataFile = readDataFile;
            _readCalendar = readCalendar;
            SetDataContext();
            InitializeComponent();
        }

        private void SetDataContext()
        {
            var getHappyPeople = new GetHappyPeople(_readDataFile, _readCalendar);
            var happyPeople = getHappyPeople.HappyPeople();

            if (happyPeople.Count() == 0) { Application.Current.Shutdown(); };

            DataContext = happyPeople;
        }

        private void Button_Click_Zoznam(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

    }
}
