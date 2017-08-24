using System;
using System.Net.Http;
using Cate.Http.Handlers;

namespace Cate.Http.Factories
{
    public class MockFactory : IFactory
    {
        public HttpClient GetClient(HttpMessageHandler handler)
        {
            return new HttpClient(handler);
        }
        public HttpMessageHandler GetHandler()
        {
            return new MockMessageHandler();
        }
    }
}