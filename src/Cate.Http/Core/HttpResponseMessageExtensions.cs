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
            if (response == null) return default(T);

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