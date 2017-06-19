using Cate.Http.Factories;
using Cate.Http.Serializers;
using System;

namespace Cate.Http.Configuration
{
    public sealed class Registry
    {
        private static readonly Lazy<Registry> _registry =
            new Lazy<Registry>(() => new Registry());

        private Registry()
        { }

        public static Registry Instance => _registry.Value;

        internal CateConfiguration Configuration { get; } = new CateConfiguration();


        public Registry UseFactory<T>()
            where T : IClientFactory, new()
        {
            Configuration.HttpClientFactory = new T();
            return this;
        }

        public Registry UseSerializer<T>()
            where T : ISerializer, new()
        {
            Configuration.JsonSerializer = new T();
            return this;
        }

        public Registry SetBaseAddress(string uri)
        {
            Configuration.BaseAddress = new Uri(uri);
            return this;
        }

        public Registry WithTimeout(int seconds)
        {
            Configuration.Timeout = TimeSpan.FromSeconds(seconds);
            return this;
        }
    }
}