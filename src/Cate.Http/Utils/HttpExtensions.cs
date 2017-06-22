using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Cate.Http.Core;

namespace Cate.Http.Utils
{
    internal static class HttpExtensions
    {
        public static void Accept(this HttpRequestMessage source, MimeType accept)
        {
            if (accept == MimeType.Json)
                source.Headers.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

            if (accept == MimeType.Xml)
                source.Headers.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/xml"));

            source.Headers.AcceptCharset.Add(new StringWithQualityHeaderValue("UTF-8"));
        }

        public static string MediaType(this HttpResponseMessage response)
            => response.Content.Headers.ContentType.MediaType;

        public static bool IsJson(this HttpResponseMessage response)
            => response.MediaType().Contains("json");

        public static bool IsXml(this HttpResponseMessage response)
            => response.MediaType().Contains("xml");

        public static void SetBasicAuthentication(this HttpClient client, string username,
                                                  string password)
        {
            var auth =
                Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", auth);
        }
    }
}