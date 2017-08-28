using System;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Cate.Http.Core;

namespace Cate.Http.Utils
{
    public static class Extensions
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

        public static string ContentMediaType(this HttpResponseMessage response)
            => response.Content.Headers.ContentType.MediaType;

        public static bool IsJson(this HttpResponseMessage response)
            => response.ContentMediaType().Contains("json");

        public static bool IsXml(this HttpResponseMessage response)
            => response.ContentMediaType().Contains("xml");

        public static void SetBasicAuthHeader(this HttpClient client, string username,
                                              string password)
        {
            var auth =
                Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", auth);
        }

        public static void SetOAuthHeader(this HttpClient client, string token)
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }
        
        public static string AppendTo(this string url, Uri baseAddress)
        {
            if (string.IsNullOrEmpty(url))
                throw new Exception("You must specify a resource or absolute uri.");

            if (url.StartsWith("/"))
                url = url.Substring(1);

            var uri = baseAddress?.AbsoluteUri + url;

            if (!Uri.TryCreate(uri, UriKind.Absolute, out Uri _))
                throw new Exception(
                    $"Unable to invoke an http request with the uri '{uri}'.");

            return uri;
        }

        public static async Task<T> Deserialize<T>(HttpResponseMessage response)
        {
            if (response == null) return default(T);
            if (!response.IsSuccessStatusCode) return default(T);

            var context = CateHttpContext.Extract(response.RequestMessage);

            try {
                if (response.IsJson())
                    using (var stream =
                        await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        return context.Configuration.JsonSerializer.Deserialize<T>(stream);

                if (response.IsXml())
                    using (var stream =
                        await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        return context.Configuration.XmlSerializer.Deserialize<T>(stream);

                throw new CateHttpException(context,
                    $"Unsupported media type ({response.ContentMediaType()})", null);
            }
            catch (CateHttpException ex) {
                context.Error = ex;
                throw;
            }
            catch (Exception ex) {
                context.Error = ex;
                throw new CateSerializerException(context, typeof(T), ex);
            }
        }
    }
}