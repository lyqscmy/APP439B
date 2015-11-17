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
using System.Windows.Threading;

namespace APP439B.View
{
    /// <summary>
    /// Interaction logic for PullParameterView.xaml
    /// </summary>
    public partial class PullParameterView : UserControl
    {
        private DispatcherTimer dispatcherTimer;
        public PullParameterView()
        {
            InitializeComponent();

            Status.Content = "次工控机未连接";
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            T.SelectedItem = T.Items[0];
            dispatcherTimer.Interval = new TimeSpan(0, 0, int.Parse(T.Text));
            dispatcherTimer.Start();
        }

        public static RoutedCommand QueryCommand = new RoutedCommand();

        private void Refresh_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            
        }

        private void Refresh_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            try
            {
                Status.Content = "立即刷新";
                gridPullData.DataContext = App.SecondBoard.HandShake();

            }
            catch(Exception)
            {
                Status.Content = "中心控制器通信异常";
                //MessageBox.Show();
            }

        }

        #region Helper
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
           
        }
        #endregion //Helper

        private void T_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
