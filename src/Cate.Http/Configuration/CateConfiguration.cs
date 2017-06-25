using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cate.Http.Core;
using Cate.Http.Factories;
using Cate.Http.Serializers;
using static System.String;

namespace Cate.Http.Configuration
{
    public class CateConfiguration
    {
        public CateConfiguration()
        {
            StandardConfiguration();
        }

        private Uri _baseAddress;

        public Uri BaseAddress
        {
            get => _baseAddress;
            set
            {
                if (value.OriginalString.EndsWith("/"))
                    _baseAddress = value;

                _baseAddress = new Uri($"{value.OriginalString}/");
            }
        }

        public TimeSpan Timeout { get; set; }

        public IClientFactory Factory { get; set; }

        public ISerializer JsonSerializer { get; set; }
        public ISerializer XmlSerializer { get; set; }

        public CateConfiguration Clone() => (CateConfiguration)MemberwiseClone();

        public IWebProxy Proxy { get; set; }
        public bool PreAuthenticate { get; set; }
        public ICredentials Credentials { get; set; }

        public KeyValuePair<string, string> BasicAuthCredentials { get; set; }

        public bool IsUsingBasicAuth =>
            !IsNullOrWhiteSpace(BasicAuthCredentials.Key) ||
            !IsNullOrWhiteSpace(BasicAuthCredentials.Value);

        public string OAuthAccessToken { get; set; }
        public bool IsUsingOAuth => !IsNullOrWhiteSpace(OAuthAccessToken);

        private void StandardConfiguration()
        {
            Factory = new StandardFactory();
            JsonSerializer = new JsonNETSerializer();
            XmlSerializer = new StandardXmlSerializer();
            Timeout = TimeSpan.FromSeconds(100);
        }

        public Func<CateHttpContext, Task> OnStartAsync { get; set; }
        public Func<CateHttpContext, Task> OnErrorAsync { get; set; }
        public Func<CateHttpContext, Task> OnEndedAsync { get; set; }
    }
}