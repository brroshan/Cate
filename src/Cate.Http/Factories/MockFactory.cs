using System;
using System.Net.Http;
using Cate.Http.Content;
using Cate.Http.Handlers;

namespace Cate.Http.Factories
{
    /// <summary>
    /// A basic factory with a mocked httpmessagehandler to be used in unit tests. Optionally accepts a function that instructs the mock how to generate a response from a request.
    /// </summary>
    public class MockFactory : IFactory
    {
        protected Func<HttpRequestMessage, HttpResponseMessage> _responseGenerator;

        public MockFactory(
            Func<HttpRequestMessage, HttpResponseMessage> responseGenerator = null)
        {
            _responseGenerator = responseGenerator;
        }

        public virtual HttpClient GetClient(HttpMessageHandler handler)
        {
            return new HttpClient(handler);
        }

        public virtual HttpMessageHandler GetHandler()
        {
            if (_responseGenerator == null) {
                _responseGenerator = request =>
                                         new HttpResponseMessage
                                         {
                                             RequestMessage = request,
                                             Content =
                                                 new JsonBody("{\"Test\":\"data\"}")
                                         };
            }

            return new MockMessageHandler(_responseGenerator);
        }
    }
}