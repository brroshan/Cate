using System.Net.Http;
using Cate.Http.Handlers;

namespace Cate.Http.Factories
{
    public interface IClientFactory
    {
        HttpClient GetClient(HttpMessageHandler handler);
        HttpMessageHandler GetHandler();
    }

    public class StandardFactory : IClientFactory
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