using System;
using System.Net;
using System.Xml;
using System.Collections.Generic;
using Cate.Http.Factories;
using Cate.Http.Serializers;

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

        public Registry UseJsonSerializer<T>()
            where T : ISerializer, new()
        {
            Configuration.JsonSerializer = new T();
            return this;
        }

        public Registry UseXmlSerializer<T>()
            where T : ISerializer, new()
        {
            Configuration.XmlSerializer = new T();
            return this;
        }

        public Registry WithXmlWriterSettings(XmlWriterSettings settings)
        {
            Configuration.XmlSerializer.Settings = settings;
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

        public Registry UseProxy(IWebProxy proxy)
        {
            Configuration.Proxy = proxy;
            return this;
        }

        public Registry UsePreAuthentication(bool preAuthenticate)
        {
            Configuration.PreAuthenticate = preAuthenticate;
            return this;
        }

        public Registry WithAuthProtocolCredentials(ICredentials cc)
        {
            Configuration.Credentials = cc;
            return this;
        }

        public Registry WithBasicAuthentication(string username, string password)
        {
            Configuration.BasicAuthCredentials =
                new KeyValuePair<string, string>(username,
                    password);
            return this;
        }
    }
}