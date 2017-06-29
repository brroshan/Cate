using System.Net.Http;
using Cate.Http.Handlers;

namespace Cate.Http.Factories
{
    public class StandardFactory : IFactory
    {
        public HttpClient GetClient(HttpMessageHandler handler)
        {
            return new HttpClient(new StandardMessageHandler(handler));
        }

        public HttpMessageHandler GetHandler()
        {
            return new HttpClientHandler();
        }
    }
}