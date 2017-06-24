using System;
using System.Net.Http;
using Cate.Http.Configuration;

namespace Cate.Http.Core
{
    public class CateHttpContext
    {
        private const string Key = "CateHttp";

        internal CateHttpContext(HttpRequestMessage request, CateConfiguration configuration)
        {
            Request = request ?? throw new ArgumentNullException(nameof(request));
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            Uri = new Uri(request.RequestUri.AbsoluteUri);
            request.Properties?.Add(Key, this);
        }

        public Uri Uri { get; }

        public CateConfiguration Configuration { get; set; }

        public HttpRequestMessage Request { get; }
        public HttpResponseMessage Response { get; set; }

        public Exception Error { get; internal set; }

        public bool Completed => Response != null;
        public bool Succeeded => Completed && Response.IsSuccessStatusCode;
        public bool HasHandledException { get; set; }

        internal static CateHttpContext Extract(HttpRequestMessage request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.Properties?.ContainsKey(Key) ?? false) {
                return (CateHttpContext)request.Properties[Key];
            }

            throw new Exception($"Could not find {Key} in the request");
        }
    }
}