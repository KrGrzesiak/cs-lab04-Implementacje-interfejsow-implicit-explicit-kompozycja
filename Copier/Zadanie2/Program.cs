using System;
using ver1;

namespace Zadanie2
{
    class Program
    {
        static void Main(string[] args)
        {
            var unknown = new MultifunctionalDevice();
            unknown.PowerOn();
            IDocument doc1 = new PDFDocument("faxFile.pdf");
            unknown.Print(in doc1);

            IDocument doc2;
            unknown.Scan(out doc2);

            IDocument doc3 = new TextDocument("FAX_DOC_SENTME.txt");
            unknown.Send(in doc3, "605455123");

            unknown.ScanAndPrint();
            unknown.AIO("123123123");
            System.Console.WriteLine(unknown.Counter);
            System.Console.WriteLine(unknown.PrintCounter);
            System.Console.WriteLine(unknown.ScanCounter);
            System.Console.WriteLine(unknown.SendCounter);
        }
    }
}
