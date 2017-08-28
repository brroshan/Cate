using System.Net.Http;
using Cate.Http.Handlers;

namespace Cate.Http.Factories
{
    public class StandardFactory : IFactory
    {
        public virtual HttpClient GetClient(HttpMessageHandler handler)
        {
            return new HttpClient(new StandardMessageHandler(handler));
        }

        public virtual HttpMessageHandler GetHandler()
        {
            return new HttpClientHandler();
        }
    }
}