using System;
using System.Net;
using Cate.Http.Factories;
using Cate.Http.Serializers;

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

        private void StandardConfiguration()
        {
            HttpClientFactory = new StandardFactory();
            JsonSerializer = new JsonNETSerializer();
            XmlSerializer = new StandardXmlSerializer();
            Timeout = TimeSpan.FromSeconds(30);
        }
    }
}