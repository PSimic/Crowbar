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

namespace Crowbar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConvertPDF_Click(object sender, RoutedEventArgs e)
        {
            Convert convert = new Convert();
            convert.ShowDialog();
        }

        private void CalculateSum_Click(object sender, RoutedEventArgs e)
        {
            Sum sum = new Sum();
            sum.ShowDialog();
        }

        private void Sign_Click(object sender, RoutedEventArgs e)
        {
            Sign sign = new Sign();
            sign.ShowDialog();
        }
    }
}
