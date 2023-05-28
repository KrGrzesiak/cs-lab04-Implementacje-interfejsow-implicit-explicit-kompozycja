using System;
using ver4;

namespace Zadanie4
{
    public class Copier : IPrinter, IScanner
    {
        public int PrintCounter { get; set; } = 0;
        public int ScanCounter { get; set; } = 0;
        public int Counter { get; set; } = 0;

        private IDevice.State Printer_state = IDevice.State.off;
        private IDevice.State Scanner_state = IDevice.State.off;

        public IDevice.State GetState()
        {
            if (Printer_state == IDevice.State.standby && Scanner_state == IDevice.State.standby) { return IDevice.State.standby; }
            if (Printer_state == IDevice.State.off && Scanner_state == IDevice.State.off) { return IDevice.State.off; }
            else { return IDevice.State.on; }
        }

        void IDevice.SetState(IDevice.State state)
        {
            Printer_state = state;
            Scanner_state = state;
        }

        public void PowerOn()
        {
            Counter++;
            Printer_state = IDevice.State.on;
            Scanner_state = IDevice.State.on;

            Console.WriteLine("<-- Device is On -->");
        }

        public void PowerOff()
        {
            Printer_state = IDevice.State.off;
            Scanner_state = IDevice.State.off;

            Console.WriteLine("<-- Device is Off -->");
        }

        public void StandbyOn()
        {
            if(GetState() != IDevice.State.off)
            {
                Printer_state = IDevice.State.standby;
                Scanner_state = IDevice.State.standby;

                Console.WriteLine("<-- Device is in Standby mode -->");
            }
        }

        public void StandbyOff()
        {
            if (GetState() != IDevice.State.off)
            {
                Printer_state = IDevice.State.on;
                Scanner_state = IDevice.State.on;

                Console.WriteLine("<-- Device come out from Standby mode -->");
            }
        }

        public void Print(in IDocument document)
        {

            if (GetState() != IDevice.State.off)
            {
                Printer_state = IDevice.State.on;
                Scanner_state = IDevice.State.standby;

                string current_DateTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                Console.WriteLine($"{ current_DateTime } Print: { document.GetFileName() }");
                PrintCounter++;

                if (PrintCounter % 3 == 0)
                {
                    Printer_state = IDevice.State.standby;
                }
            }
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.PDF)
        {
            document = new TextDocument("");

            if (GetState() != IDevice.State.off)
            {
                Scanner_state = IDevice.State.on;
                Printer_state = IDevice.State.standby;

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

                if (ScanCounter % 2 == 0)
                {
                    Scanner_state = IDevice.State.standby;
                }
            }
        }

        public void ScanAndPrint()
        {
            if (GetState() != IDevice.State.off)
            {
                Scan(out IDocument document, IDocument.FormatType.JPG);
                Print(document);
            }
        }
    }
}
