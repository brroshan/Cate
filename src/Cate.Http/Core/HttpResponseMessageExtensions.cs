using System.Net.Http;
using System.Threading.Tasks;
using static Cate.Http.Utils.Extensions;

namespace Cate.Http.Core
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> ReceiveAs<T>(this Task<HttpResponseMessage> source)
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

        public static T ReceiveAs<T>(this HttpResponseMessage source)
            => Deserialize<T>(source).Result;

        public static string ReceiveString(this HttpResponseMessage source)
            => AsString(source).Result;

        private static async Task<string> AsString(HttpResponseMessage response)
        {
            if (response == null) return null;
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
    }
}