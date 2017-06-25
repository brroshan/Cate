using System;
using System.Net.Http;
using System.Threading.Tasks;
using Cate.Http.Configuration;
using Cate.Http.Utils;

namespace Cate.Http.Core
{
    public interface ICateHttp
    {
        CateConfiguration Configuration { get; set; }

        HttpMessageHandler MessageHandler { get; }

        HttpClient Client { get; }

        Task<HttpResponseMessage> SendAsync(string uri, HttpMethod method,
                                            MimeType mimeType, HttpContent body = null);
    }

    public class CateHttp : ICateHttp
    {
        private HttpClient _client;
        private HttpMessageHandler _handler;

        public CateHttp()
        {
            Configuration = CateStartup.Registry.Configuration.Clone();
        }

        public CateHttp(Action<CateConfiguration> configure)
            : this()
        {
            configure(Configuration);
        }

        public CateHttp(string baseAddress)
            : this()
        {
            if (!string.IsNullOrWhiteSpace(baseAddress)) {
                Configuration.BaseAddress = new Uri(baseAddress);
            }
        }

        public CateConfiguration Configuration { get; set; }

        public HttpMessageHandler MessageHandler
        {
            get
            {
                if (_handler == null) {
                    _handler = Configuration.Factory.GetHandler();

                    if (_handler is HttpClientHandler httpClientHandler) {
                        SetClientHandler(httpClientHandler);
                    }
                }
                return _handler;
            }
        }

        private void SetClientHandler(HttpClientHandler httpClientHandler)
        {
            httpClientHandler.Proxy = Configuration.Proxy;
            httpClientHandler.UseProxy = Configuration.Proxy != null;
            httpClientHandler.PreAuthenticate = Configuration.PreAuthenticate;
            httpClientHandler.Credentials = Configuration.Credentials;
        }

        public HttpClient Client
        {
            get
            {
                if (_client == null) {
                    _client = Configuration.Factory.GetClient(MessageHandler);
                    SetClient();
                }
                return _client;
            }
        }

        private void SetClient()
        {
            _client.BaseAddress = Configuration.BaseAddress;
            _client.Timeout = Configuration.Timeout;

            if (Configuration.IsUsingBasicAuth)
                _client.SetBasicAuthHeader(
                    Configuration.BasicAuthCredentials.Key,
                    Configuration.BasicAuthCredentials.Value);

            if (Configuration.IsUsingOAuth)
                _client.SetOAuthHeader(Configuration.OAuthAccessToken);
        }

        public async Task<HttpResponseMessage> SendAsync(
            string url, HttpMethod method, MimeType mimeType, HttpContent body = null)
        {
            CateHttpContext context = null;

            try {
                var uri = url.AppendTo(Configuration.BaseAddress);
                var request =
                    new HttpRequestMessage(method, uri) { Content = body };
                request.Accept(mimeType);
                context = new CateHttpContext(request, Configuration);

                return await Client.SendAsync(request).ConfigureAwait(false);
            }
            catch when (context != null && context.HasHandledException) {
                return context.Response;
            }
        }
    }
}