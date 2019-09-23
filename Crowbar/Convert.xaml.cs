using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Crowbar
{
    /// <summary>
    /// Interaction logic for Convert.xaml
    /// </summary>
    public partial class Convert : Window
    {
        List<string> listOfFilesPath = new List<string>();
        string sheetName = string.Empty;
        string folderLocation = string.Empty;

        public Convert()
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

        private void Folder_Click(object sender, RoutedEventArgs e)
        {
            ImportExport.ExportFolder(out folderLocation);

            listBox.Items.Add("Putanja foldera: " + folderLocation);
        }

        private void SheetName_Click(object sender, RoutedEventArgs e)
        {
            sheetName = textBox.Text;

            listBox.Items.Add("Naziv sheet-a: " + sheetName);
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string errors = string.Empty;
                Tools.ConvertFromExcelToPDF(listOfFilesPath, folderLocation, sheetName, out errors);

                if (!errors.Equals(string.Empty))
                    listBox.Items.Add(errors);

                listBox.Items.Add("--- Konverzija uspesno zavrsena. ---");
            }
            catch (Exception ex)
            {
                listBox.Items.Clear();
                listBox.Items.Add(ex.Message);
            }
        }
    }
}
