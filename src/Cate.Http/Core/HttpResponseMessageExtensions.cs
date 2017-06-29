using System;
using System.Net.Http;
using System.Threading.Tasks;
using Cate.Http.Utils;

namespace Cate.Http.Core
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> Receive<T>(this Task<HttpResponseMessage> source)
        {
            var response = await source.ConfigureAwait(false);
            return await Deserialize<T>(response);
        }

        public static async Task<string> ReceiveString(
            this Task<HttpResponseMessage> source)
        {
            var response = await source.ConfigureAwait(false);
            return await AsString(response);
        }

        public static T Receive<T>(this HttpResponseMessage source)
            => Deserialize<T>(source).Result;

        public static string ReceiveString(this HttpResponseMessage source)
            => AsString(source).Result;

        private static async Task<string> AsString(HttpResponseMessage response)
        {
            if (response == null) return null;
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        private static async Task<T> Deserialize<T>(HttpResponseMessage response)
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
                    $"Unsupported media type ({response.MediaType()})", null);
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