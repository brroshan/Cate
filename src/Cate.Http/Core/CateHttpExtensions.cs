using System.Net.Http;
using System.Threading.Tasks;
using Cate.Http.Content;

namespace Cate.Http.Core
{
    public static class CateHttpExtensions
    {
        public static async Task<HttpResponseMessage> GetJsonAsync(
            this IHttpCate cate, string uri)
            => await cate.SendAsync(uri, HttpMethod.Get, MimeType.Json);

        public static async Task<HttpResponseMessage> PutJsonAsync<T>(
            this IHttpCate cate, string uri, T data)
        {
            var body = new JsonBody(cate
                .Configuration.JsonSerializer.Serialize(data));
            return await cate.SendAsync(uri, HttpMethod.Put, MimeType.Json, body);
        }

        public static async Task<HttpResponseMessage> PostJsonAsync<T>(
            this IHttpCate cate, string uri, T data)
        {
            var body = new JsonBody(cate
                .Configuration.JsonSerializer.Serialize(data));
            return await cate.SendAsync(uri, HttpMethod.Post, MimeType.Json, body);
        }

        public static async Task<HttpResponseMessage> GetXmlAsync(
            this IHttpCate cate, string uri)
            => await cate.SendAsync(uri, HttpMethod.Get, MimeType.Xml);

        public static async Task<HttpResponseMessage> PutXmlAsync<T>(
            this IHttpCate cate, string uri, T data)
        {
            var body = new XmlBody(cate.Configuration.XmlSerializer.Serialize(data));
            return await cate.SendAsync(uri, HttpMethod.Put, MimeType.Xml, body);
        }

        public static async Task<HttpResponseMessage> PostXmlAsync<T>(
            this IHttpCate cate, string uri, T data)
        {
            var body =
                new XmlBody(cate.Configuration.XmlSerializer.Serialize(data));
            return await cate.SendAsync(uri, HttpMethod.Post, MimeType.Xml, body);
        }

        public static async Task<HttpResponseMessage> DeleteAsync(
            this IHttpCate cate, string uri)
            => await cate.SendAsync(uri,
                HttpMethod.Delete, MimeType.Json);
    }
}