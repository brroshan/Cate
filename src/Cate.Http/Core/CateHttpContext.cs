using System;
using System.Diagnostics;
using System.Net.Http;
using Cate.Http.Configuration;
using Cate.Http.Content;

namespace Cate.Http.Core
{
    public class CateHttpContext
    {
        private const string Key = "CateHttp";

        internal CateHttpContext(HttpRequestMessage request,
                                 CateConfiguration configuration)
        {
            Request = request;
            Configuration = configuration;

            Uri = new Uri(request.RequestUri.AbsoluteUri);
            Watch = new Stopwatch();
            request.Properties?.Add(Key, this);
        }

        public Uri Uri { get; }

        public CateConfiguration Configuration { get; set; }

        public HttpRequestMessage Request { get; }
        public HttpResponseMessage Response { get; internal set; }

        public Exception Error { get; internal set; }
        public string ErrorBody { get; set; }
        internal Stopwatch Watch { get; }
        public TimeSpan Lasted => Watch.Elapsed;

        public bool Completed => Response != null;
        public bool Succeeded => Completed && Response.IsSuccessStatusCode;
        public bool HasHandledException { get; set; }

        public string RequestBody
        {
            get
            {
                var data = (Request.Content as StringBody)?.Body
                           ?? Request?.Content?.ReadAsStringAsync().Result;
                return data ?? "No data sent along in the body of the request.";
            }
        }

        internal static CateHttpContext Extract(HttpRequestMessage request)
        {
            if (request.Properties?.ContainsKey(Key) ?? false)
                return (CateHttpContext)request.Properties[Key];

            return new CateHttpContext(request, CateStartup.Registry.Configuration.Clone());
        }

        public override string ToString()
        {
            return
                $"Uri: {Uri}, Completed: {Completed}, Succeeded: {Succeeded}, Lasted: {Lasted.TotalSeconds}s.";
        }
    }
}