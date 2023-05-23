using System;
using System.Collections.Generic;
using ver3;

namespace scanner
{
    public class Scanner : BaseDevice, IScanner
    {
        public int Scan_counter { get; set; } = 0;

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.PDF)
        {
            document = new TextDocument("");

            if (GetState() == IDevice.State.on)
            {
                string current_DateTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                Scan_counter++;

                switch (formatType)
                {
                    case IDocument.FormatType.PDF:
                        document = new PDFDocument($"PDFScan{ Scan_counter }.pdf");
                        Console.WriteLine($"{ current_DateTime } Scan: { document.GetFileName() }");
                        break;
                    case IDocument.FormatType.JPG:
                        document = new ImageDocument($"ImageScan{ Scan_counter }.jpg");
                        Console.WriteLine($"{ current_DateTime } Scan: { document.GetFileName() }");
                        break;
                    case IDocument.FormatType.TXT:
                        document = new TextDocument($"TextScan{ Scan_counter }.txt");
                        Console.WriteLine($"{ current_DateTime } Scan: { document.GetFileName() }");
                        break;
                }
            }
        }
    }
}
