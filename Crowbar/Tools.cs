using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Crowbar
{
    public class Tools
    {
        public static void SignPDF(List<string> filesPaths, string exportFolder, string signatureLocation, out string errors)
        {
            if (filesPaths.Count == 0)
                throw new Exception("Nisu uneti PDF fajlovi.");

            if (exportFolder.Equals(string.Empty))
                throw new Exception("Nije selektovana putanja ya potpisane PDF fajlove.");

            if (signatureLocation.Equals(string.Empty))
                throw new Exception("Nije unet potpis.");

            errors = string.Empty;

            foreach (var file in filesPaths)
            {
                try
                {
                    using (Stream inputPdfStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (Stream inputImageStream = new FileStream(exportFolder, FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (Stream outputPdfStream = new FileStream($@"{exportFolder}\{FileName(file)}.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        var reader = new PdfReader(inputPdfStream);
                        var stamper = new PdfStamper(reader, outputPdfStream);
                        var pdfContentByte = stamper.GetOverContent(1);

                        reader.SelectPages("1");

                        Image image = Image.GetInstance(inputImageStream);
                        image.ScaleAbsoluteHeight(65);
                        image.ScaleAbsoluteWidth(155);

                        image.SetAbsolutePosition(80, 80);
                        pdfContentByte.AddImage(image);
                        stamper.Close();
                    }
                }
                catch (Exception ex)
                {
                    errors += ex.Message + $" '{FileName(file)}'" + "\n";
                }
            }
        }

        private static string FileName(string filePath)
        {
            string[] exportPath = filePath.Split('\\');

            return exportPath[exportPath.Length - 1].Split('.')[0];
        }
    }
}
