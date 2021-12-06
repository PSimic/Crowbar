using System.Windows;

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
