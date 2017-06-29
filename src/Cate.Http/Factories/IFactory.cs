using System.Net.Http;

namespace Cate.Http.Factories
{
    public interface IFactory
    {
        HttpClient GetClient(HttpMessageHandler handler);
        HttpMessageHandler GetHandler();
    }
}