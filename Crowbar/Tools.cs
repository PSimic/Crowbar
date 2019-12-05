using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Office.Interop.Excel;

namespace Crowbar
{
    public class Tools
    {
        public static void SignPDF(List<string> filesPaths, string exportFolder, string signatureLocation, string upDown, string leftRight, out string errors)
        {
            if (filesPaths.Count == 0)
                throw new Exception("Nisu uneti PDF fajlovi.");

            if (signatureLocation.Equals(string.Empty))
                throw new Exception("Nije unet potpis.");

            if (exportFolder.Equals(string.Empty))
                throw new Exception("Nije selektovana putanja za potpisane PDF fajlove.");

            errors = string.Empty;

            foreach (var file in filesPaths)
            {
                try
                {
                    using (Stream inputPdfStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (Stream inputImageStream = new FileStream(signatureLocation, FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (Stream outputPdfStream = new FileStream($@"{exportFolder}\{FileName(file)}.pdf", FileMode.Create, FileAccess.Write, FileShare.Read))
                    {
                        var reader = new PdfReader(inputPdfStream);
                        var stamper = new PdfStamper(reader, outputPdfStream);
                        var pdfContentByte = stamper.GetOverContent(1);

                        reader.SelectPages("1");

                        Image image = Image.GetInstance(inputImageStream);
                        image.ScaleAbsoluteHeight(65);
                        image.ScaleAbsoluteWidth(155);

                        image.SetAbsolutePosition(int.Parse(leftRight), int.Parse(upDown));
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

        public static void ConvertFromExcelToPDF(List<string> filesPaths, string exportFolder, string sheetName, out string errors)
        {
            if (filesPaths.Count == 0)
                throw new Exception("Nisu uneti excel fajlovi.");

            if (exportFolder.Equals(string.Empty))
                throw new Exception("Nije selektovana putanja u koju ce se konvertovati excel fajlovi.");

            if (sheetName.Equals(string.Empty))
                throw new Exception("Nije unet naziv sheet-a");

            errors = string.Empty;

            Application xlsApp = new Application();

            foreach (string file in filesPaths)
            {
                Workbook wkb = xlsApp.Workbooks.Open(file);

                try
                {
                    foreach (Worksheet ws in wkb.Worksheets.OfType<Worksheet>())
                    {
                        if (ws.Name == sheetName)
                        {
                            ws.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                            ws.PageSetup.Zoom = false;
                            ws.PageSetup.FitToPagesWide = 1;
                            ws.PageSetup.FitToPagesTall = false;

                            ws.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, $@"{exportFolder}\{FileName(file)}.pdf");
                        }
                    }
                }
                catch (Exception ex)
                {
                    errors += ex.Message + $" '{FileName(file)}'" + "\n";
                }
                finally
                {
                    wkb.Close(false, file);
                }
            }

            xlsApp.DisplayAlerts = false;
            xlsApp.Quit();

        }

        public static decimal SumValues(List<string> filesPaths, string sheetName, out string response)
        {
            if (filesPaths.Count == 0)
                throw new Exception("Nisu uneti excel fajlovi.");

            response = string.Empty;
            decimal sum = 0;

            Application xlsApp = new Application();

            foreach (string file in filesPaths)
            {
                Workbook wkb = xlsApp.Workbooks.Open(file);

                try
                {
                    foreach (Worksheet ws in wkb.Worksheets.OfType<Worksheet>())
                    {
                        if (ws.Name == sheetName)
                        {
                            Range searchedRange = xlsApp.get_Range("A1", "Q2000");
                            Range currentFind = searchedRange.Find("Vrednost za nadelu");

                            Range cell = (Range)ws.Cells[currentFind.Row, 16];

                            if (cell.Value == null || cell.Value < 0)
                            {
                                Range cell2 = (Range)ws.Cells[currentFind.Row, 15];

                                if (cell2.Value == null || cell2.Value < 0)
                                    response += $"Vrednost za nadelu u fajlu '{FileName(file)}' nije ispravna.\n";
                                else
                                    sum += (decimal)cell2.Value;

                                continue;
                            }

                            sum += (decimal)cell.Value;
                        }
                    }

                }
                catch (Exception ex)
                {
                    response += ex.Message + " - " + FileName(file) + "\n";
                }
                finally
                {
                    wkb.Close(false, file);
                }
            }

            xlsApp.DisplayAlerts = false;
            xlsApp.Quit();

            return sum;
        }

        public static string FileName(string filePath)
        {
            string[] exportPath = filePath.Split('\\');

            return exportPath[exportPath.Length - 1].Split('.')[0];
        }
    }
}
