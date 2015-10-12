using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using APP439B.View;
using APP439B.ViewModel;

namespace APP439B
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static SettingViewModel settings = new SettingViewModel();

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                Connect_Executed(this, null);
            }
            catch 
            {
                Status.Content = "中心控制器未连接";
            }
        }

        # region Commands
        private void Connect_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !App.MainBoard.IsConnect;
            
        }

        private void Connect_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            App.MainBoard.Set(settings);
            App.MainBoard.Connect();
            Status.Content = "串口号" + settings.PortName + "波特率" + settings.BaudRate;
        }


        private void Disconnect_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = App.MainBoard.IsConnect;
        }

        private void Disconnect_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Status.Content = "断开连接主板";
            App.MainBoard.Disconnect();
        }

        private void Settings_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SettingsView settingsView = new SettingsView();
            settingsView.DataContext = settings;
            Window settingsWindow = new Window
            {
                Title = "主板串口参数设置",
                Width = 300,
                Height = 300,
                Content = settingsView
            };

            settingsWindow.ShowDialog();
        }

        private void Settings_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        #endregion //Commands
    }
}
