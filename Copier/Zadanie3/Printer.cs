using System;
using System.Collections.Generic;
using ver3;

namespace printer
{
    public class Printer : BaseDevice, IPrinter
    {
        public int Print_counter { get; set; } = 0;

        public void Print(in IDocument document)
        {
            if (GetState() == IDevice.State.on)
            {
                Print_counter++;
                string current_DataTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                Console.WriteLine($"{ current_DataTime } Print: { document.GetFileName() }");
            }
        }
    }
}
