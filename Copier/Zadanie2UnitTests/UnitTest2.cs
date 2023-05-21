using Microsoft.VisualStudio.TestTools.UnitTesting;
using ver1;
using Zadanie2;
using System;
using System.IO;

namespace Zadanie2UnitTests
{

    public class ConsoleRedirectionToStringWriter : IDisposable
    {
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        public ConsoleRedirectionToStringWriter()
        {
            stringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }

        public string GetOutput()
        {
            return stringWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }
    }


    [TestClass]
    public class UnitTestMultifunctionalDevice
    {
        [TestMethod]
        public void Device_GetState_StateOff()
        {
            var device = new MultifunctionalDevice();
            device.PowerOff();

            Assert.AreEqual(IDevice.State.off, device.GetState());
        }

        [TestMethod]
        public void Device_GetState_StateOn()
        {
            var device = new MultifunctionalDevice();
            device.PowerOn();

            Assert.AreEqual(IDevice.State.on, device.GetState());
        }

        [TestMethod]
        public void Device_Send_on()
        {
            var device = new MultifunctionalDevice();
            device.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc = new TextDocument("ala.txt");
                string address = "123123123";
                device.Send(in doc, address);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("sent"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(address));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void Device_Send_off()
        {
            var device = new MultifunctionalDevice();
            device.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc = new TextDocument("ala.txt");
                string address = "123123123";
                device.Send(in doc, address);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("sent"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains(address));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void AIO_on()
        {
            var device = new MultifunctionalDevice();
            device.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc = new TextDocument("fax.pdf");
                string address = "345678100";
                device.AIO(address);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("sent"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(address));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void AIO_off()
        {
            var device = new MultifunctionalDevice();
            device.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc = new TextDocument("fax.pdf");
                string address = "345678100";
                device.AIO(address);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("sent"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains(address));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_sendCounter()
        {
            var device = new MultifunctionalDevice();
            device.PowerOn();

            IDocument doc1 = new PDFDocument("fax.pdf");
            device.Send(in doc1, "123123123");
            IDocument doc2 = new TextDocument("fax.txt");
            device.Send(in doc2, "606345980");
            IDocument doc3 = new ImageDocument("fax.jpg");
            device.Send(in doc3, "567332145");

            device.AIO("121212129");
            device.AIO("345678900");


            Assert.AreEqual(5, device.SendCounter);
        }
    }
}