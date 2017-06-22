using System;
using System.Net;
using System.Collections.Generic;
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

        public Uri BaseAddress { get; set; }
        public TimeSpan Timeout { get; set; }

        public IClientFactory HttpClientFactory { get; set; }

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
            HttpClientFactory = new StandardFactory();
            JsonSerializer = new JsonNETSerializer();
            XmlSerializer = new StandardXmlSerializer();
            Timeout = TimeSpan.FromSeconds(30);
        }
    }
}