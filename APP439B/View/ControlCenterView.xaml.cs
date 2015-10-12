﻿using System;
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
            player = new SoundPlayer();
            player.SoundLocation = "../../Sounds/redalert.wav";
            playing = false;
        }

        private void HandShake_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
        private void HandShake_CanExecut(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void HandShake_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                App.MainBoard.HandShake();
            }
            catch
            {
 
            }

           
        }

        private void PlayBoardcast_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (playing)
                {
                    PlayBoardcast.Content = "播放警报";
                    player.Stop();
                    playing = false;
                }
                else
                {
                    PlayBoardcast.Content = "解除警报";
                    player.PlayLooping();
                    playing = true;
                }
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

        private void Start_Click(object sender, RoutedEventArgs e)
        {

            if (Start.Content.ToString() == "实验开始")
            {
                try
                {
                    App.MainBoard.TestStart();
                    Start.Content = "实验停止";
                }
                catch
                {

                }
                
            }
            else
            {
                try
                {
                    App.MainBoard.TestStop();
                    Start.Content = "实验开始";
                }
                catch
                {

                }
                
            }
        }
    }
}