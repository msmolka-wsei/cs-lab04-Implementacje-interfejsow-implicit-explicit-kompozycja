using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;

namespace Zadanie1
{
    public class Copier : BaseDevice, IPrinter, IScanner
    {
        public int PrintCounter { get; set; } = 0;
        public int ScanCounter { get; set; } = 0;


        public void Print(in IDocument document)
        {
            if (IDevice.State.off == state)
                return;

            DateTime today = DateTime.Today;
            Console.WriteLine(today.ToString("M/d/yy hh:mm:ss") + " Print " + Convert.ToString(document.GetFileName()));
            PrintCounter++;
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType=IDocument.FormatType.JPG) 
        {   
            document = null;
            if (IDevice.State.off == state)
                return;
            DateTime today = DateTime.Today;
            switch (formatType)
            {
                case IDocument.FormatType.PDF:
                    document = new PDFDocument($"PDFDocument{ScanCounter++:0000}.pdf");
                    break;
                case IDocument.FormatType.JPG:
                    document = new ImageDocument($"JPGDocument{ScanCounter++:0000}.jpg");
                    break;

                case IDocument.FormatType.TXT:
                    document = new TextDocument($"TXTDocument{ScanCounter++:0000}.txt");
                    break;

                default:
                    throw new NotImplementedException();
            }
            Console.WriteLine(today.ToString("M/d/yy hh:mm:ss") + " Scan: " + Convert.ToString(document.GetFileName()));

        }

        public void ScanAndPrint()
        {
            IDocument doc;
            Scan(out doc);
            Print(in doc);
        }
    }
}
