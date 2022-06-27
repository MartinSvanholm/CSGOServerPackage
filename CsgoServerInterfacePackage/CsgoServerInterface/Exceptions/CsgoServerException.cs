using CsgoServerInterface.CsgoServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CsgoServerInterface.Exceptions;

public class CsgoServerException : Exception
{
    public CsgoServerException(string message, ICsgoServer csgoServer, HttpStatusCode httpStatusCode) : base(message)
    {
        CsgoServer = csgoServer;
        StatusCode = httpStatusCode;
    }

    public CsgoServerException(string message, ICsgoServer csgoServer) : base(message)
    {
        CsgoServer = csgoServer;
    }

    public ICsgoServer CsgoServer { get; set; }
    public HttpStatusCode StatusCode { get; set; }
}
