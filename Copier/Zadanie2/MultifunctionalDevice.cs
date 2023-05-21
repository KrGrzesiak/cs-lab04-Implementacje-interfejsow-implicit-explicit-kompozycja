using System;
using ver1;
using Zadanie1;

namespace Zadanie2
{
    public class MultifunctionalDevice : Copier, IFax
    {
        public int SendCounter { get; set; } = 0;
        
        private bool Is_number(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] < '0' && str[i] > '9') { return false; }
            }

            return true;
        }

        public void Send(in IDocument document, string address)
        {
            if (GetState() == IDevice.State.on)
            {
                SendCounter++;
                Console.WriteLine($"The document \"{ document.GetFileName() }\" has been sent to address { address }!");
            }
        }

        public void AIO(string address)
        {
            if (GetState() == IDevice.State.on && (address.Length == 9 && Is_number(address)))
            {
                Scan(out IDocument document, IDocument.FormatType.PDF);
                Send(in document, address);
                Print(in document);
            }
        }
    }
}
