using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Cate.Http.Configuration;
using Cate.Http.Core;
using static Cate.Http.Configuration.CateStartup;

namespace Cate.Http.Handlers
{
    public class StandardMessageHandler : DelegatingHandler
    {
        public StandardMessageHandler(HttpMessageHandler innerHandler) :
            base(innerHandler)
        { }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var context = CateHttpContext.Extract(request);
            context.Watch.Start();

            try {
                await Emit(EventType.Start, context);
                context.Response =
                    await BaseSendAsync(context, request, cancellationToken)
                        .ConfigureAwait(false);
                context.Response.RequestMessage = request;

                if (context.Succeeded)
                    return context.Response;

                throw new CateHttpException(context);
            }
            catch (Exception ex) {
                context.Error = ex;
                await Emit(EventType.Error, context);
                throw;
            }
            finally {
                context.Watch.Stop();
                await Emit(EventType.Ended, context);
            }
        }

        private async Task<HttpResponseMessage> BaseSendAsync(
            CateHttpContext context, HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            try {
                return await base.SendAsync(request, cancellationToken)
                                 .ConfigureAwait(false);
            }
            catch (Exception ex) {
                throw new CateHttpException(context, ex);
            }
        }
    }
}