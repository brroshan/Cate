using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Cate.Http.Content;

namespace Cate.Http.Core
{
    public static class CateHttpExtensions
    {
        public static async Task<HttpResponseMessage> GetJsonAsync(
            this ICateHttp cate, string uri)
            => await cate.SendAsync(uri, HttpMethod.Get, MimeType.Json);

        public static async Task<HttpResponseMessage> PutJsonAsync<T>(
            this ICateHttp cate, string uri, T data)
        {
            var body = new JsonBody(cate
                .Configuration.JsonSerializer.Serialize(data));
            return await cate.SendAsync(uri, HttpMethod.Put, MimeType.Json, body);
        }

        public static async Task<HttpResponseMessage> PostJsonAsync<T>(
            this ICateHttp cate, string uri, T data)
        {
            var body = new JsonBody(cate
                .Configuration.JsonSerializer.Serialize(data));
            return await cate.SendAsync(uri, HttpMethod.Post, MimeType.Json, body);
        }

        public static async Task<HttpResponseMessage> GetXmlAsync(
            this ICateHttp cate, string uri)
            => await cate.SendAsync(uri, HttpMethod.Get, MimeType.Xml);

        public static async Task<HttpResponseMessage> PutXmlAsync<T>(
            this ICateHttp cate, string uri, T data)
        {
            var body = new XmlBody(cate.Configuration.XmlSerializer.Serialize(data));
            return await cate.SendAsync(uri, HttpMethod.Put, MimeType.Xml, body);
        }

        public static async Task<HttpResponseMessage> PostXmlAsync<T>(
            this ICateHttp cate, string uri, T data)
        {
            var body =
                new XmlBody(cate.Configuration.XmlSerializer.Serialize(data));
            return await cate.SendAsync(uri, HttpMethod.Post, MimeType.Xml, body);
        }

        public static async Task<HttpResponseMessage> DeleteAsync(
            this ICateHttp cate, string uri)
            => await cate.SendAsync(uri,
                HttpMethod.Delete, MimeType.Json);

        public static async Task<HttpResponseMessage> PostFormUrlEncodedAsync(
            this ICateHttp cate, string uri,
            IEnumerable<KeyValuePair<string, string>> data)
        {
            var body = new FormUrlEncodedContent(data);
            return await cate.SendAsync(uri, HttpMethod.Post, MimeType.FormUrlEncoded,
                body);
        }

        public static HttpResponseMessage GetJson(
            this ICateHttp cate, string uri)
            => cate.GetJsonAsync(uri).Result;

        public static HttpResponseMessage PutJson<T>(
            this ICateHttp cate, string uri, T data)
            => cate.PutJsonAsync(uri, data).Result;

        public static HttpResponseMessage PostJson<T>(
            this ICateHttp cate, string uri, T data)
            => cate.PostJsonAsync(uri, data).Result;

        public static HttpResponseMessage GetXml(
            this ICateHttp cate, string uri)
            => cate.GetXmlAsync(uri).Result;

        public static HttpResponseMessage PutXml<T>(
            this ICateHttp cate, string uri, T data)
            => cate.PutXmlAsync(uri, data).Result;

        public static HttpResponseMessage PostXml<T>(
            this ICateHttp cate, string uri, T data)
            => cate.PostXmlAsync(uri, data).Result;

        public static HttpResponseMessage PostFormUrlEncode(
            this ICateHttp cate, string uri,
            IEnumerable<KeyValuePair<string, string>> data)
            => cate.PostFormUrlEncodedAsync(uri, data).Result;

        public static HttpResponseMessage Delete(
            this ICateHttp cate, string uri)
            => cate.DeleteAsync(uri).Result;
    }
}