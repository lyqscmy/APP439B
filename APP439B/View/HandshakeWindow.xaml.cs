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
using System.Windows.Shapes;

namespace APP439B.View
{
    /// <summary>
    /// Interaction logic for HandshakeWindow.xaml
    /// </summary>
    public partial class HandshakeWindow : Window
    {
        public HandshakeWindow()
        {
            InitializeComponent();
        }

        private void HandShake(object sender, RoutedEventArgs e)
        {
            string button = (sender as Button).Name.ToString();

            bool result = false;
            switch (button)
            {
                case "Envirnoment":
                    Console.WriteLine(button);
                    result = App.MainBoard.HandShake("Envirnoment"); 
                    if (result)
                        EnvirIndicator.StateIndex = 1;
                    
                    break;

                case "Motor":
                    Console.WriteLine(button);
                    result = App.SecondBoard.HandShake("Motor"); 
                     if (result)
                         MotorIndicator.StateIndex = 1;
                    break;

                case "Video":
                    Console.WriteLine(button);
                    result = App.SecondBoard.HandShake("Video");
                    if (result)
                       VideoIndicator.StateIndex = 1;
                    break;

                case "Cesu":
                    Console.WriteLine(button);
                    result = App.MainBoard.HandShake("Cesu");
                    if (result)
                        CesuIndicator.StateIndex = 1;
                    break;

                case "Jilu":
                    Console.WriteLine(button);
                    result = App.SecondBoard.HandShake("Jilu");
                    if (result)
                        JiluIndicator.StateIndex = 1;
                    break;

                case "All":
                    Console.WriteLine(button);
                    result = App.MainBoard.HandShake("All") && App.SecondBoard.HandShake("All");
                    if (result)
                    {
                        EnvirIndicator.StateIndex = 1;
                        MotorIndicator.StateIndex = 1;
                        VideoIndicator.StateIndex = 1;
                        CesuIndicator.StateIndex = 1;
                        JiluIndicator.StateIndex = 1;
                    }
                       
                    break;
                default:
                    break;
            }
        }

        private void ok(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void cancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
