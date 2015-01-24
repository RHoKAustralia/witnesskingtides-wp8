using System;
using System.Net;

namespace KingTides.Core.Api.Communication
{
    public class WebRequestFactory : IWebRequestFactory
    {
        public WebRequest Create(Uri requestUri, string acceptEncoding = null)
        {
            var webrequest = (HttpWebRequest)WebRequest.Create(requestUri);
            webrequest.Headers["Origin"] = "http://localhost:5000";
            return webrequest;
        }
    }
}