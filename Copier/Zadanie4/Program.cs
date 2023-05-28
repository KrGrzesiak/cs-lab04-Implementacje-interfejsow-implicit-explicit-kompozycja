using System;
using ver4;

namespace Zadanie4
{
    class Program
    {
        static void Main(string[] args)
        {
            var xerox = new Copier();
            // Stan początkowy
            Console.WriteLine(xerox.GetState());
            xerox.PowerOn();
            IDocument doc1 = new PDFDocument("zad4.pdf");
            xerox.Print(in doc1);
            xerox.Print(in doc1);
            xerox.Print(in doc1);
            // Stan po wydrukowaniu 3 dokumentów
            Console.WriteLine(xerox.GetState());
            xerox.Print(in doc1);
            // Stan po wydrukowaniu 4 dokumentów
            Console.WriteLine(xerox.GetState());

            IDocument doc2;
            xerox.Scan(out doc2);
            xerox.Scan(out doc2);
            // Stan po zeskanowaniu 2 dokumentów
            Console.WriteLine(xerox.GetState());
            xerox.Scan(out doc2);
            // Stan po zeskanowaniu 3 dokumentów
            Console.WriteLine(xerox.GetState());


            xerox.ScanAndPrint();
            xerox.ScanAndPrint();
            xerox.ScanAndPrint();
            // Stan po zeskanowaniu 6 dokumentów i wydrukowaniu 7 dokumentów
            Console.WriteLine(xerox.GetState());

            xerox.PowerOff();
            Console.WriteLine(xerox.GetState());

            System.Console.WriteLine(xerox.Counter);
            System.Console.WriteLine(xerox.PrintCounter);
            System.Console.WriteLine(xerox.ScanCounter);
        }
    }
}
