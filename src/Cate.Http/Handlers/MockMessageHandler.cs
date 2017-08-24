using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Cate.Http.Core;

namespace Cate.Http.Handlers
{
    internal class MockMessageHandler : HttpMessageHandler
    {
        public HttpResponseMessage FakeResponse { get; set; }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            FakeResponse.RequestMessage = request;
            return Task.FromResult(FakeResponse);
        }

        public static MockMessageHandler MockHandler(ICateHttp cate)
            => cate.MessageHandler as MockMessageHandler;
    }
}