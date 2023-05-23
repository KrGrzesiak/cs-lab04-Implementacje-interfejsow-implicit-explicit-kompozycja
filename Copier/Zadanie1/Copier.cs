using System;
using ver1;

namespace Zadanie1
{
    public class Copier : BaseDevice, IPrinter, IScanner
    {
        public int PrintCounter { get; set; } = 0;
        public int ScanCounter { get; set; } = 0;
        public new int Counter { get; set; } = 0;

        public new void PowerOn()
        {
            if(GetState() == IDevice.State.off)
            {
                Counter++;
                base.PowerOn();
            }
        }

        public void Print(in IDocument document)
        {
            if (GetState() == IDevice.State.on)
            {
                string current_DateTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                Console.WriteLine($"{ current_DateTime } Print: { document.GetFileName() }");
                PrintCounter++;
            }
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.PDF)
        {
            document = new TextDocument("");

            if (GetState() == IDevice.State.on)
            {
                string current_DateTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                ScanCounter++;

                switch (formatType)
                {
                    case IDocument.FormatType.PDF:
                        document = new PDFDocument($"PDFScan{ ScanCounter }.pdf");
                        Console.WriteLine($"{ current_DateTime } Scan: { document.GetFileName() }");
                        break;
                    case IDocument.FormatType.JPG:
                        document = new ImageDocument($"ImageScan{ ScanCounter }.jpg");
                        Console.WriteLine($"{ current_DateTime } Scan: { document.GetFileName() }");
                        break;
                    case IDocument.FormatType.TXT:
                        document = new TextDocument($"TextScan{ ScanCounter }.txt");
                        Console.WriteLine($"{ current_DateTime } Scan: { document.GetFileName() }");
                        break;
                }
            }
        }

        public void ScanAndPrint()
        {
            if (GetState() == IDevice.State.on)
            {
                Scan(out IDocument document, IDocument.FormatType.JPG);
                Print(document);
            }
        }
    }
}
