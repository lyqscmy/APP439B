using APP439B.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace APP439B
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static MainBoard mainBoard = new MainBoard();
        public static MainBoard MainBoard { get { return mainBoard; } }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            # region Windows
            MainWindow mainWindow = new MainWindow();

            LoginWindow login = new LoginWindow();
            login.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            login.ShowDialog();

            mainWindow.Show();
            # endregion //Windows
        }
    }
}
