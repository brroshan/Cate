using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Cate.Http.Handlers
{
    internal class MockMessageHandler : HttpMessageHandler
    {
        private readonly Func<HttpRequestMessage, HttpResponseMessage> _responseGenerator;

        public MockMessageHandler(
            Func<HttpRequestMessage, HttpResponseMessage> responseGenerator)
        {
            _responseGenerator = responseGenerator;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = _responseGenerator(request);
            response.RequestMessage = request;
            
            return Task.FromResult(response);
        }
    }
}