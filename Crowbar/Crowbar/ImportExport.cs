using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Crowbar
{
    public class ImportExport
    {
        public static void ImportFiles(out List<string> listOfFilesPath, string fileType)
        {
            listOfFilesPath = new List<string>();

            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = fileType;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);

            if (openFileDialog.ShowDialog() == true)
                foreach (string fileName in openFileDialog.FileNames)
                    listOfFilesPath.Add(fileName);
        }

        public static void ImportFile(out string fileLocation, string fileType)
        {
            fileLocation = string.Empty;

            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = fileType;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);

            if (openFileDialog.ShowDialog() == true)
                fileLocation = openFileDialog.FileName;
        }

        public static void ExportFolder(out string folderLocation)
        {
            folderLocation = string.Empty;

            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    folderLocation = folderBrowserDialog.SelectedPath;
        }
    }
}
