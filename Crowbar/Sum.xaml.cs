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

namespace Crowbar
{
    /// <summary>
    /// Interaction logic for Sum.xaml
    /// </summary>
    public partial class Sum : Window
    {
        List<string> listOfFilesPath = new List<string>();
        string sheetName = "Iskaz";

        public Sum()
        {
            InitializeComponent();
        }

        private void ImportExcel_Click(object sender, RoutedEventArgs e)
        {
            listOfFilesPath.Clear();
            listBox.Items.Clear();
            ImportExport.ImportFiles(out listOfFilesPath, "Excel files (*.xls; *.xlsx)|*.xls;*.xlsx");

            listBox.Items.Add("Broj ucitanih fajlova: " + listOfFilesPath.Count());
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            string response = string.Empty;

            try
            {
                listBox.Items.Add($"Suma svih vrednosti za nadelu iznosi: " + String.Format("{0:0.00}", Tools.SumValues(listOfFilesPath, sheetName, out response)));

                listBox.Items.Add(response);
            }
            catch (Exception ex)
            {
                listBox.Items.Add(ex.Message);
            }
        }
    }
}
