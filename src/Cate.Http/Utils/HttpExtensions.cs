using System.Net.Http;
using System.Net.Http.Headers;
using Cate.Http.Core;

namespace Cate.Http.Utils
{
    internal static class HttpExtensions
    {
        public static void Accept(this HttpRequestMessage source, MimeType accept)
        {
            if(accept == MimeType.Json)
                source.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if(accept == MimeType.Xml)
                source.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

            source.Headers.AcceptCharset.Add(new StringWithQualityHeaderValue("UTF-8"));
        }
    }
}
