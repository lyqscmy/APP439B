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

            
            switch (button)
            {
                case "Envirnoment":
                    Console.WriteLine(button);
                    break;

                case "Motor":
                    Console.WriteLine(button);
                    break;
                case "Video":
                    Console.WriteLine(button);
                    break;
                case "Cesu":
                    Console.WriteLine(button);
                    break;
                case "Jilu":
                    Console.WriteLine(button);
                    break;
                case "All":
                    Console.WriteLine(button);
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
