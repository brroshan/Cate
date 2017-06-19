using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cate.Http.Core
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> ToAsync<T>(this Task<HttpResponseMessage> source)
        {
            var response = await source.ConfigureAwait(false);
            if (response == null) return default(T);

            var context = CateHttpContext.Extract(response.RequestMessage);
            try {
                using (var stream = await response
                    .Content.ReadAsStreamAsync().ConfigureAwait(false)) {
                    return context.Configuration.JsonSerializer.Deserialize<T>(stream);
                }
            }
            catch (Exception ex) {
                context.Error = ex;
                throw new CateSerializerException(context, typeof(T), ex);
            }
        }
    }
}