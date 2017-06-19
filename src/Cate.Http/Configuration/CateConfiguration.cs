using System;
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

        public CateConfiguration Clone() => (CateConfiguration)MemberwiseClone();
        
        private void StandardConfiguration()
        {
            HttpClientFactory = new StandardFactory();
            JsonSerializer = new JsonNETSerializer();
            Timeout = TimeSpan.FromSeconds(30);
        }
    }
}