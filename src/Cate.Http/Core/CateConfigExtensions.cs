using System.Net.Http.Headers;
using Cate.Http.Utils;

namespace Cate.Http.Core
{
    public static class CateConfigExtensions
    {
        public static IHttpCate UseBasicAuth(this IHttpCate cate, string username,
                                             string password)
        {
            cate.Client.SetBasicAuthHeader(username, password);
            return cate;
        }

        public static IHttpCate UseOAuth(this IHttpCate cate, string token)
        {
            cate.Client.SetOAuthHeader(token);
            return cate;
        }
    }
}