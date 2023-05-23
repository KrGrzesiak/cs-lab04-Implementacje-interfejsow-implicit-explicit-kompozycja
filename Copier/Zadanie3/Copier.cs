using System;
using ver3;
using printer;
using scanner;

namespace Zadanie3
{
    public class Copier : BaseDevice, IPrinter, IScanner
    {
        private readonly Printer printer = new();
        private readonly Scanner scanner = new();

        public int PrintCounter { get; set; } = 0;
        public int ScanCounter { get; set; } = 0;
        public new int Counter { get; set; } = 0;

        public new void PowerOff()
        {
            if (GetState() == IDevice.State.on)
            {
                printer.PowerOff();
                scanner.PowerOff();
                base.PowerOff();
            }
        }

        public new void PowerOn()
        {
            if (GetState() == IDevice.State.off)
            {
                Counter++;
                printer.PowerOn();
                scanner.PowerOn();
                base.PowerOn();
            }
        }

        public void Print(in IDocument document)
        {
            PrintCounter = printer.Print_counter;
            printer.Print(in document);
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.PDF)
        {
            ScanCounter = scanner.Scan_counter;
            scanner.Scan(out document, formatType);
        }

        public void ScanAndPrint()
        {
            if (GetState() == IDevice.State.on)
            {
                Scan(out IDocument document, IDocument.FormatType.JPG);
                Print(in document);

                PrintCounter = printer.Print_counter;
                ScanCounter = scanner.Scan_counter;
            }
        }
    }
}
