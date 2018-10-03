using iTextSharp.text;
using iTextSharp.text.exceptions;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFStamperOnMergedPDF
{
    class Program
    {
        static void Main(string[] args)
        {
            AddPageNumber("../../Action-Verbs-for-Resumes.pdf", "../../Action-Verbs-for-Resumes.pdf");
            Console.ReadLine();
        }

        static void AddPageNumber(string fileIn, string fileOut)
        {
            byte[] bytes = File.ReadAllBytes(fileIn);
            Console.WriteLine($"Original PDF size in bytes { bytes}");
            Font blackFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);
            using (MemoryStream stream = new MemoryStream())
            {
                PdfReader reader = new PdfReader(bytes);
                using (PdfStamper stamper = new PdfStamper(reader, stream))
                {
                    int pages = reader.NumberOfPages;
                    for (int i = 1; i <= pages; i++)
                    {
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_CENTER, new Phrase(i.ToString(), blackFont), 568f, 15f, 0);
                    }
                    Console.WriteLine($"Page Size{pages }");
                }
                bytes = stream.ToArray();

                Console.WriteLine($"Stamped PDF size in bytes { bytes}");
            }
            File.WriteAllBytes(fileOut, bytes);
        }
    }
}
