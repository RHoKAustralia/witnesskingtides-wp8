using System;
using System.Net;

namespace KingTides.Core.Api.Communication
{
    public interface IWebRequestFactory
    {
        WebRequest Create(Uri requestUri, string acceptEncoding = null);
    }
}