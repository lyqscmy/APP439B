using APP439B.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using APP439B.Model;
using System.Windows.Input;

namespace APP439B
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        # region Properties
        private static MainBoard mainBoard = new MainBoard();
        public static MainBoard MainBoard { get { return mainBoard; } }

        private static SecondBoard secondBoard = new SecondBoard();
        public static SecondBoard SecondBoard { get { return secondBoard; } }

        //private static Database database = new Database();
        //public static Database Database { get { return database; } }

        private static bool[] steps = new bool[5];
        public static bool[] Steps 
        {
            get { return steps; }
            set { steps = value; }
        }

        # endregion //Properties

        //# region Commands

        //public void HandShake(object sender, ExecutedRoutedEventArgs e)
        //{
        //    try
        //    {
        //        App.MainBoard.HandShake();
        //        App.SecondBoard.HandShake();
        //    }
        //    catch
        //    { }
          
        //}

        //public void CanHandShake(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    e.CanExecute = Steps[0];
        //}

        //public  void PlayBoardcast(object sender, ExecutedRoutedEventArgs e)
        //{
            
        //}

        //public void CanPlayBoardcast(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    e.CanExecute = Steps[1];
        //}

        //public void Start(object sender, ExecutedRoutedEventArgs e)
        //{

        //}

        //public void CanStart(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    e.CanExecute = Steps[1];
        //}

        //public void Stop(object sender, ExecutedRoutedEventArgs e)
        //{

        //}

        //public void CanStop(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    e.CanExecute = Steps[1];
        //}

        //# endregion //Commands

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            # region Windows
            MainWindow mainWindow = new MainWindow();

            LoginWindow login = new LoginWindow();
            login.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Nullable<bool> dialogResult = login.ShowDialog();
            if ((bool)dialogResult)
            {
                mainWindow.Show();
            }
            else {
                App.Current.Shutdown();
            }

            
            

           
            # endregion //Windows
        }
    }
}
