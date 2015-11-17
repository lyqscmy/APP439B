using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
using APP439B.ViewModel;

namespace APP439B.View
{
    /// <summary>
    /// Interaction logic for ControlCenterView.xaml
    /// </summary>
    public partial class ControlCenterView : UserControl
    {
        private SoundPlayer player;
        private bool playing;

        public ControlCenterView()
        {
            InitializeComponent();
            //this.DataContext = new ControlCenterViewModel();
            player = new SoundPlayer();
            player.SoundLocation = "../../Sounds/yuxianjb.wav";
            playing = false;
        }

        private void HandShake_Click(object sender, RoutedEventArgs e)
        {
            HandshakeWindow handShakeWindow = new HandshakeWindow();
            Nullable<bool> dialogResult = handShakeWindow.ShowDialog();
            if((bool)dialogResult)
            {
                HandShake.IsEnabled = false;
                PlayBoardcast.IsEnabled = true;
            }
            
        }

        private void yuxianjb_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                player.Play();
                PlayBoardcast.IsEnabled = false;
                Start.IsEnabled = true;
            }
            catch (System.IO.FileNotFoundException err)
            {
                // An error will occur here if the file can't be found.
            }
            catch (FormatException err)
            {
                // A FormatException will occur here if the file doesn't
                // contain valid WAV audio.
            }
        }

        private void jiechujb_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                player.Stop();
                player.SoundLocation = "../../Sounds/jiechujb.wav";
                player.Play();
                HandShake.IsEnabled = true;
                Stop.IsEnabled = false;
                PlaySafeBoardcast.IsEnabled = false;
            }
            catch (System.IO.FileNotFoundException err)
            {
                // An error will occur here if the file can't be found.
            }
            catch (FormatException err)
            {
                // A FormatException will occur here if the file doesn't
                // contain valid WAV audio.
            }
        }

        private void fire(object sender, RoutedEventArgs e)
        {
            Start.IsEnabled = false;
            Stop.IsEnabled = true;
            PlaySafeBoardcast.IsEnabled = true;
        }


        private void stop(object sender, RoutedEventArgs e)
        {
            Start.IsEnabled = true;
            Stop.IsEnabled = false;
            PlaySafeBoardcast.IsEnabled = false;

        }

        //private void Start_Click(object sender, RoutedEventArgs e)
        //{

        //    if (Start.Content.ToString() == "实验开始")
        //    {
        //        try
        //        {
        //            App.MainBoard.TestStart();
        //            App.SecondBoard.TestStart();
        //            Start.Content = "实验停止";
        //        }
        //        catch(Exception)
        //        {
                    
        //        }
                
        //    }
        //    else
        //    {
        //        try
        //        {
        //            App.MainBoard.TestStop();
        //            App.SecondBoard.TestStop();
        //            Start.Content = "实验开始";
        //        }
        //        catch(Exception)
        //        {

        //        }
                
        //    }
        //}

    }
}
