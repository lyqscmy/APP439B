using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace APP439B.View
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class EnvirnomentView : UserControl
    {
        DispatcherTimer dispatcherTimer;
        
        public EnvirnomentView()
        {
            InitializeComponent();

            Status.Content = "中心控制器未连接";
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            T.SelectedItem = T.Items[0];
            dispatcherTimer.Interval = new TimeSpan(0, 0, int.Parse(T.Text));
            dispatcherTimer.Start();
        }

        public static RoutedCommand QueryCommand = new RoutedCommand();

        //public int T 
        //{
        //    get
        //    {
        //        return t;
        //    }
        //    set
        //    {
        //        if (value == t)
        //            return;

        //        t = value;
        //        dispatcherTimer.Stop();
        //        dispatcherTimer.Interval = new TimeSpan(0, 0, t);
        //        dispatcherTimer.Start();
        //    }
        //}

        private void Refresh_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if(App.MainBoard.IsConnect)
            {
                Status.Content = "串口连接成功";
                e.CanExecute = true;
            }
            else
            {
                Status.Content = "中心控制器未连接";
                e.CanExecute = false;
            }
        }

        private void Refresh_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            try
            {
                Status.Content = "立即刷新";
                gridEnvirnomentData.DataContext = App.MainBoard.Query("Envirnoment");
               
            }
            catch
            {
                Status.Content = "中心控制器通信异常";
                //MessageBox.Show();
            }
           
        }

        #region Helper
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (App.MainBoard.IsConnect)
            {
                Refresh_Executed(this,null);
            }
        }
        #endregion //Helper

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int value;
            if (!Int32.TryParse(T.Text, out value))
            {
                dispatcherTimer.Stop();
                dispatcherTimer.Interval = new TimeSpan(0, 0, value);
                dispatcherTimer.Start();
            }
        }

    }
}
