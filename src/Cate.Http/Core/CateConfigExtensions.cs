using Cate.Http.Utils;

namespace Cate.Http.Core
{
    public static class CateConfigExtensions
    {
        public static IHttpCate UseBasicAuth(this IHttpCate cate, string username,
                                             string password)
        {
            cate.Client.SetBasicAuthentication(username, password);
            return cate;
        }
    }
}