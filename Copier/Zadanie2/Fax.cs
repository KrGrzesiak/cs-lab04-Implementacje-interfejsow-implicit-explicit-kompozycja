using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ver1
{
    /// <summary>
    /// Jeżeli urządzenie jest włączone to wysyła podany dokument pod podany adres telefoniczny
    /// </summary>
    /// <param name="document">obiekt typu IDocument, różny od `null`</param>
    /// <param name="address">Telefon odbiorcy</param>
    public interface IFax : IDevice
    {
        void Send(in IDocument document, string address);
    }
}
