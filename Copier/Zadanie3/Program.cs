using System;
using ver3;

namespace Zadanie3
{
    class Program
    {
        static void Main(string[] args)
        {
            var xerox = new Copier();
            xerox.PowerOn();
            IDocument doc1 = new PDFDocument("aaa.pdf");
            xerox.Print(in doc1);

            IDocument doc2;
            xerox.Scan(out doc2);

            xerox.ScanAndPrint();
            System.Console.WriteLine(xerox.Counter);
            System.Console.WriteLine(xerox.PrintCounter);
            System.Console.WriteLine(xerox.ScanCounter);

            xerox.PowerOff();

            // ------------------------

            var newDevice = new MultidimensionalDevice();
            newDevice.PowerOn();
            IDocument doc3 = new ImageDocument("FaxImg.jpg");

            newDevice.Print(in doc3);
            newDevice.Scan(out doc3);
            newDevice.Send(in doc3, "606412789");

            newDevice.ScanAndPrint();
            newDevice.Scan_Send_Print("606456789");

            Console.WriteLine(newDevice.Counter);
            Console.WriteLine("Print counter " + newDevice.PrintCounter);
            Console.WriteLine("Send counter " + newDevice.SendCounter);
            Console.WriteLine("Scan counter " + newDevice.ScanCounter);

            newDevice.PowerOff();
        }
    }
}
