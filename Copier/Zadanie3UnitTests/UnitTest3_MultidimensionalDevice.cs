using Microsoft.VisualStudio.TestTools.UnitTesting;
using ver3;
using Zadanie3;
using System;
using System.IO;

namespace ver3UnitTests
{
    [TestClass]
    public class UnitTestMultidimensionalDevice_zad3
    {
        [TestMethod]
        public void MultidimensionalDevice_GetState_StateOff()
        {
            var device = new MultidimensionalDevice();
            device.PowerOff();

            Assert.AreEqual(IDevice.State.off, device.GetState());
        }

        [TestMethod]
        public void MultidimensionalDevice_GetState_StateOn()
        {
            var device = new MultidimensionalDevice();
            device.PowerOn();

            Assert.AreEqual(IDevice.State.on, device.GetState());
        }


        // weryfikacja, czy po wywołaniu metody `Print` i włączeniu 'MultidimensionalDevice' w napisie pojawia się słowo `Print`
        [TestMethod]
        public void MultidimensionalDevice_Print_DeviceOn()
        {
            var device = new MultidimensionalDevice();
            device.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                device.Print(in doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Print` i wyłączeniu 'MultidimensionalDevice' w napisie NIE pojawia się słowo `Print`
        [TestMethod]
        public void MultidimensionalDevice_Print_DeviceOff()
        {
            var device = new MultidimensionalDevice();
            device.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                device.Print(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i Wyłączeniu 'MultidimensionalDevice' w napisie NIE pojawia się słowo `Scan`
        [TestMethod]
        public void MultidimensionalDevice_Scan_DeviceOff()
        {
            var device = new MultidimensionalDevice();
            device.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                device.Scan(out doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i włączeniu 'MultidimensionalDevice' w napisie pojawia się słowo `Scan`
        [TestMethod]
        public void MultidimensionalDevice_Scan_DeviceOn()
        {
            var device = new MultidimensionalDevice();
            device.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                device.Scan(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // Weryfikacja, czy przy włączonym 'MultidimensionalDevice' i po wywołaniu metody Send dostarcznej z Interfejsu IFax zostanie wypisane słowo 'sent' a także adres odbiorcy.
        public void MultidimensionalDevice_Send_DeviceOn()
        {
            var device = new MultidimensionalDevice();
            device.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc = new TextDocument("IFax.pdf");
                string address = "111222333";
                device.Send(in doc, address);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Sent"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(address));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // Weryfikacja, czy przy wyłączonym 'MultidimensionalDevice' i po wywołaniu metody Send NIE zostanie wypisane słowo 'sent' a także adres odbiorcy.
        [TestMethod]
        public void MultidimensionalDevice_Send_DeviceOff()
        {
            var device = new MultidimensionalDevice();
            device.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc = new TextDocument("IFax.pdf");
                string address = "123123123";
                device.Send(in doc, address);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Sent"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains(address));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy wywołanie metody `Scan` z parametrem określającym format dokumentu
        // zawiera odpowiednie rozszerzenie (`.jpg`, `.txt`, `.pdf`)
        [TestMethod]
        public void MultidimensionalDevice_Scan_FormatTypeDocument()
        {
            var device = new MultidimensionalDevice();
            device.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                device.Scan(out doc1, formatType: IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                device.Scan(out doc1, formatType: IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                device.Scan(out doc1, formatType: IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }


        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłącznemu 'MultidimensionalDevice' w napisie pojawiają się słowa `Print` oraz `Scan`
        [TestMethod]
        public void MultidimensionalDevice_ScanAndPrint_DeviceOn()
        {
            var device = new MultidimensionalDevice();
            device.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                device.ScanAndPrint();
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i przy włączonemu 'MultidimensionalDevice' w napisie NIE pojawia się słowo `Print` ani słowo `Scan`
        [TestMethod]
        public void MultidimensionalDevice_ScanAndPrint_DeviceOff()
        {
            var device = new MultidimensionalDevice();
            device.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                device.ScanAndPrint();
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // Weryfikacja czy włączone 'MultidimensionalDevice' zwraca 'Scan', 'Sent', adres odbiorcy, 'Print'.
        [TestMethod]
        public void MultidimensionalDevice_Scan_Send_Print_ON()
        {
            var device = new MultidimensionalDevice();
            string address = "123456789";
            device.PowerOn();

            var currentConsoleOUt = Console.Out;
            currentConsoleOUt.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                device.Scan_Send_Print(address);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Sent"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(address));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
        }

        // Weryfikacja czy włączone 'MultidimensionalDevice' zwraca 'Scan', 'Sent', adres odbiorcy, 'Print'.
        [TestMethod]
        public void MultidimensionalDevice_Scan_Send_Print_OFF()
        {
            var device = new MultidimensionalDevice();
            string address = "123456789";
            device.PowerOff();

            var currentConsoleOUt = Console.Out;
            currentConsoleOUt.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                device.Scan_Send_Print(address);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Sent"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains(address));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
        }

        [TestMethod]
        public void MultidimensionalDevice_PrintCounter()
        {
            var device = new MultidimensionalDevice();
            device.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            device.Print(in doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            device.Print(in doc2);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            device.Print(in doc3);

            device.PowerOff();
            device.Print(in doc3);
            device.Scan(out doc1);
            device.PowerOn();

            device.ScanAndPrint();
            device.ScanAndPrint();
            device.Scan_Send_Print("121234656");

            // 6 wydruków, gdy urządzenie włączone
            Assert.AreEqual(6, device.PrintCounter);
        }

        [TestMethod]
        public void MultidimensionalDevice_ScanCounter()
        {
            var device = new MultidimensionalDevice();
            device.PowerOn();

            IDocument doc1;
            device.Scan(out doc1);
            IDocument doc2;
            device.Scan(out doc2);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            device.Print(in doc3);

            device.PowerOff();
            device.Print(in doc3);
            device.Scan(out doc1);
            device.PowerOn();

            device.ScanAndPrint();
            device.ScanAndPrint();
            device.Scan_Send_Print("606906900");

            // 5 skany, gdy urządzenie włączone
            Assert.AreEqual(5, device.ScanCounter);
        }

        [TestMethod]
        public void MultidimensionalDevice_SendCounter()
        {
            var device = new MultidimensionalDevice();
            device.PowerOn();

            IDocument doc1 = new ImageDocument("send.jpg");
            device.Send(in doc1, "123456789");
            IDocument doc2 = new TextDocument("send.txt");
            device.Send(in doc2, "222333444");
            IDocument doc3 = new PDFDocument("send.pdf");
            device.Send(in doc3, "192837465");
            IDocument doc4 = new PDFDocument("send2_.pdf");
            device.Send(in doc4, "999888777");
            IDocument doc5 = new PDFDocument("send3_.pdf");
            device.Send(in doc5, "606457893");

            device.PowerOff();
            device.PowerOn();

            device.Scan_Send_Print("123456789");
            device.Scan_Send_Print("123987645");
            device.Scan_Send_Print("357913570");

            device.PowerOff();

            Assert.AreEqual(8, device.SendCounter);
        }

        [TestMethod]
        public void MultidimensionalDevice_PowerOnCounter()
        {
            var device = new MultidimensionalDevice();
            device.PowerOn();
            device.PowerOn();
            device.PowerOn();

            IDocument doc1;
            device.Scan(out doc1);
            IDocument doc2;
            device.Scan(out doc2);

            device.PowerOff();
            device.PowerOff();
            device.PowerOff();
            device.PowerOn();

            IDocument doc3 = new ImageDocument("aaa.jpg");
            device.Print(in doc3);

            device.PowerOff();
            device.Print(in doc3);
            device.Scan(out doc1);
            device.PowerOn();

            device.ScanAndPrint();
            device.ScanAndPrint();

            // 3 włączenia
            Assert.AreEqual(3, device.Counter);
        }

    }
}
