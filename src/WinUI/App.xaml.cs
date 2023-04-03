using System;
using System.Windows;

namespace WinUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var mainWindow = new MainWindow();
            if (e.Args.Length > 0)
            {
                mainWindow.Show();
            }
            else
            {
                var obj = new Object();
                var arg = new RoutedEventArgs();
                mainWindow.Button_Click_HappyPerson(obj, arg);
            }
        }
    }
}
