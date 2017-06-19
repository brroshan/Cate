using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Cate.Http.Core;

namespace Cate.Http.Handlers
{
    public class StandardMessageHandler : DelegatingHandler
    {
        public StandardMessageHandler(HttpMessageHandler innerHandler) : base(innerHandler)
        { }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var context = CateHttpContext.Extract(request);

            try {
                context.Response = await BaseSendAsync(context, request, cancellationToken)
                    .ConfigureAwait(false);
                context.Response.RequestMessage = request;

                if (context.Successful)
                    return context.Response;

                throw new CateHttpException(context);
            }
            catch (Exception ex) {
                context.Error = ex;
                throw;
            }
        }

        private async Task<HttpResponseMessage> BaseSendAsync(
            CateHttpContext context, HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            try {
                return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex) {
                throw new CateHttpException(context, ex);
            }
        }
    }
}