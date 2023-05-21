using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ver1
{
    public interface IFax : IDevice
    {
        void Send(in IDocument document, string address);
    }
}
