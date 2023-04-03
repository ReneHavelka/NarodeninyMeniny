using ApplicationL.Common.Interfaces;
using ApplicationL.Queries;
using Infrastructure;
using System.Windows;

namespace WinUI.Views
{
    /// <summary>
    /// Interaction logic for BrithdayNamesdayPerson.xaml
    /// </summary>
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
