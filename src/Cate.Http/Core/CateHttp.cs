using System;
using System.Net.Http;
using System.Threading.Tasks;
using Cate.Http.Configuration;
using Cate.Http.Utils;

namespace Cate.Http.Core
{
    public interface IHttpCate
    {
        CateConfiguration Configuration { get; set; }

        HttpMessageHandler MessageHandler { get; }

        HttpClient Client { get; }

        Task<HttpResponseMessage> SendAsync(string uri, HttpMethod method,
                                            MimeType mimeType, HttpContent body = null);
    }

    public class CateHttp : IHttpCate
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

        public string BaseUrl => Configuration.BaseAddress.AbsoluteUri;

        public CateConfiguration Configuration { get; set; }

        public HttpMessageHandler MessageHandler
        {
            get
            {
                if (_handler == null) {
                    _handler = Configuration.HttpClientFactory.GetHandler();

                    if (_handler is HttpClientHandler httpClientHandler) {
                        ConfigureClientHandler(httpClientHandler);
                    }
                }
                return _handler;
            }
        }

        private void ConfigureClientHandler(HttpClientHandler httpClientHandler)
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
                    _client = Configuration.HttpClientFactory.GetClient(MessageHandler);
                    _client.BaseAddress = Configuration.BaseAddress;
                    _client.Timeout = Configuration.Timeout;
                }
                return _client;
            }
        }

        public async Task<HttpResponseMessage> SendAsync(
            string url, HttpMethod method, MimeType mimeType, HttpContent body = null)
        {
            var uri = BaseUrl + url;
            var request = new HttpRequestMessage(method, uri) { Content = body };

            request.Accept(mimeType);

            var context = new CateHttpContext(request, Configuration);

            return await Client.SendAsync(request).ConfigureAwait(false);
        }
    }
}