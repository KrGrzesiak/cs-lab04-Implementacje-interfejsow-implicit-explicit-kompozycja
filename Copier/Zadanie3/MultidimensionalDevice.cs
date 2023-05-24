using System;
using System.Collections.Generic;
using ver3;
using printer;
using scanner;

namespace Zadanie3
{
    public class MultidimensionalDevice : BaseDevice, IFax
    {
        private readonly Printer printer = new();
        private readonly Scanner scanner = new();

        public int PrintCounter { get; set; } = 0;
        public int ScanCounter { get; set; } = 0;
        public int SendCounter { get; set; } = 0;
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
            printer.Print(in document);
            PrintCounter = printer.Print_counter;
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.PDF)
        {
            scanner.Scan(out document, formatType);
            ScanCounter = scanner.Scan_counter;
        }

        public void Send(in IDocument document, string address)
        {
            if (GetState() == IDevice.State.on)
            {
                SendCounter++;
                Console.WriteLine($"Sent: { document.GetFileName() } to: { address }");
            }
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

        public void Scan_Send_Print(string address)
        {
            if (GetState() == IDevice.State.on)
            {
                Scan(out IDocument document, IDocument.FormatType.JPG);
                Send(in document, address);
                Print(in document);

                PrintCounter = printer.Print_counter;
                ScanCounter = scanner.Scan_counter;
            }
        }
    }
}
