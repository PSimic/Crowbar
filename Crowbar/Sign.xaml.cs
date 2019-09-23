using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Crowbar
{
    /// <summary>
    /// Interaction logic for Sign.xaml
    /// </summary>
    public partial class Sign : Window
    {
        List<string> listOfFilesPath = new List<string>();
        string signatureLocatoin = string.Empty;
        string folderLocation = string.Empty;

        public Sign()
        {
            InitializeComponent();
        }

        private void ImportPDF_Click(object sender, RoutedEventArgs e)
        {
            listOfFilesPath.Clear();
            listBox.Items.Clear();
            ImportExport.ImportFiles(out listOfFilesPath, "PDF files (*.pdf)|*.pdf");

            listBox.Items.Add("Broj ucitanih fajlova: " + listOfFilesPath.Count());
        }

        private void ImportSign_Click(object sender, RoutedEventArgs e)
        {
            ImportExport.ImportFile(out signatureLocatoin, "PNG file (*.png)|*.png");

            listBox.Items.Add("Lokacija ucitanog potpisa: " + signatureLocatoin);
        }

        private void Folder_Click(object sender, RoutedEventArgs e)
        {
            ImportExport.ExportFolder(out folderLocation);

            listBox.Items.Add("Putanja foldera: " + folderLocation);
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string errors = string.Empty;
                Tools.SignPDF(listOfFilesPath, folderLocation, signatureLocatoin, out errors);

                if (!errors.Equals(string.Empty))
                    listBox.Items.Add(errors);

                listBox.Items.Add("--- Operacija uspesno zavrsena. ---");
            }
            catch (Exception ex)
            {
                listBox.Items.Clear();
                listBox.Items.Add(ex.Message);
            }
        }
    }
}
