using Cate.Http.Utils;

namespace Cate.Http.Core
{
    public static class CateConfigExtensions
    {
        public static ICateHttp UseBasicAuth(this ICateHttp cate, string username,
                                             string password)
        {
            cate.Client.SetBasicAuthHeader(username, password);
            return cate;
        }

        public static ICateHttp UseOAuth(this ICateHttp cate, string token)
        {
            cate.Client.SetOAuthHeader(token);
            return cate;
        }
    }
}